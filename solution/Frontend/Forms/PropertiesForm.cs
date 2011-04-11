using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Core;
using Core.Modules;
using System.Collections;
using System.Reflection;
using System.IO;
using System.Threading;
using System.Windows.Threading;
using System.Diagnostics;
using onlyconnect;

namespace Frontend.Forms
{
    public delegate void ModuleChanged(object sender, EventArgs e);

    public partial class PropertiesForm : Form
    {
        public HtmlAgilityPack.HtmlNode activeElem;
        private List<TextBox> textboxList = new List<TextBox>();
        public AModule module;
        public event ModuleChanged moduleChanged;

        public PropertiesForm()
        {
            InitializeComponent();
            CFormController.Instance.propertyBox = this;
        }

        /// <summary>
        /// Creates textboxes and shit
        /// </summary>
        /// <param name="element">Module element</param>
        /// <returns></returns>
        public bool SetContent(HtmlAgilityPack.HtmlNode element)
        {
            this.activeElem = element;

            module = CXMLParser.Instance.getModuleFromNode(element.FirstChild);

            tableLayoutPanel1.Controls.Clear();
            tableLayoutPanel1.RowStyles.Clear();
            textboxList.Clear();
            int row = 0;
            foreach (HtmlAgilityPack.HtmlAttribute attribute in element.FirstChild.Attributes)
            {
                if (attribute.Name.Length > 6 && attribute.Name.Substring(0, 6).Equals("setup_"))
                {

                    tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));

                    Label label = new Label();
                    label.Text = attribute.Name;
                    tableLayoutPanel1.Controls.Add(label, 0, row);

                    TextBox textbox = new TextBox();
                    textbox.Name = attribute.Name;
                    textbox.Text = attribute.Value;
                    textbox.KeyDown += new KeyEventHandler(textbox_KeyDown);
                    textboxList.Add(textbox);
                    tableLayoutPanel1.Controls.Add(textbox, 1, row++);
                }
            }

            this.Text = element.FirstChild.Attributes["name"].Value + " properties";
            return true;
        }

        void textbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // Modify active element
                foreach (TextBox tb in textboxList)
                {
                    Object[] args = new Object[] { tb.Text };
                    module.setup.GetType().InvokeMember(tb.Name, BindingFlags.Public | BindingFlags.Instance | BindingFlags.SetProperty, null, module.setup, args);
                }

                // Fire event
                this.moduleChanged(null, null);

                CFormController.Instance.mainForm.hideProperties();
            }
        }

        /// <summary>
        /// Not really closing, just hiding
        /// </summary>
        /// <param name="e"></param>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            CFormController.Instance.mainForm.hideProperties();
            e.Cancel = true;
        }

        ///// <summary>
        ///// Disabling moving with this window
        ///// </summary>
        ///// <param name="message"></param>
        //protected override void WndProc(ref Message message)
        //{
        //    const int WM_SYSCOMMAND = 0x0112;
        //    const int SC_MOVE = 0xF010;

        //    switch (message.Msg)
        //    {
        //        case WM_SYSCOMMAND:
        //            int command = message.WParam.ToInt32() & 0xfff0;
        //            if (command == SC_MOVE)
        //            {
        //                this.Focus();
        //                return;
        //            }
        //            break;
        //    }

        //    base.WndProc(ref message);
        //}
    }
}
