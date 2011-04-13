using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Core;
using System.IO;
using Frontend.Forms;
using System.Runtime.InteropServices;
using onlyconnect;
using System.Diagnostics;
using Frontend.HtmlEditorClasses;

namespace Frontend.UserControls
{
    public partial class CanvasTabPage : TabPage
    {
        /// <summary>
        /// For events which dont get reset on changeVisibility of htmleditor
        /// </summary>
        private bool eventsBound = false;

        public CanvasTabPage()
        {
            InitializeComponent();
            htmlEditor1.ReadyStateChanged += new ReadyStateChangedHandler(htmlEditor1_ReadyStateChanged);
        }

        /// <summary>
        /// Init when htmlEditor is ready
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void htmlEditor1_ReadyStateChanged(object sender, ReadyStateChangedEventArgs e)
        {
            if (e.ReadyState == "complete")
            {
                // Designer needs to be setup every time webbrowser initiates
                // (affects going from edit mode to design mode)
                HtmlEditorClasses.CRestrictedEditDesigner restrictedEditDesigner = new HtmlEditorClasses.CRestrictedEditDesigner();
                restrictedEditDesigner.moduleClicked += new HtmlEditorClasses.ModuleClickedEventHandler(restrictedEditDesigner_moduleClicked);
                restrictedEditDesigner.canvasClicked += new HtmlEditorClasses.CanvasClickedEventHandler(restrictedEditDesigner_canvasClicked);
                htmlEditor1.SetEditDesigner(restrictedEditDesigner);
                htmlEditor1.AllowDrop = true;

                // dropTarget is bound on complete readyState, though
                // remains when editor changes invisibility, so this would get
                // bound x times, unlike editDesigner above
                if (!this.eventsBound)
                {
                    htmlEditor1.dropTarget.dragEnter += new DragEnterHandler(theSite_dragEnter);
                    htmlEditor1.dropTarget.drop += new DropHandler(theSite_drop);
                    htmlEditor1.dropTarget.dragOver += new DragOverHandler(dropTarget_dragOver);

                    this.eventsBound = true;
                }
            }
        }

        void restrictedEditDesigner_canvasClicked(object sender, HtmlEditorClasses.ElementDataEventArgs e)
        {
            // Set menu
            if (e.eventObj.Button == BUTTON.RIGHT)
            {
                htmlEditor1.ContextMenuStrip = rightClickMenu;
            }
        }

        void restrictedEditDesigner_moduleClicked(object sender, HtmlEditorClasses.ElementDataEventArgs e)
        {
            // Set menu
            if (e.eventObj.Button == BUTTON.RIGHT)
            {
                htmlEditor1.ContextMenuStrip = moduleRightClickMenu;
            }
            // On leftclick show properties window - only when in designmode though
            if (e.eventObj.Button == BUTTON.LEFT && this.htmlEditor1.IsDesignMode)
            {
                HtmlAgilityPack.HtmlDocument doc = HTMLDocumentConverter.mshtmlDocToAgilityPackDoc(htmlEditor1.HtmlDocument2);
                HtmlAgilityPack.HtmlNode elem = doc.GetElementbyId(e.element.id);
                CFormController.Instance.mainForm.propertiesForm.moduleChanged += new ModuleChanged(propertiesForm_moduleChanged);
                CFormController.Instance.mainForm.showProperties(elem);
            }
        }

        void propertiesForm_moduleChanged(object sender, EventArgs e)
        {
            // Get edited module node
            HtmlAgilityPack.HtmlNode activeNode = CFormController.Instance.mainForm.propertiesForm.activeElem;

            // Change its content
            activeNode.InnerHtml = CXMLParser.Instance.getNodeFromModule(CFormController.Instance.mainForm.propertiesForm.module).OuterHtml;
            activeNode.InnerHtml += CFormController.Instance.mainForm.propertiesForm.module.generatePreview();

            // And load all back to IHTMLDocument
            htmlEditor1.LoadDocument("<BODY>" + activeNode.OwnerDocument.DocumentNode.InnerHtml + "</BODY>");
        }

