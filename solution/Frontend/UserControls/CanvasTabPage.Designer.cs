using System;
namespace Frontend.UserControls
{
    partial class CanvasTabPage
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.browserTabPage = new System.Windows.Forms.TabPage();
            this.htmlEditor1 = new onlyconnect.HtmlEditor();
            this.textEditorTabPage = new System.Windows.Forms.TabPage();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.tabControl1.SuspendLayout();
            this.browserTabPage.SuspendLayout();
            this.textEditorTabPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Alignment = System.Windows.Forms.TabAlignment.Right;
            this.tabControl1.AllowDrop = true;
            this.tabControl1.Controls.Add(this.browserTabPage);
            this.tabControl1.Controls.Add(this.textEditorTabPage);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.HotTrack = true;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(0);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(611, 538);
            this.tabControl1.TabIndex = 3;
            this.tabControl1.TabStop = false;
            // 
            // tabPage1
            // 
            this.browserTabPage.AllowDrop = true;
            this.browserTabPage.Controls.Add(this.htmlEditor1);
            this.browserTabPage.Location = new System.Drawing.Point(4, 4);
            this.browserTabPage.Name = "tabPage1";
            this.browserTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.browserTabPage.Size = new System.Drawing.Size(584, 530);
            this.browserTabPage.TabIndex = 0;
            this.browserTabPage.Text = "view";
            this.browserTabPage.UseVisualStyleBackColor = true;
            // 
            // htmlEditor1
            // 
            this.htmlEditor1.DefaultComposeSettings.BackColor = System.Drawing.Color.White;
            this.htmlEditor1.DefaultComposeSettings.DefaultFont = new System.Drawing.Font("Arial", 10F);
            this.htmlEditor1.DefaultComposeSettings.Enabled = false;
            this.htmlEditor1.DefaultComposeSettings.ForeColor = System.Drawing.Color.Black;
            this.htmlEditor1.DefaultPreamble = onlyconnect.EncodingType.UTF8;
            this.htmlEditor1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.htmlEditor1.DocumentEncoding = onlyconnect.EncodingType.WindowsCurrent;
            this.htmlEditor1.IsDesignMode = true;
            this.htmlEditor1.Location = new System.Drawing.Point(3, 3);
            this.htmlEditor1.Name = "htmlEditor1";
            this.htmlEditor1.SelectionAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.htmlEditor1.SelectionBackColor = System.Drawing.Color.Empty;
            this.htmlEditor1.SelectionBullets = false;
            this.htmlEditor1.SelectionFont = null;
            this.htmlEditor1.SelectionForeColor = System.Drawing.Color.Empty;
            this.htmlEditor1.SelectionNumbering = false;
            this.htmlEditor1.Size = new System.Drawing.Size(578, 524);
            this.htmlEditor1.TabIndex = 0;
            this.htmlEditor1.Text = "htmlEditor1";
            this.htmlEditor1.VisibleChanged += new System.EventHandler(this.wb_VisibleChanged);
            // 
            // tabPage2
            // 
            this.textEditorTabPage.AllowDrop = true;
            this.textEditorTabPage.Controls.Add(this.textBox1);
            this.textEditorTabPage.Location = new System.Drawing.Point(4, 4);
            this.textEditorTabPage.Name = "tabPage2";
            this.textEditorTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.textEditorTabPage.Size = new System.Drawing.Size(584, 530);
            this.textEditorTabPage.TabIndex = 1;
            this.textEditorTabPage.Text = "edit";
            this.textEditorTabPage.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.AllowDrop = true;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Location = new System.Drawing.Point(3, 3);
            this.textBox1.Margin = new System.Windows.Forms.Padding(0);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(578, 524);
            this.textBox1.TabIndex = 0;
            this.textBox1.VisibleChanged += new System.EventHandler(this.textBox1_VisibleChanged);
            this.textBox1.DragDrop += new System.Windows.Forms.DragEventHandler(this.textBox1_DragDrop);
            this.textBox1.DragEnter += new System.Windows.Forms.DragEventHandler(this.textBox1_DragEnter);
            this.textBox1.DragOver += new System.Windows.Forms.DragEventHandler(this.textBox1_DragOver);
            // 
            // CanvasTabPage
            // 
            this.AllowDrop = true;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.Controls.Add(this.tabControl1);
            this.Name = "ControlCanvas";
            this.Size = new System.Drawing.Size(611, 538);
            this.tabControl1.ResumeLayout(false);
            this.browserTabPage.ResumeLayout(false);
            this.textEditorTabPage.ResumeLayout(false);
            this.textEditorTabPage.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage browserTabPage;
        private System.Windows.Forms.TabPage textEditorTabPage;
        internal System.Windows.Forms.TextBox textBox1;
        private onlyconnect.HtmlEditor htmlEditor1;
        private System.Windows.Forms.TabControl tabControl1;
    }
}
