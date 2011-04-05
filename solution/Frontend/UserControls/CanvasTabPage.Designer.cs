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
            try
            {
                base.Dispose(disposing);
            }
            catch (Exception) { }

        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.TabControl tabControl1;
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.htmlEditor1 = new onlyconnect.HtmlEditor();
            this.textBox1 = new System.Windows.Forms.TextBox();
            tabControl1 = new System.Windows.Forms.TabControl();
            tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Alignment = System.Windows.Forms.TabAlignment.Right;
            tabControl1.AllowDrop = true;
            tabControl1.Controls.Add(this.tabPage1);
            tabControl1.Controls.Add(this.tabPage2);
            tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            tabControl1.HotTrack = true;
            tabControl1.Location = new System.Drawing.Point(0, 0);
            tabControl1.Margin = new System.Windows.Forms.Padding(0);
            tabControl1.Multiline = true;
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new System.Drawing.Size(611, 538);
            tabControl1.TabIndex = 3;
            tabControl1.TabStop = false;
            // 
            // tabPage1
            // 
            this.tabPage1.AllowDrop = true;
            this.tabPage1.Controls.Add(this.htmlEditor1);
            this.tabPage1.Location = new System.Drawing.Point(4, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(584, 530);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "view";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.AllowDrop = true;
            this.tabPage2.Controls.Add(this.textBox1);
            this.tabPage2.Location = new System.Drawing.Point(4, 4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(584, 530);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "edit";
            this.tabPage2.UseVisualStyleBackColor = true;
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
            this.Controls.Add(tabControl1);
            this.Name = "ControlCanvas";
            this.Size = new System.Drawing.Size(611, 538);
            tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        internal System.Windows.Forms.TextBox textBox1;
        private onlyconnect.HtmlEditor htmlEditor1;
    }
}