        void theSite_drop(DataObject sender, DragEventArgs e)
        {
            CFormController.Instance.mainForm.setStatus("drop" + e.X.ToString());
            if (sender.GetData("System.Windows.Forms.ListView+SelectedListViewItemCollection", false) != null)
            {
                // Get module preview (multiple modules can be dragged)
                String input = String.Empty;
                ListView.SelectedListViewItemCollection listViewItemModules = (ListView.SelectedListViewItemCollection)sender.GetData("System.Windows.Forms.ListView+SelectedListViewItemCollection", false);
                foreach (ListViewItem listViewItemModule in listViewItemModules)
                {
                    input += CXMLParser.Instance.getPreviewFromProjectXML(CXMLParser.Instance.getNodeFromModule(CModuleReader.Instance.GetModuleInstanceFromName(listViewItemModule.Text)).OuterHtml);
                }

                // Get relative drop location
                Point htmlEditorCorner = htmlEditor1.PointToScreen(new Point(0, 0));
                int X = e.X - htmlEditorCorner.X;
                int Y = e.Y - htmlEditorCorner.Y;

                // Get element on which module was dropped
                IHTMLElement hoverElem = htmlEditor1.HtmlDocument2.ElementFromPoint(X, Y);

                if (hoverElem.tagName.Equals("BODY"))
                {
                    Debug.WriteLine("dropped on body");
                    if(hoverElem.innerText == null && hoverElem.innerHTML == null)
                        htmlEditor1.LoadDocument("<body>" + input + "</body>");
                    else
                        hoverElem.innerHTML += input;
                }
                else
                {
                    Debug.WriteLine("dropped on " + hoverElem.tagName);

                    // Mshtml deletes <module> in element load, 
                    // uhm so it has to be converted to HtmlAgilityPack.HtmlDocument
                    // and then back
                    Guid guid = new Guid();
                    hoverElem.SetAttribute("id", guid.ToString(), 0);

                    // Get wanted element and modify its content
                    HtmlAgilityPack.HtmlDocument htmlDoc = HTMLDocumentConverter.mshtmlDocToAgilityPackDoc(htmlEditor1.HtmlDocument2);
                    HtmlAgilityPack.HtmlNode node = htmlDoc.GetElementbyId(guid.ToString());
                    node.Attributes.Remove("id");
                    node.InnerHtml += input;

                    Debug.WriteLine("dropping in the end of " + node.Name);

                    // And back to IHTMLDocument
                    htmlEditor1.LoadDocument("<body>" + htmlDoc.DocumentNode.InnerHtml + "</body>");
                }
            }
        }

        void theSite_dragEnter(DataObject sender, DragEventArgs e)
        {
            //CFormController.Instance.mainForm.setStatus("dragEnter " + e.X.ToString());
        }

        void dropTarget_dragOver(DataObject sender, DragEventArgs e)
        {
            //Point htmlEditorCorner = htmlEditor1.PointToScreen(new Point(0, 0));
            //int X = e.X - htmlEditorCorner.X;
            //int Y = e.Y - htmlEditorCorner.Y;

            //IDisplayServices ds = (IDisplayServices)htmlEditor1.HtmlDocument2;
            //IDisplayPointer displayPointer;
            //ds.CreateDisplayPointer(out displayPointer);


            //tagPOINT targetPoint = new tagPOINT();
            ////HtmlElement elem = new HtmlElement();

            //targetPoint.x = 50;
            //targetPoint.y = 50;

            //uint whatever = 1;
            ////htmlEditor1.cur
            //    //displayPointer
            ////displayPointer.MoveToPoint(targetPoint, 
            //displayPointer.MoveToPoint(targetPoint, (int)COORD_SYSTEM.COORD_SYSTEM_CONTAINER, htmlEditor1.HtmlDocument2.GetBody() , whatever, out whatever);


            //ds.CreateDisplayPointer(out displayPointer);
            //IHTMLCaret caret;
            //int iRetVal = ds.GetCaret(out caret);
            ////caret.

            //caret.MoveCaretToPointer(displayPointer, false, CARET_DIRECTION.CARET_DIRECTION_FORWARD);
        }

