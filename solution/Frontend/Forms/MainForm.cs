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
using Frontend.UserControls;
using System.Collections;
using System.Reflection;
using System.IO;

namespace Frontend.Forms
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// programatically created toolboxForm
        /// </summary>
        private ToolBoxForm toolbox;

        public MainForm()
        {
            InitializeComponent();
            CFormController.Instance.mainForm = this;
            InitLanguageBox();
            InitToolbox();
            CModuleReader.Instance.ModulesReloadedEvent += new ModulesReloadedHandler(reloadLanguageBox);
            InitProperties();
        }

        private void reloadLanguageBox(object sender, EventArgs e)
        {
            langSelectBox.Items.Clear();
            langSelectBox.Items.Add(String.Empty);
            foreach (String lang in CModuleReader.Instance.languages)
            {
                langSelectBox.Items.Add(lang);
            }
            langSelectBox.SelectedIndex = 0;
        }

        private void InitLanguageBox()
        {
            CFormController.Instance.languageBox = langSelectBox;
            langSelectBox.Items.Add(String.Empty);
            foreach (String lang in CModuleReader.Instance.languages)
            {
                langSelectBox.Items.Add(lang);
            }
            langSelectBox.SelectedIndex = 0;
        }

        private void InitToolbox()
        {
            toolbox = new ToolBoxForm();
            toolbox.MdiParent = this;
            toolbox.loadModules(String.Empty);
            toolbox.Dock = DockStyle.Fill;
            splitContainer1.Panel1.Controls.Add(toolbox);
            toolbox.Show();
            hideToolBox();
        }

        private void InitProperties()
        {
            hideProperties();
        }

        private void ShowNewForm(object sender, EventArgs e)
        {
            Stream fileStream;
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Project File (*." + Core.App.Default.projectExtension + ")|*." + Core.App.Default.projectExtension;
            saveFileDialog.Title = "Create New File";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                fileStream = saveFileDialog.OpenFile();
                if (fileStream != null)
                {
                    string fileURL = saveFileDialog.FileName;
                    string fileName = System.IO.Path.GetFileName(fileURL);

                    CanvasTabPage canvas = new CanvasTabPage();
                    canvas.content = Resources.new_file.ToString();
                    canvas.Text = fileName;
                    tabControl1.TabPages.Add(canvas);
                    tabControl1.SelectedTab = canvas;


                    StreamWriter sw = new StreamWriter(fileStream);
                    sw.Write(canvas.content);
                    sw.Close();
                    fileStream.Close();

                    canvas.url = fileURL;
                    showToolBox();
                }
            }
        }

        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "Project File (*." + Core.App.Default.projectExtension + ")|*." + Core.App.Default.projectExtension;
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string fileURL = openFileDialog.FileName;
                string fileName = System.IO.Path.GetFileName(fileURL);

                CanvasTabPage canvas = new CanvasTabPage();
                canvas.content = System.IO.File.ReadAllText(fileURL);
                canvas.Text = fileName;
                canvas.url = fileURL;
                tabControl1.TabPages.Add(canvas);
                tabControl1.SelectedTab = canvas;
                showToolBox();
            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stream fileStream;
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Project File (*." + Core.App.Default.projectExtension + ")|*." + Core.App.Default.projectExtension;
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                fileStream = saveFileDialog.OpenFile();
                if (fileStream != null)
                {
                    string fileURL = saveFileDialog.FileName;
                    string fileName = System.IO.Path.GetFileName(fileURL);

                    CanvasTabPage activeChild = (CanvasTabPage)tabControl1.SelectedTab;

                    StreamWriter sw = new StreamWriter(fileStream);
                    sw.Write(activeChild.content);
                    sw.Close();
                    fileStream.Close();

                    activeChild.Text = fileName;
                    activeChild.url = fileURL;
                }
            }
        }

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            CanvasTabPage activeChild = (CanvasTabPage)tabControl1.SelectedTab;
            System.IO.File.WriteAllText(@activeChild.url, activeChild.content);
            //activeChild.RefreshBrowser();
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
            if (activeChild != null)
                activeChild.textBox1.Cut();
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CanvasTabPage activeChild = (CanvasTabPage)tabControl1.SelectedTab;
            if(activeChild != null)
                activeChild.textBox1.Copy();
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CanvasTabPage activeChild = (CanvasTabPage)tabControl1.SelectedTab;
            if (activeChild != null)
                activeChild.textBox1.Paste();
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CanvasTabPage activeChild = (CanvasTabPage)tabControl1.SelectedTab;
            if (activeChild != null)
            {
                activeChild.textBox1.Undo();
            }
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CanvasTabPage activeChild = (CanvasTabPage)tabControl1.SelectedTab;
            if (activeChild != null)
            {
                activeChild.textBox1.Undo();
            }
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CanvasTabPage activeChild = (CanvasTabPage)tabControl1.SelectedTab;
            if (activeChild != null)
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
                showToolBox();
            else
                hideToolBox();
        }

        private void propertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (propertiesMenuItem.Checked)
                showProperties();
            else
                hideProperties();
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

        internal void showProperties()
        {
            propertiesMenuItem.Checked = true;
            splitContainer2.Panel2Collapsed = false;
            splitContainer2.Panel2.Show();
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
    }
}
