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
using Frontend.UserControls;
using System.Collections;
using System.Reflection;
using System.IO;
using Frontend.Helpers;

namespace Frontend.Forms
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// programatically created toolboxForm
        /// </summary>
        private ToolBoxForm toolboxForm;
        public PropertiesForm propertiesForm;

        public MainForm()
        {
            InitializeComponent();
            CFormController.Instance.mainForm = this;

            InitLanguageBox();
            InitToolbox();
            InitProperties();

            CModuleReader.Instance.ModulesReloadedEvent += new ModulesReloadedHandler(reloadLanguageBox);
        }

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

        private void InitLanguageBox()
        {
            CFormController.Instance.languageBox = langSelectBox;
            CLanguageInfo emptyItem = CLanguageInfoFactory.getLangItem("empty");
            langSelectBox.Items.Add(emptyItem);
            foreach (String lang in CModuleReader.Instance.languages)
            {
                //new item
                langSelectBox.Items.Add(CLanguageInfoFactory.getLangItem(lang));
            }
            langSelectBox.SelectedIndex = 0;
        }

        private void InitToolbox()
        {
            toolboxForm = new ToolBoxForm();
            toolboxForm.MdiParent = this;
            toolboxForm.loadModules(CLanguageInfoFactory.getLangItem("empty"));
            toolboxForm.Dock = DockStyle.Fill;
            splitContainer1.Panel1.Controls.Add(toolboxForm);
            toolboxForm.Show();
            hideToolBox();
        }

        private void InitProperties()
        {
            propertiesForm = new PropertiesForm();
            propertiesForm.MdiParent = this;
            propertiesForm.Dock = DockStyle.Fill;
            splitContainer2.Panel2.Controls.Add(propertiesForm);
            propertiesForm.Show();
            hideProperties();
        }

        private void ShowNewForm(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = CFileDialogFactory.createNewFileDialog();
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                // Get variables needed for canvastabpage
                string fileURL = saveFileDialog.FileName;

                // New form - new info
                CProjectInfo projectInfo = new CProjectInfo();
                projectInfo.languageID = ((CLanguageInfo)langSelectBox.SelectedItem).Value;

                // Create tabpage from them
                CanvasTabPage tabPage = CCanvasTabPageFactory.createNewPage(projectInfo, fileURL);

                // And add it to tabControl
                tabControl1.TabPages.Add(tabPage);
                tabControl1.SelectedTab = tabPage;

                // Save it also
                CFileHelper.saveProject(tabPage);

                showToolBox();
            }
        }

        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = CFileDialogFactory.createOpenFileDialog();
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                // Get variables needed for canvastabpage
                string fileURL = openFileDialog.FileName;

                // Get file content
                String projectContent = CFileHelper.readProject(fileURL);

                // Create tabpage from them
                CanvasTabPage tabPage = CCanvasTabPageFactory.createPageFromFile(projectContent, fileURL);

                // And add it to tabControl
                tabControl1.TabPages.Add(tabPage);
                tabControl1.SelectedTab = tabPage;

                this.showToolBox();
            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Get active child and update its properties
            CanvasTabPage activeChild = (CanvasTabPage)tabControl1.SelectedTab;

            if (activeChild == null)
                // Nothing to save as
                return;

            // Invoke setter to get projectinfo if in view mode
            activeChild.XMLProjectContent = activeChild.XMLProjectContent;

            SaveFileDialog saveFileDialog = CFileDialogFactory.createSaveFileDialog();
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                // Get variables needed for canvastabpage
                activeChild.url = saveFileDialog.FileName;
                activeChild.Text = System.IO.Path.GetFileName(activeChild.url);

                // And save it
                CFileHelper.saveProject(activeChild);
            }
        }

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            CanvasTabPage activeChild = (CanvasTabPage)tabControl1.SelectedTab;

            if (activeChild == null)
                // Nothing to save
                return;

            CFileHelper.saveProject(activeChild);
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.saveToolStripButton_Click(sender, e);
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CanvasTabPage activeChild = (CanvasTabPage)tabControl1.SelectedTab;
            if (activeChild == null)
                return;
            if (activeChild.getSelectedTab().Equals("browser"))
            {
                activeChild.htmlEditor1.HtmlDocument2.ExecCommand("cut", false, null);
            }
            else if (activeChild.getSelectedTab().Equals("editor"))
            {
                activeChild.textBox1.Cut();
            }
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CanvasTabPage activeChild = (CanvasTabPage)tabControl1.SelectedTab;
            if (activeChild == null)
                return;
            if (activeChild.getSelectedTab().Equals("browser"))
            {
                activeChild.htmlEditor1.HtmlDocument2.ExecCommand("copy", false, null);
            }
            else if (activeChild.getSelectedTab().Equals("editor"))
            {
                activeChild.textBox1.Copy();
            }
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CanvasTabPage activeChild = (CanvasTabPage)tabControl1.SelectedTab;
            if (activeChild == null)
                return;
            if (activeChild.getSelectedTab().Equals("browser"))
            {
                activeChild.htmlEditor1.HtmlDocument2.ExecCommand("paste", false, null);
            }
            else if (activeChild.getSelectedTab().Equals("editor"))
            {
                activeChild.textBox1.Paste();
            }
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CanvasTabPage activeChild = (CanvasTabPage)tabControl1.SelectedTab;
            if (activeChild == null)
                return;
            if (activeChild.getSelectedTab().Equals("browser"))
            {
                activeChild.htmlEditor1.HtmlDocument2.ExecCommand("undo", false, null);
            }
            else if (activeChild.getSelectedTab().Equals("editor"))
            {
                activeChild.textBox1.Undo();
            }
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CanvasTabPage activeChild = (CanvasTabPage)tabControl1.SelectedTab;
            if (activeChild == null)
                return;
            if (activeChild.getSelectedTab().Equals("browser"))
            {
                activeChild.htmlEditor1.HtmlDocument2.ExecCommand("undo", false, null);
            }
            else if (activeChild.getSelectedTab().Equals("editor"))
            {
                activeChild.textBox1.Undo();
            }
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CanvasTabPage activeChild = (CanvasTabPage)tabControl1.SelectedTab;
            if (activeChild == null)
                return;
            if (activeChild.getSelectedTab().Equals("browser"))
            {
                activeChild.htmlEditor1.HtmlDocument2.ExecCommand("selectall", false, null);
            }
            else if (activeChild.getSelectedTab().Equals("editor"))
            {
                activeChild.textBox1.SelectAll();
            }
        }

        private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStrip.Visible = toolBarToolStripMenuItem.Checked;
        }

        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            statusStrip.Visible = statusBarToolStripMenuItem.Checked;
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.TabPages.Clear();
            hideToolBox();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm aboutForm = new AboutForm();
            aboutForm.ShowDialog();
        }

        private void toolBoxMenuItem_Click(object sender, EventArgs e)
        {
            if (toolBoxMenuItem.Checked)
            {
                showToolBox();
            }
            else
            {
                hideToolBox();
            }
        }

        private void propertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (propertiesMenuItem.Checked)
            {
                showProperties();
            }
            else
            {
                hideProperties();
            }
        }

        internal void showToolBox()
        {
            toolBoxMenuItem.Checked = true;
            splitContainer1.Panel1Collapsed = false;
            splitContainer1.Panel1.Show();
        }

        internal void hideToolBox()
        {
            toolBoxMenuItem.Checked = false;
            splitContainer1.Panel1Collapsed = true;
            splitContainer1.Panel1.Hide();
        }

        internal void showProperties(HtmlAgilityPack.HtmlNode element = null)
        {
            propertiesMenuItem.Checked = true;
            splitContainer2.Panel2Collapsed = false;
            splitContainer2.Panel2.Show();

            if (element != null)
            {
                propertiesForm.SetContent(element);
            }

        }

        internal void hideProperties()
        {
            propertiesMenuItem.Checked = false;
            splitContainer2.Panel2Collapsed = true;
            splitContainer2.Panel2.Hide();
        }

        internal void setStatus(String status)
        {
            this.toolStripStatusLabel.Text = status;
        }

        private void boldButton_Click(object sender, EventArgs e)
        {
            CanvasTabPage activeChild = (CanvasTabPage)tabControl1.SelectedTab;
            if (activeChild == null)
                return;

            if (activeChild.getSelectedTab().Equals("browser"))
            {
                activeChild.htmlEditor1.HtmlDocument2.ExecCommand("bold", false, null);
            }
        }

        private void underlinedButton_Click(object sender, EventArgs e)
        {
            CanvasTabPage activeChild = (CanvasTabPage)tabControl1.SelectedTab;
            if (activeChild == null)
                return;

            if (activeChild.getSelectedTab().Equals("browser"))
            {
                activeChild.htmlEditor1.HtmlDocument2.ExecCommand("underline", false, null);
            }
        }

        private void centerButton_Click(object sender, EventArgs e)
        {
            CanvasTabPage activeChild = (CanvasTabPage)tabControl1.SelectedTab;
            if (activeChild == null)
                return;

            if (activeChild.getSelectedTab().Equals("browser"))
            {
                activeChild.htmlEditor1.HtmlDocument2.ExecCommand("justifycenter", false, null);
            }
        }

        private void italicButton_Click(object sender, EventArgs e)
        {
            CanvasTabPage activeChild = (CanvasTabPage)tabControl1.SelectedTab;
            if (activeChild == null)
                return;

            if (activeChild.getSelectedTab().Equals("browser"))
            {
                activeChild.htmlEditor1.HtmlDocument2.ExecCommand("italic", false, null);
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.hideProperties();
        }

        private void generateCodeButton_Click(object sender, EventArgs e)
        {
            CanvasTabPage activeChild = (CanvasTabPage)tabControl1.SelectedTab;
            if (activeChild == null)
                return;

            GenerateCodeForm gcf = new GenerateCodeForm(activeChild);
            gcf.Show();
        }
    }
}