        /// <summary>
        /// Project content
        /// </summary>
        public String XMLProjectContent
        {
            set
            {
                // Parse preview only if browser is shown
                if (this.tabControl1.SelectedTab == browserTabPage)
                {
                    string html = CXMLParser.Instance.getPreviewFromProjectXML(value);
                    htmlEditor1.LoadDocument("<body>" + html + "</body>");
                }
                textBox1.Text = value;
            }
            get
            {
                // Read project xml from browser preview
                if (this.tabControl1.SelectedTab == browserTabPage)
                {
                    // Or at least try
                    try
                    {
                        return CXMLParser.Instance.getProjectXMLFromPreview(htmlEditor1.HtmlDocument2.GetBody().innerHTML);
                    }
                    catch(Exception)
                    {
                        return textBox1.Text;
                    }
                }
                // Or from editmode textbox
                else
                {
                    return textBox1.Text;
                }
            }
        }

        private String _url;
        /// <summary>
        /// Url of edited file
        /// </summary>
        public String url
        {
            set
            {
                this._url = value;
            }
            get
            {
                return this._url;
            }
        }

        private void textBox1_DragDrop(object sender, DragEventArgs e)
        {
            // If modules from toolbox dropped
            if (e.Data.GetDataPresent("System.Windows.Forms.ListView+SelectedListViewItemCollection", false))
            {
                // Generate preview of those modules
                String input = String.Empty;
                ListView.SelectedListViewItemCollection listViewItemModules = (ListView.SelectedListViewItemCollection)e.Data.GetData("System.Windows.Forms.ListView+SelectedListViewItemCollection", false);
                foreach (ListViewItem listViewItemModule in listViewItemModules)
                {
                    input += CXMLParser.Instance.getNodeFromModule(CModuleReader.Instance.GetModuleInstanceFromName(listViewItemModule.Text)).OuterHtml;
                }

                // And insert them on cursor position
                int cursorPosition = textBox1.SelectionStart;
                textBox1.Text = textBox1.Text.Insert(textBox1.SelectionStart, input);
                textBox1.SelectionStart = cursorPosition + input.Length;
            }
        }

        private void textBox1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
            textBox1.Focus();
        }

        private void textBox1_DragOver(object sender, DragEventArgs e)
        {
            Point textboxCorner = textBox1.PointToScreen(new Point(0, 0));
            int X = e.X - textboxCorner.X;
            int Y = e.Y - textboxCorner.Y;
            textBox1.SelectionStart = textBox1.GetCharIndexFromPosition(new Point(X, Y));
            if (textBox1.SelectionStart == (textBox1.Text.Length - 1))
            {
                textBox1.SelectionStart++;
            }
        }

        private void wb_VisibleChanged(object sender, EventArgs e)
        {
            this.XMLProjectContent = textBox1.Text;
        }

        private void textBox1_VisibleChanged(object sender, EventArgs e)
        {
            this.XMLProjectContent = CXMLParser.Instance.getProjectXMLFromPreview(htmlEditor1.HtmlDocument2.GetBody().innerHTML);
            // Properties not clickable from editmode .. yet
            CFormController.Instance.mainForm.hideProperties();
        }

        private void viewSource_Click(object sender, EventArgs e)
        {
            ShowSourceForm showSource = new ShowSourceForm();
            showSource.textBox1.Text = htmlEditor1.GetDocumentSource();
            showSource.ShowDialog();
        }

        public String getSelectedTab()
        {
            if (this.tabControl1.SelectedTab == browserTabPage)
                return "browser";
            if (this.tabControl1.SelectedTab == textEditorTabPage)
                return "editor";
            else return String.Empty;
        }

        private void toggleEditMode_Click(object sender, EventArgs e)
        {
            // Need to invoke setter to refresh content
            // (editmode sets content to null automatically)
            this.XMLProjectContent = this.XMLProjectContent;

            // Toggle design mode
            this.htmlEditor1.IsDesignMode = !this.htmlEditor1.IsDesignMode;

            // And invoke getter
            this.XMLProjectContent = this.XMLProjectContent;
            this.htmlEditor1.InvokeReadyStateChanged("complete");
        }

    }
}
