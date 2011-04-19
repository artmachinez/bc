using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Core;
using Core.Project;
using Core.Helpers;
using System.Diagnostics;

namespace Frontend.Forms
{
    public partial class GenerateCodeForm : Form
    {
        private UserControls.CanvasTabPage page;

        public GenerateCodeForm(UserControls.CanvasTabPage page)
        {
            InitializeComponent();
            page.projectInfo.projectXml = page.activeProjectContent;
            this.page = page;
            this.languageValueLabel.Text = page.projectInfo.languageID;
        }

        private void pathTextBox_MouseClick(object sender, MouseEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Html File (*.html)|*.html";
            saveFileDialog.Title = "generate web";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                this.pathTextBox.Text = saveFileDialog.FileName;
            }
        }

        private void generateButton_Click(object sender, EventArgs e)
        {
            if (!CFileHelper.generatePage(page.projectInfo, this.pathTextBox.Text))
            {
                MessageBox.Show("Error generating output");
            }
            else
            {
                MessageBox.Show("Code generated successfully");
                this.Close();
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }






    }
}
