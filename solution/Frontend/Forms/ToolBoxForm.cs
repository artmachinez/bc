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
using Core.Project;
using Core.Helpers;
using System.Collections;
using System.Reflection;
using System.IO;
using System.Threading;
using System.Windows.Threading;
using Frontend.Helpers;

namespace Frontend.Forms
{
    /// <summary>
    /// Form on the left side of program window, shows all the loaded modules
    /// </summary>
    public partial class ToolBoxForm : Form
    {
        public ToolBoxForm()
        {
            InitializeComponent();
            InitLanguageBox();
            CFormController.Instance.toolbox = this;
            CModuleReader.Instance.ModulesReloadedEvent += new ModulesReloadedHandler(reloadModules);
            CModuleReader.Instance.ModulesReloadedEvent += new ModulesReloadedHandler(reloadLanguageBox);
            CFormController.Instance.languageBox.SelectedIndexChanged += new EventHandler(reloadModules);
        }

        /// <summary>
        /// Add all languages to languagebox
        /// </summary>
        private void InitLanguageBox()
        {
            CFormController.Instance.languageBox = this.langSelectBox;
            CLanguageInfo emptyItem = CLanguageInfoFactory.getLangItem("empty");
            this.langSelectBox.Items.Add(emptyItem);
            foreach (String lang in CModuleReader.Instance.languages)
            {
                //new item
                this.langSelectBox.Items.Add(CLanguageInfoFactory.getLangItem(lang));
            }
            this.langSelectBox.SelectedIndex = 0;
        }

        /// <summary>
        /// Reload languagebox - some modules have been added or something
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void reloadLanguageBox(object sender, EventArgs e)
        {
            langSelectBox.Items.Clear();
            CLanguageInfo emptyItem = CLanguageInfoFactory.getLangItem("empty");
            langSelectBox.Items.Add(emptyItem);
            foreach (String lang in CModuleReader.Instance.languages)
            {
                langSelectBox.Items.Add(CLanguageInfoFactory.getLangItem(lang));
            }
            langSelectBox.SelectedIndex = 0;
        }

        /// <summary>
        /// Event handler for reloading languages / assemblies in modules directory
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void reloadModules(object sender, EventArgs e)
        {
            this.loadModules((CLanguageInfo)CFormController.Instance.languageBox.SelectedItem);
        }

        /// <summary>
        /// Loads modules to listview
        /// </summary>
        /// <param name="language"></param>
        public void loadModules(CLanguageInfo language)
        {
            List<Type> modules = new List<Type>();
            try
            {
                if (language.Value.Equals("empty"))
                {
                    modules.AddRange(CModuleReader.Instance.langToModulesMap[String.Empty]);
                }
                else
                {
                    modules.AddRange(CModuleReader.Instance.langToModulesMap[language.Value]);
                    modules.AddRange(CModuleReader.Instance.langToModulesMap[String.Empty]);
                }
            }
            catch (KeyNotFoundException)
            {
                CFormController.Instance.mainForm.setStatus("no WS modules found: ");
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

        /// <summary>
        /// Not really closing, just hiding
        /// </summary>
        /// <param name="e"></param>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            CFormController.Instance.mainForm.hideToolBox();
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

        /// <summary>
        /// Code from http://support.microsoft.com/kb/822483 for enabling drag'n'drop for listview at runtime
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
