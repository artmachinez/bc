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
using System.Diagnostics;

namespace Frontend.Forms
{
    public partial class GenerateCodeForm : Form
    {
        private UserControls.CanvasTabPage page;

        public GenerateCodeForm(UserControls.CanvasTabPage page)
        {
            this.page = page;
            InitializeComponent();
        }

        private void pathTextBox_MouseClick(object sender, MouseEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Html File (*.html)|*.html";
            saveFileDialog.Title = "generate web";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string fileURL = saveFileDialog.FileName;
                string fileName = System.IO.Path.GetFileName(fileURL);

                this.pathTextBox.Text = fileURL;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Debug.WriteLine(CXMLParser.Instance.getHTMLFromProjectXML(this.page.XMLProjectContent));
            //this.generateHTMLFile();
            //this.Close();
        }

        private void generateHTMLFile()
        {
            StreamWriter fileStream = new StreamWriter(this.pathTextBox.Text);
            fileStream.Write(this.page.XMLProjectContent);
            fileStream.Close();
        }






    }
}
