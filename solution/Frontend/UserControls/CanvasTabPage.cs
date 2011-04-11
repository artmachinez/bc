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
        private bool previewSucceeded = true;
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
            // On leftclick show properties window
            if (e.eventObj.Button == BUTTON.LEFT)
            {
                HtmlAgilityPack.HtmlDocument doc = HTMLDocumentConverter.mshtmlDocToAgilityPackDoc(htmlEditor1.HtmlDocument2);
                HtmlAgilityPack.HtmlNode elem = doc.GetElementbyId(e.element.id);
                //CFormController.Instance.mainForm.
                CFormController.Instance.mainForm.propertiesForm.moduleChanged += new ModuleChanged(propertiesForm_moduleChanged);
                CFormController.Instance.mainForm.showProperties(elem);
            }
        }

        void propertiesForm_moduleChanged(object sender, EventArgs e)
        {
            HtmlAgilityPack.HtmlNode activeNode = CFormController.Instance.mainForm.propertiesForm.activeElem;

            //HtmlAgilityPack.HtmlDocument doc = CFormController.Instance.mainForm.propertiesForm.activeElem.OwnerDocument;
            activeNode.InnerHtml = CXMLParser.Instance.getNodeFromModule(CFormController.Instance.mainForm.propertiesForm.module).OuterHtml;
            activeNode.InnerHtml += CFormController.Instance.mainForm.propertiesForm.module.generatePreview();
            //HtmlAgilityPack.HtmlNode newNode = CFormController.Instance.mainForm.propertiesForm.module.generatePreview();
            //activeNode.ParentNode.ReplaceChild(, activeNode);

            //CXMLParser.Instance.get
            htmlEditor1.LoadDocument(activeNode.OwnerDocument.DocumentNode.InnerHtml);

        }

        void theSite_drop(DataObject sender, DragEventArgs e)
        {
            CFormController.Instance.mainForm.setStatus("drop" + e.X.ToString());
            if (sender.GetData("System.Windows.Forms.ListView+SelectedListViewItemCollection", false) != null)
            {
                String input = String.Empty;
                ListView.SelectedListViewItemCollection listViewItemModules = (ListView.SelectedListViewItemCollection)sender.GetData("System.Windows.Forms.ListView+SelectedListViewItemCollection", false);
                foreach (ListViewItem listViewItemModule in listViewItemModules)
                {
                    input += CXMLParser.Instance.getPreviewFromProjectXML(CXMLParser.Instance.getNodeFromModule(CModuleReader.Instance.GetModuleInstanceFromName(listViewItemModule.Text)).OuterHtml);
                }

                Point htmlEditorCorner = htmlEditor1.PointToScreen(new Point(0, 0));
                int X = e.X - htmlEditorCorner.X;
                int Y = e.Y - htmlEditorCorner.Y;

                IHTMLElement hoverElem = htmlEditor1.HtmlDocument2.ElementFromPoint(X, Y);

                Console.WriteLine(hoverElem.tagName + " " + X + "-" + Y);

                try
                {
                    hoverElem.outerHTML += input;
                }
                catch (COMException exc)
                {
                    htmlEditor1.HtmlDocument2.GetBody().innerHTML += input;
                    Debug.WriteLine(exc.Message);
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

        public String content
        {
            set
            {
                htmlEditor1.LoadDocument(CXMLParser.Instance.getPreviewFromProjectXML(value));
                textBox1.Text = value;
            }
            get
            {
                if (this.tabControl1.SelectedTab == browserTabPage)
                {
                    try
                    {
                        return CXMLParser.Instance.getProjectXMLFromPreview(htmlEditor1.HtmlDocument2.GetBody().innerHTML);
                    }
                    catch(Exception)
                    {
                        return textBox1.Text;
                    }
                }
                else
                {
                    return textBox1.Text;
                }
            }
        }

        // Url of edited file
        private String _url;
        public string url
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
            if (e.Data.GetDataPresent("System.Windows.Forms.ListView+SelectedListViewItemCollection", false))
            {
                String input = String.Empty;

                ListView.SelectedListViewItemCollection listViewItemModules = (ListView.SelectedListViewItemCollection)e.Data.GetData("System.Windows.Forms.ListView+SelectedListViewItemCollection", false);
                foreach (ListViewItem listViewItemModule in listViewItemModules)
                {
                    input += CXMLParser.Instance.getNodeFromModule(CModuleReader.Instance.GetModuleInstanceFromName(listViewItemModule.Text)).OuterHtml;
                }

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
            try
            {
                //String output = CXMLParser.Instance.getPreviewFromProjectXML(this.content);
                //this.htmlEditor1.LoadDocument(output);
                this.content = textBox1.Text;
                previewSucceeded = true;
            }
            catch (System.Xml.XmlException exc)
            {
                this.htmlEditor1.LoadDocument(exc.Message);
                previewSucceeded = false;
            }
            return;
        }

        private void textBox1_VisibleChanged(object sender, EventArgs e)
        {
            if (previewSucceeded)
            {
                this.content = CXMLParser.Instance.getProjectXMLFromPreview(htmlEditor1.HtmlDocument2.GetBody().innerHTML);
            }
        }

        private void viewSource_Click(object sender, EventArgs e)
        {
            ShowSourceForm showSource = new ShowSourceForm();
            showSource.textBox1.Text = htmlEditor1.GetDocumentSource();
            showSource.ShowDialog();
        }

    }
}
