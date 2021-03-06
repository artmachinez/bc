﻿using System;
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
            this.components = new System.ComponentModel.Container();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.browserTabPage = new System.Windows.Forms.TabPage();
            this.htmlEditor1 = new onlyconnect.HtmlEditor();
            this.textEditorTabPage = new System.Windows.Forms.TabPage();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.moduleRightClickMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.moduleProperties = new System.Windows.Forms.ToolStripMenuItem();
            this.moduleViewSource = new System.Windows.Forms.ToolStripMenuItem();
            this.moduleToggleEditMode = new System.Windows.Forms.ToolStripMenuItem();
            this.rightClickMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.viewSource = new System.Windows.Forms.ToolStripMenuItem();
            this.toggleEditMode = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1.SuspendLayout();
            this.browserTabPage.SuspendLayout();
            this.textEditorTabPage.SuspendLayout();
            this.moduleRightClickMenu.SuspendLayout();
            this.rightClickMenu.SuspendLayout();
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
            // browserTabPage
            // 
            this.browserTabPage.AllowDrop = true;
            this.browserTabPage.Controls.Add(this.htmlEditor1);
            this.browserTabPage.Location = new System.Drawing.Point(4, 4);
            this.browserTabPage.Name = "browserTabPage";
            this.browserTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.browserTabPage.Size = new System.Drawing.Size(584, 530);
            this.browserTabPage.TabIndex = 0;
            this.browserTabPage.Text = "view";
            this.browserTabPage.UseVisualStyleBackColor = true;
            // 
            // htmlEditor1
            // 
            this.htmlEditor1.AllowDrop = true;
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
            // textEditorTabPage
            // 
            this.textEditorTabPage.AllowDrop = true;
            this.textEditorTabPage.Controls.Add(this.textBox1);
            this.textEditorTabPage.Location = new System.Drawing.Point(4, 4);
            this.textEditorTabPage.Name = "textEditorTabPage";
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
            // moduleRightClickMenu
            // 
            this.moduleRightClickMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.moduleProperties,
            this.moduleViewSource,
            this.moduleToggleEditMode});
            this.moduleRightClickMenu.Name = "moduleRightClickMenu";
            this.moduleRightClickMenu.Size = new System.Drawing.Size(139, 70);
            this.moduleRightClickMenu.Text = "moduleRightClickMenu";
            // 
            // moduleProperties
            // 
            this.moduleProperties.Name = "moduleProperties";
            this.moduleProperties.Size = new System.Drawing.Size(168, 22);
            this.moduleProperties.Text = "Properties";
            this.moduleProperties.Click += new System.EventHandler(this.moduleProperties_Click);
            // 
            // moduleViewSource
            // 
            this.moduleViewSource.Name = "moduleViewSource";
            this.moduleViewSource.Size = new System.Drawing.Size(168, 22);
            this.moduleViewSource.Text = "View Source";
            this.moduleViewSource.Click += new System.EventHandler(this.viewSource_Click);
            // 
            // moduleToggleEditMode
            // 
            this.moduleToggleEditMode.Checked = true;
            this.moduleToggleEditMode.CheckOnClick = true;
            this.moduleToggleEditMode.CheckState = System.Windows.Forms.CheckState.Checked;
            this.moduleToggleEditMode.Name = "moduleToggleEditMode";
            this.moduleToggleEditMode.Size = new System.Drawing.Size(138, 22);
            this.moduleToggleEditMode.Text = "Edit Mode";
            this.moduleToggleEditMode.Click += new System.EventHandler(this.toggleEditMode_Click);
            // 
            // rightClickMenu
            // 
            this.rightClickMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewSource,
            this.toggleEditMode});
            this.rightClickMenu.Name = "rightClickMenu";
            this.rightClickMenu.Size = new System.Drawing.Size(139, 48);
            this.rightClickMenu.Text = "rightClickMenu";
            // 
            // viewSource
            // 
            this.viewSource.Name = "viewSource";
            this.viewSource.Size = new System.Drawing.Size(168, 22);
            this.viewSource.Text = "View Source";
            this.viewSource.Click += new System.EventHandler(this.viewSource_Click);
            // 
            // toggleEditMode
            // 
            this.toggleEditMode.Checked = true;
            this.toggleEditMode.CheckOnClick = true;
            this.toggleEditMode.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toggleEditMode.Name = "toggleEditMode";
            this.toggleEditMode.Size = new System.Drawing.Size(138, 22);
            this.toggleEditMode.Text = "Edit Mode";
            this.toggleEditMode.Click += new System.EventHandler(this.toggleEditMode_Click);
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
            this.moduleRightClickMenu.ResumeLayout(false);
            this.rightClickMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage browserTabPage;
        private System.Windows.Forms.TabPage textEditorTabPage;
        internal System.Windows.Forms.TextBox textBox1;
        public onlyconnect.HtmlEditor htmlEditor1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.ContextMenuStrip moduleRightClickMenu;
        private System.Windows.Forms.ToolStripMenuItem moduleProperties;
        private System.Windows.Forms.ContextMenuStrip rightClickMenu;
        private System.Windows.Forms.ToolStripMenuItem moduleViewSource;
        private System.Windows.Forms.ToolStripMenuItem viewSource;
        private System.Windows.Forms.ToolStripMenuItem toggleEditMode;
        private System.Windows.Forms.ToolStripMenuItem moduleToggleEditMode;
    }
}
