using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Core;
using Core.Project;
using Core.Helpers;
using System.IO;
using Frontend.Forms;
using System.Runtime.InteropServices;
using onlyconnect;
using System.Diagnostics;
using Frontend.HtmlEditorClasses;
using Frontend.Helpers;

namespace Frontend.UserControls
{
    /// <summary>
    /// A project page, contains text editor and wysiwyg editor
    /// </summary>
    public partial class CanvasTabPage : TabPage
    {
        /// <summary>
        /// For events which dont get reset on changeVisibility of htmleditor
        /// </summary>
        private bool eventsBound = false;

        /// <summary>
        /// Info about project - for saving, loading
        /// </summary>
        internal CProjectInfo projectInfo;

        /// <summary>
        /// Url of edited file
        /// </summary>
        public String url;

        /// <summary>
        /// Active Project content 
        /// - sets/gets data in dependency of texteditor/wysiwygeditor visibility
        /// </summary>
        public String activeProjectContent
        {
            set
            {
                // Parse preview only if browser is shown
                if (this.tabControl1.SelectedTab == browserTabPage)
                {
                    string html = CXMLParser.Instance.GetPreviewFromProjectXML(value);
                    htmlEditor1.LoadDocument("<body>" + html + "</body>");
                }
                this.projectInfo.projectXml = value;
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
                        return CXMLParser.Instance.GetProjectXMLFromPreview(htmlEditor1.HtmlDocument2.GetBody().innerHTML);
                    }
                    catch (Exception)
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

        /// <summary>
        /// Constructor
        /// </summary>
        public CanvasTabPage()
        {
            InitializeComponent();
            htmlEditor1.ReadyStateChanged += new ReadyStateChangedHandler(htmlEditor1_ReadyStateChanged);
            CFormController.Instance.languageBox.SelectedIndexChanged += new EventHandler(languageBox_SelectedIndexChanged);
        }

        /// <summary>
        /// Gets string representation of selected tab
        /// </summary>
        /// <returns>Name of tab</returns>
        public String getSelectedTab()
        {
            if (this.tabControl1.SelectedTab == browserTabPage)
                return "browser";
            if (this.tabControl1.SelectedTab == textEditorTabPage)
                return "editor";
            else return String.Empty;
        }

        #region Event callbacks

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
                try
                {
                    HtmlAgilityPack.HtmlDocument doc = HTMLDocumentConverter.mshtmlDocToAgilityPackDoc(htmlEditor1.HtmlDocument2);
                    HtmlAgilityPack.HtmlNode elem = doc.GetElementbyId(e.element.id);
                    CFormController.Instance.mainForm.propertiesForm.moduleChanged += new ModuleChanged(propertiesForm_moduleChanged);
                    CFormController.Instance.mainForm.showProperties(elem);
                }
                catch (Exception)
                {
                    // failed to show properties
                }
            }
        }

        void propertiesForm_moduleChanged(object sender, EventArgs e)
        {
            // Get edited module node
            HtmlAgilityPack.HtmlNode activeNode = CFormController.Instance.mainForm.propertiesForm.activeElem;

            // Change its content
            activeNode.InnerHtml = CXMLParser.Instance.GetNodeFromModule(CFormController.Instance.mainForm.propertiesForm.module).OuterHtml;
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
                    input += CXMLParser.Instance.GetPreviewFromProjectXML(CXMLParser.Instance.GetNodeFromModule(CModuleReader.Instance.GetModuleInstanceFromName(listViewItemModule.Text)).OuterHtml);
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
                    input += CXMLParser.Instance.GetNodeFromModule(CModuleReader.Instance.GetModuleInstanceFromName(listViewItemModule.Text)).OuterHtml;
                }

                // And insert them on cursor position
                int cursorPosition = textBox1.SelectionStart;
                this.activeProjectContent = this.activeProjectContent.Insert(textBox1.SelectionStart, input);
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
            this.activeProjectContent = this.textBox1.Text;
        }

        private void textBox1_VisibleChanged(object sender, EventArgs e)
        {
            this.activeProjectContent = CXMLParser.Instance.GetProjectXMLFromPreview(htmlEditor1.HtmlDocument2.GetBody().innerHTML);
            // Properties not clickable from editmode .. yet
            CFormController.Instance.mainForm.hideProperties();
        }

        private void viewSource_Click(object sender, EventArgs e)
        {
            ShowSourceForm showSource = new ShowSourceForm();
            showSource.textBox1.Text = htmlEditor1.GetDocumentSource();
            showSource.ShowDialog();
        }

        private void toggleEditMode_Click(object sender, EventArgs e)
        {
            // Need to invoke setter to refresh content
            // (editmode sets content to null automatically)
            this.activeProjectContent = this.activeProjectContent;

            // Toggle design mode
            this.htmlEditor1.IsDesignMode = !this.htmlEditor1.IsDesignMode;

            // And invoke getter
            this.activeProjectContent = this.activeProjectContent;
            this.htmlEditor1.InvokeReadyStateChanged("complete");
        }

        /// <summary>
        /// Fires when box with languages changed - needs to check if there are conflict
        /// modules, ask user if he really wants to change language and if so, 
        /// remove conflict modules.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void languageBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Change project only if language is actually changed
            bool langChanged = (this.projectInfo.languageID != ((CLanguageInfo)CFormController.Instance.languageBox.SelectedItem).Value);
            // And its ment to be for this project - tab is active
            bool thisSelected = (CFormController.Instance.mainTabControl.SelectedTab == this);

            if (thisSelected && langChanged)
            {
                // User accepted lang change
                ToolStripComboBox langBox = (ToolStripComboBox)sender;


                String oldContent = this.activeProjectContent;
                // Parse out modules not supported in new language
                String oldLanguage = this.projectInfo.languageID;
                String newLanguage = ((CLanguageInfo)langBox.SelectedItem).Value;
                String newContent = CXMLParser.Instance.ChangeProjectLanguage(this.activeProjectContent, newLanguage);

                // Nothing to worry about, we can change language
                if (newContent.Equals(oldContent))
                {
                    this.projectInfo.languageID = newLanguage;
                    return;
                }

                bool userAccepted = (MessageBox.Show("Really change? (Some modules might be lost)", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes);
                if (userAccepted)
                {
                    this.activeProjectContent = newContent;
                    this.projectInfo.languageID = newLanguage;
                }
                else
                {
                    // User declined lang change, set language back to project language
                    foreach (CLanguageInfo language in CFormController.Instance.languageBox.Items)
                    {
                        if (language.Value.Equals(this.projectInfo.languageID))
                            CFormController.Instance.languageBox.SelectedItem = language;
                    }
                }
            }
        }

        #endregion
    }
}
