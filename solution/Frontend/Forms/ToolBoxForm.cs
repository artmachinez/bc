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

namespace Frontend.Forms
{
    public partial class ToolBoxForm : Form
    {
        public ToolBoxForm()
        {
            InitializeComponent();
            CFormController.Instance.toolbox = this;
            CModuleReader.Instance.ModulesReloadedEvent += new ModulesReloadedHandler(reloadModules);
            CFormController.Instance.languageBox.SelectedIndexChanged += new EventHandler(reloadModules);
        }

        private void reloadModules(object sender, EventArgs e)
        {
            this.loadModules(CFormController.Instance.languageBox.SelectedItem.ToString());
        }

        public void loadModules(String language)
        {
            List<Type> modules = new List<Type>();
            try
            {
                modules.AddRange(CModuleReader.Instance.langToModulesMap[language]);
                if (language != String.Empty)
                {
                    modules.AddRange(CModuleReader.Instance.langToModulesMap[String.Empty]);
                }
            }
            catch (KeyNotFoundException e)
            {
                CFormController.Instance.mainForm.setStatus("no WS modules found");
            }

            listView1.Groups.Clear();
            listView1.Items.Clear();
            foreach (Type moduleType in modules)
            {
                String groupName = moduleType.GetField("group").GetValue(null).ToString();
                listView1.Groups.Add(groupName, groupName);

                String moduleName = moduleType.GetField("name").GetValue(null).ToString();
                ListViewItem newItem = new ListViewItem(moduleName);
                newItem.Group = listView1.Groups[groupName];

                listView1.Items.Add(newItem);
            }
        }

        /**
         * not really closing, just hiding
         */
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            CFormController.Instance.mainForm.hideToolBox();
            e.Cancel = true;
        }

        /**
         * disabling moving this window
         */
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

        /**
         * code from http://support.microsoft.com/kb/822483 for enabling drag'n'drop for listview at runtime
         */
        private void listView1_ItemDrag(object sender, ItemDragEventArgs e)
        {
            listView1.DoDragDrop(listView1.SelectedItems, DragDropEffects.Copy);
        }
        private void listView1_DragEnter(object sender, DragEventArgs e)
        {
            int len = e.Data.GetFormats().Length - 1;
            int i;
            for (i = 0; i <= len; i++)
            {
                if (e.Data.GetFormats()[i].Equals("System.Windows.Forms.ListView+SelectedListViewItemCollection"))
                {
                    e.Effect = DragDropEffects.Copy;
                }
            }
        }
    }
}
