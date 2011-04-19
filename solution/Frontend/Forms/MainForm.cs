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
        /// Programatically created toolbox
        /// </summary>
        private ToolBoxForm toolboxForm;
        /// <summary>
        /// Programatically created properties
        /// </summary>
        public PropertiesForm propertiesForm;

        /// <summary>
        /// Constructor
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            CFormController.Instance.mainForm = this;
            InitToolbox();
            InitProperties();
        }

        /// <summary>
        /// Changes text of status strip in the bottom of form
        /// </summary>
        /// <param name="status"></param>
        internal void setStatus(String status)
        {
            this.toolStripStatusLabel.Text = status;
        }

        #region File manipulation callbacks

        private void ShowNewForm(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = CFileDialogFactory.createNewFileDialog();
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                // Get variables needed for canvastabpage
                string fileURL = saveFileDialog.FileName;

                // New form - new info
                CProjectInfo projectInfo = new CProjectInfo();
                projectInfo.languageID = ((CLanguageInfo)CFormController.Instance.languageBox.SelectedItem).Value;

                // Create tabpage from them
                CanvasTabPage tabPage = CCanvasTabPageFactory.createNewPage(projectInfo, fileURL);

                // And add it to tabControl
                pageContainerControl.TabPages.Add(tabPage);
                pageContainerControl.SelectedTab = tabPage;

                // Manually cast selectindexchanged, since it doesnt fire when the first tab
                // is opened
                this.pageContainerControl_SelectedIndexChanged(null, null);

                // Save it also
                CFileHelper.saveProject(tabPage.projectInfo, tabPage.url);

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

                CProjectInfo info = CFileHelper.getProject(fileURL);

                if (info == null)
                {
                    MessageBox.Show("Unable to open project", "Error in opening file");
                    return;
                }

                CanvasTabPage tabPage = CCanvasTabPageFactory.createPage(CFileHelper.getProject(fileURL), fileURL);

                // And add it to tabControl
                pageContainerControl.TabPages.Add(tabPage);
                pageContainerControl.SelectedTab = tabPage;

                // Manually cast selectindexchanged, since it doesnt fire when the first tab
                // is opened
                this.pageContainerControl_SelectedIndexChanged(null, null);

                this.showToolBox();
            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Get active child and update its properties
            CanvasTabPage activeChild = (CanvasTabPage)pageContainerControl.SelectedTab;

            if (activeChild == null)
                // Nothing to save as
                return;

            // Invoke setter to get projectinfo if in view mode
            activeChild.projectInfo.projectXml = activeChild.activeProjectContent;

            SaveFileDialog saveFileDialog = CFileDialogFactory.createSaveFileDialog();
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                // Get variables needed for canvastabpage
                activeChild.url = saveFileDialog.FileName;
                activeChild.Text = System.IO.Path.GetFileName(activeChild.url);

                // And save it
                CFileHelper.saveProject(activeChild.projectInfo, activeChild.url);
            }
        }

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            CanvasTabPage activeChild = (CanvasTabPage)pageContainerControl.SelectedTab;

            if (activeChild == null)
                // Nothing to save
                return;

            // Invoke setter to get projectinfo if in view mode
            activeChild.projectInfo.projectXml = activeChild.activeProjectContent;

            CFileHelper.saveProject(activeChild.projectInfo, activeChild.url);
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.saveToolStripButton_Click(sender, e);
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region Text edit callbacks

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CanvasTabPage activeChild = (CanvasTabPage)pageContainerControl.SelectedTab;
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
            CanvasTabPage activeChild = (CanvasTabPage)pageContainerControl.SelectedTab;
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
            CanvasTabPage activeChild = (CanvasTabPage)pageContainerControl.SelectedTab;
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
            CanvasTabPage activeChild = (CanvasTabPage)pageContainerControl.SelectedTab;
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
            CanvasTabPage activeChild = (CanvasTabPage)pageContainerControl.SelectedTab;
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
            CanvasTabPage activeChild = (CanvasTabPage)pageContainerControl.SelectedTab;
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

        private void boldButton_Click(object sender, EventArgs e)
        {
            CanvasTabPage activeChild = (CanvasTabPage)pageContainerControl.SelectedTab;
            if (activeChild == null)
                return;

            if (activeChild.getSelectedTab().Equals("browser"))
            {
                activeChild.htmlEditor1.HtmlDocument2.ExecCommand("bold", false, null);
            }
        }

        private void underlinedButton_Click(object sender, EventArgs e)
        {
            CanvasTabPage activeChild = (CanvasTabPage)pageContainerControl.SelectedTab;
            if (activeChild == null)
                return;

            if (activeChild.getSelectedTab().Equals("browser"))
            {
                activeChild.htmlEditor1.HtmlDocument2.ExecCommand("underline", false, null);
            }
        }

        private void centerButton_Click(object sender, EventArgs e)
        {
            CanvasTabPage activeChild = (CanvasTabPage)pageContainerControl.SelectedTab;
            if (activeChild == null)
                return;

            if (activeChild.getSelectedTab().Equals("browser"))
            {
                activeChild.htmlEditor1.HtmlDocument2.ExecCommand("justifycenter", false, null);
            }
        }

        private void italicButton_Click(object sender, EventArgs e)
        {
            CanvasTabPage activeChild = (CanvasTabPage)pageContainerControl.SelectedTab;
            if (activeChild == null)
                return;

            if (activeChild.getSelectedTab().Equals("browser"))
            {
                activeChild.htmlEditor1.HtmlDocument2.ExecCommand("italic", false, null);
            }
        }

        private void centerButton_Click_1(object sender, EventArgs e)
        {
            CanvasTabPage activeChild = (CanvasTabPage)pageContainerControl.SelectedTab;
            if (activeChild == null)
                return;

            if (activeChild.getSelectedTab().Equals("browser"))
            {
                activeChild.htmlEditor1.HtmlDocument2.ExecCommand("center", false, null);
            }
        }

        #endregion

        #region Show/hide utilities callbacks

        private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStrip.Visible = toolBarToolStripMenuItem.Checked;
        }

        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            statusStrip.Visible = statusBarToolStripMenuItem.Checked;
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pageContainerControl.TabPages.Clear();
            hideToolBox();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm aboutForm = new AboutForm();
            aboutForm.ShowDialog();
        }

        private void generateCodeButton_Click(object sender, EventArgs e)
        {
            CanvasTabPage activeChild = (CanvasTabPage)pageContainerControl.SelectedTab;
            if (activeChild == null)
                return;

            GenerateCodeForm gcf = new GenerateCodeForm(activeChild);
            gcf.Show();
        }

        #endregion

        #region Toolbox manipulation

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

        #endregion

        #region Properties manipulation

        private void InitProperties()
        {
            propertiesForm = new PropertiesForm();
            propertiesForm.MdiParent = this;
            propertiesForm.Dock = DockStyle.Fill;
            splitContainer2.Panel2.Controls.Add(propertiesForm);
            propertiesForm.Show();
            hideProperties();
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

        #endregion

        #region Other callbacks

        /// <summary>
        /// Changing language in languagebox with tabpage change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pageContainerControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Get active child and update its properties
            CanvasTabPage activeChild = (CanvasTabPage)pageContainerControl.SelectedTab;

            if (activeChild == null)
                return;

            //CFormController.Instance.languageBox.SelectedItem = activeChild.
            foreach (CLanguageInfo language in CFormController.Instance.languageBox.Items)
            {
                if (language.Value.Equals(activeChild.projectInfo.languageID))
                    CFormController.Instance.languageBox.SelectedItem = language;
            }
        }

        #endregion

    }
}
