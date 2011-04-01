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
using mshtml;
using System.Runtime.InteropServices;

namespace Frontend.UserControls
{
    public partial class CanvasTabPage : TabPage
    {
        private IHTMLDocument2 doc;
        private bool previewSucceeded = true;

        public CanvasTabPage()
        {
            InitializeComponent();

            // Init wysiwyg mode
            previewWebBrowser1.DocumentText = "init";
            //previewWebBrowser1.
            doc = (IHTMLDocument2)previewWebBrowser1.Document.DomDocument;

            //IHTMLEditServices editservices = (IHTMLEditServices)previewWebBrowser1;


            //IHTMLEditDesigner designer = new CEditDesigner();
            //IHTMLEditServices es = (IHTMLEditServices)Marshal.GetObjectForIUnknown((IntPtr)previewWebBrowser1.GetService(typeof(PreviewWebBrowser)));
            //es.AddDesigner(designer);
            //previewWebBrowser1.GetService(typeof
            //return;
            //IServiceProvider isp = (IServiceProvider)doc;
            //IHTMLEditServices es;
            //System.Guid IHtmlEditServicesGuid = new System.Guid("3050f663-98b5-11cf-bb82-00aa00bdce0b");
            //System.Guid SHtmlEditServicesGuid = new System.Guid(0x3050f7f9, 0x98b5, 0x11cf, 0xbb, 0x82, 0x00, 0xaa, 0x00, 0xbd, 0xce, 0x0b);
            //IntPtr ppv;
            //IHTMLEditDesigner designer = new EditDesigner();
            //if (isp != null)
            //{
            //    isp.QueryService(ref SHtmlEditServicesGuid, ref IHtmlEditServicesGuid, out ppv);
            //    es = (IHTMLEditServices)Marshal.GetObjectForIUnknown(ppv);
            //    int retval = es.AddDesigner(ds);
            //    Marshal.Release(ppv);
            //}

            //editservices = \ 
            //doc.que
            //IHTMLEditServices
            //editservices.AddDesigner(designer);
            
        }

        private void eventer(object sender, EventArgs args)
        {
        }

        public String content
        {
            set
            {
                textBox1.Text = value;
            }
            get
            {
                return textBox1.Text;
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

        public void RefreshBrowser()
        {
            previewWebBrowser1.Refresh();
        }

        private void textBox1_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("System.Windows.Forms.ListView+SelectedListViewItemCollection", false))
            {
                String input = String.Empty;

                ListView.SelectedListViewItemCollection listViewItemModules = (ListView.SelectedListViewItemCollection)e.Data.GetData("System.Windows.Forms.ListView+SelectedListViewItemCollection", false);
                foreach (ListViewItem listViewItemModule in listViewItemModules)
                {
                    input += CXMLParser.Instance.getNodeFromModule(CModuleReader.Instance.GetModuleInstanceFromName(listViewItemModule.Text)).OuterXml;
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

        private void webBrowser1_VisibleChanged(object sender, EventArgs e)
        {
            try
            {
                String output = CXMLParser.Instance.getPreviewFromProjectXML(this.content);
                //doc.designMode = "On";
                doc.write(output);
                doc.close();
                doc.designMode = "On";
                doc.ondragstart = "x";
                previewSucceeded = true;
            }
            catch (System.Xml.XmlException exc)
            {
                doc.write(exc.Message);
                doc.close();
                previewSucceeded = false;
            }
            return;
        }

        private void textBox1_VisibleChanged(object sender, EventArgs e)
        {
            if (previewSucceeded)
            {
                this.content = CXMLParser.Instance.getProjectXMLFromPreview(doc.body.innerHTML);// .innerHTML;
            }
        }

    }
}
