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
//using mshtml;
using System.Runtime.InteropServices;
using onlyconnect;

namespace Frontend.UserControls
{
    public partial class CanvasTabPage : TabPage
    {
        //private EditableWebBrowser.WebBrowser ewb;

        //private HtmlEditor htmlEditor;

        private bool previewSucceeded = true;

        public CanvasTabPage()
        {
            InitializeComponent();
            htmlEditor1.ReadyStateChanged += new ReadyStateChangedHandler(htmlEditor1_ReadyStateChanged);
            //htmlEditor1.theSite.dragEnter += new DragEnterHandler(theSite_dragEnter);
            //htmlEditor1.theSite.drop += new DropHandler(theSite_drop);
            //htmlEditor1.AllowDrop = true;
        }

        void theSite_drop(object sender, EventArgs e)
        {
            CFormController.Instance.mainForm.setStatus("drop");
        }

        void theSite_dragEnter(object sender, EventArgs e)
        {
            CFormController.Instance.mainForm.setStatus("dragEnter");
        }

        void htmlEditor1_ReadyStateChanged(object sender, ReadyStateChangedEventArgs e)
        {
            if (e.ReadyState == "complete")
            {
                htmlEditor1.SetEditDesigner(new HtmlEditorClasses.CRestrictedEditDesigner());
                htmlEditor1.theSite.dragEnter += new DragEnterHandler(theSite_dragEnter);
                htmlEditor1.theSite.drop += new DropHandler(theSite_drop);
                htmlEditor1.AllowDrop = true;
            }
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
                //previewWebBrowser1.Url = new Uri(value);
                this._url = value;
            }
            get
            {
                //return previewWebBrowser1.Url.LocalPath;
                return this._url;
            }
        }

        //public void RefreshBrowser()
        //{
        //    htmlEditor1.Refresh();
        //}

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
                this.content = CXMLParser.Instance.getProjectXMLFromPreview(htmlEditor1.sDocument);
            }
        }

    }
}
