﻿using System;
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
using Frontend.Editor.HtmlEditor;

namespace Frontend.UserControls
{
    public partial class CanvasTabPage : TabPage
    {
        //private EditableWebBrowser.WebBrowser ewb;

        private HtmlEditor htmlEditor;

        private bool previewSucceeded = true;

        public CanvasTabPage()
        {
            InitializeComponent();

            this.htmlEditor = new HtmlEditor();
            this.htmlEditor.Dock = DockStyle.Fill;
            this.htmlEditor.VisibleChanged += new EventHandler(wb_VisibleChanged);
            onlyconnect.IHTMLEditDesigner designer = new Editor.CRestrictedEditDesigner();
            this.htmlEditor.SetEditDesigner(designer);
            this.htmlEditor.IsDesignMode = true;


            this.tabPage1.Controls.Add(htmlEditor);

            return;
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
            htmlEditor.Refresh();
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

        private void wb_VisibleChanged(object sender, EventArgs e)
        {
            try
            {
                String output = CXMLParser.Instance.getPreviewFromProjectXML(this.content);
                this.htmlEditor.LoadDocument(output);
                //this.htmlEditor.HtmlDocument2.SetDesignMode("On");
                previewSucceeded = true;
            }
            catch (System.Xml.XmlException exc)
            {
                this.htmlEditor.LoadDocument(exc.Message);
                previewSucceeded = false;
            }
            return;
        }

        private void textBox1_VisibleChanged(object sender, EventArgs e)
        {
            if (previewSucceeded)
            {
                this.content = CXMLParser.Instance.getProjectXMLFromPreview(htmlEditor.HtmlDocument2.GetBody().innerHTML); //this.ewb.doc2wb.body.innerHTML);// .innerHTML;
            }
        }

    }
}
