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
using Core.Helpers;
using System.Collections;
using System.Reflection;
using System.IO;
using System.Threading;
using System.Windows.Threading;
using System.Diagnostics;
using onlyconnect;
using Frontend.Helpers;

namespace Frontend.Forms
{
    /// <summary>
    /// Delegate for moduleChanged event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void ModuleChanged(object sender, EventArgs e);

    /// <summary>
    /// Form on the right side of main window, shows all properties of a module
    /// </summary>
    public partial class PropertiesForm : Form
    {
        /// <summary>
        /// Clicked module element
        /// </summary>
        public HtmlAgilityPack.HtmlNode activeElem;
        /// <summary>
        /// All generated textboxes
        /// </summary>
        private List<TextBox> textboxList = new List<TextBox>();
        /// <summary>
        /// Clicked module
        /// </summary>
        public AModule module;
        /// <summary>
        /// Event firing when properties changed - all of them must be okay, 
        /// if one throws exception, none will be saved
        /// </summary>
        public event ModuleChanged moduleChanged;

        /// <summary>
        /// Construct
        /// </summary>
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
            // Save element (<div class='modulecontainer'><module ....)
            this.activeElem = element;

            // Create module
            module = CXMLParser.Instance.GetModuleFromNode(element.FirstChild);

            // Clear Properties
            tableLayoutPanel1.Controls.Clear();
            tableLayoutPanel1.RowStyles.Clear();
            textboxList.Clear();

            // Create row for each setup_ property in moduleSetup class
            int row = 0;
            foreach (HtmlAgilityPack.HtmlAttribute attribute in element.FirstChild.Attributes)
            {
                if (attribute.Name.Length > 6 && attribute.Name.StartsWith("setup_"))
                {

                    tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));

                    Label label = new Label();
                    label.Text = attribute.Name.Substring(6);
                    tableLayoutPanel1.Controls.Add(label, 0, row);

                    TextBox textbox = new TextBox();
                    textbox.Width = 200;
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
                // Modify active element's setup
                foreach (TextBox tb in textboxList)
                {
                    // Create setup property
                    Object[] args = new Object[] { tb.Text };
                    try
                    {
                        module.setup.GetType().InvokeMember(tb.Name, BindingFlags.Public | BindingFlags.Instance | BindingFlags.SetProperty, null, module.setup, args);
                    }
                    catch (TargetInvocationException ext)
                    {
                        // Module sent exception, show message, 
                        // but do not close properties or fire changed event
                        // - everything must be just fine
                        MessageBox.Show(ext.InnerException.Message, "Invalid property");
                        return;
                    }
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

        /// <summary>
        /// Disabling moving with this window
        /// </summary>
        /// <param name="message"></param>
        protected override void WndProc(ref Message message)
        {
            const int WM_SYSCOMMAND = 0x0112;
            const int SC_MOVE = 0xF010;

            switch (message.Msg)
            {
                case WM_SYSCOMMAND:
                    int command = message.WParam.ToInt32() & 0xfff0;
                    if (command == SC_MOVE)
                    {
                        this.Focus();
                        return;
                    }
                    break;
            }

            base.WndProc(ref message);
        }
    }
}
