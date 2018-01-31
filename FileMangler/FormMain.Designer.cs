namespace FileMangler
{
    partial class FormMain
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.PageSelectFiles = new System.Windows.Forms.TabPage();
            this.BtnCopyMatchingFiles = new System.Windows.Forms.Button();
            this.LMatchingFiles = new System.Windows.Forms.Label();
            this.BtnFindFiles = new System.Windows.Forms.Button();
            this.CLBFilesThatMatch = new System.Windows.Forms.CheckedListBox();
            this.BtnBrowseProjectFolder = new System.Windows.Forms.Button();
            this.TProjectFolder = new System.Windows.Forms.TextBox();
            this.LProjectFolder = new System.Windows.Forms.Label();
            this.PageDebug = new System.Windows.Forms.TabPage();
            this.TDebug = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SBStatus = new System.Windows.Forms.StatusStrip();
            this.FolderBrowser = new System.Windows.Forms.FolderBrowserDialog();
            this.tabControl1.SuspendLayout();
            this.PageSelectFiles.SuspendLayout();
            this.PageDebug.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.PageSelectFiles);
            this.tabControl1.Controls.Add(this.PageDebug);
            this.tabControl1.Location = new System.Drawing.Point(12, 27);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(877, 519);
            this.tabControl1.TabIndex = 3;
            // 
            // PageSelectFiles
            // 
            this.PageSelectFiles.Controls.Add(this.BtnCopyMatchingFiles);
            this.PageSelectFiles.Controls.Add(this.LMatchingFiles);
            this.PageSelectFiles.Controls.Add(this.BtnFindFiles);
            this.PageSelectFiles.Controls.Add(this.CLBFilesThatMatch);
            this.PageSelectFiles.Controls.Add(this.BtnBrowseProjectFolder);
            this.PageSelectFiles.Controls.Add(this.TProjectFolder);
            this.PageSelectFiles.Controls.Add(this.LProjectFolder);
            this.PageSelectFiles.Location = new System.Drawing.Point(4, 22);
            this.PageSelectFiles.Name = "PageSelectFiles";
            this.PageSelectFiles.Padding = new System.Windows.Forms.Padding(3);
            this.PageSelectFiles.Size = new System.Drawing.Size(869, 493);
            this.PageSelectFiles.TabIndex = 0;
            this.PageSelectFiles.Text = "Select Files";
            this.PageSelectFiles.UseVisualStyleBackColor = true;
            // 
            // BtnCopyMatchingFiles
            // 
            this.BtnCopyMatchingFiles.Location = new System.Drawing.Point(88, 418);
            this.BtnCopyMatchingFiles.Name = "BtnCopyMatchingFiles";
            this.BtnCopyMatchingFiles.Size = new System.Drawing.Size(123, 23);
            this.BtnCopyMatchingFiles.TabIndex = 9;
            this.BtnCopyMatchingFiles.Text = "Copy Matching Files";
            this.BtnCopyMatchingFiles.UseVisualStyleBackColor = true;
            this.BtnCopyMatchingFiles.Click += new System.EventHandler(this.BtnCopyMatchingFiles_Click);
            // 
            // LMatchingFiles
            // 
            this.LMatchingFiles.AutoSize = true;
            this.LMatchingFiles.Location = new System.Drawing.Point(7, 62);
            this.LMatchingFiles.Name = "LMatchingFiles";
            this.LMatchingFiles.Size = new System.Drawing.Size(75, 13);
            this.LMatchingFiles.TabIndex = 8;
            this.LMatchingFiles.Text = "Matching Files";
            // 
            // BtnFindFiles
            // 
            this.BtnFindFiles.Location = new System.Drawing.Point(88, 33);
            this.BtnFindFiles.Name = "BtnFindFiles";
            this.BtnFindFiles.Size = new System.Drawing.Size(75, 23);
            this.BtnFindFiles.TabIndex = 7;
            this.BtnFindFiles.Text = "Find Files";
            this.BtnFindFiles.UseVisualStyleBackColor = true;
            this.BtnFindFiles.Click += new System.EventHandler(this.BtnFindFiles_Click);
            // 
            // CLBFilesThatMatch
            // 
            this.CLBFilesThatMatch.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CLBFilesThatMatch.FormattingEnabled = true;
            this.CLBFilesThatMatch.Location = new System.Drawing.Point(88, 62);
            this.CLBFilesThatMatch.Name = "CLBFilesThatMatch";
            this.CLBFilesThatMatch.Size = new System.Drawing.Size(775, 349);
            this.CLBFilesThatMatch.TabIndex = 6;
            // 
            // BtnBrowseProjectFolder
            // 
            this.BtnBrowseProjectFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnBrowseProjectFolder.Location = new System.Drawing.Point(788, 4);
            this.BtnBrowseProjectFolder.Name = "BtnBrowseProjectFolder";
            this.BtnBrowseProjectFolder.Size = new System.Drawing.Size(75, 23);
            this.BtnBrowseProjectFolder.TabIndex = 5;
            this.BtnBrowseProjectFolder.Text = "Browse";
            this.BtnBrowseProjectFolder.UseVisualStyleBackColor = true;
            this.BtnBrowseProjectFolder.Click += new System.EventHandler(this.BtnBrowseProjectFolder_Click);
            // 
            // TProjectFolder
            // 
            this.TProjectFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TProjectFolder.Location = new System.Drawing.Point(88, 6);
            this.TProjectFolder.Name = "TProjectFolder";
            this.TProjectFolder.Size = new System.Drawing.Size(694, 20);
            this.TProjectFolder.TabIndex = 4;
            this.TProjectFolder.Text = "C:\\Webs\\OpsysHO_Dev";
            // 
            // LProjectFolder
            // 
            this.LProjectFolder.AutoSize = true;
            this.LProjectFolder.Location = new System.Drawing.Point(7, 9);
            this.LProjectFolder.Name = "LProjectFolder";
            this.LProjectFolder.Size = new System.Drawing.Size(72, 13);
            this.LProjectFolder.TabIndex = 3;
            this.LProjectFolder.Text = "Project Folder";
            // 
            // PageDebug
            // 
            this.PageDebug.Controls.Add(this.TDebug);
            this.PageDebug.Location = new System.Drawing.Point(4, 22);
            this.PageDebug.Name = "PageDebug";
            this.PageDebug.Padding = new System.Windows.Forms.Padding(3);
            this.PageDebug.Size = new System.Drawing.Size(869, 493);
            this.PageDebug.TabIndex = 1;
            this.PageDebug.Text = "Debug";
            this.PageDebug.UseVisualStyleBackColor = true;
            // 
            // TDebug
            // 
            this.TDebug.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TDebug.Location = new System.Drawing.Point(3, 3);
            this.TDebug.Multiline = true;
            this.TDebug.Name = "TDebug";
            this.TDebug.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.TDebug.Size = new System.Drawing.Size(863, 487);
            this.TDebug.TabIndex = 0;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(901, 24);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(92, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // SBStatus
            // 
            this.SBStatus.Location = new System.Drawing.Point(0, 549);
            this.SBStatus.Name = "SBStatus";
            this.SBStatus.Size = new System.Drawing.Size(901, 22);
            this.SBStatus.TabIndex = 5;
            this.SBStatus.Text = "statusStrip1";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(901, 571);
            this.Controls.Add(this.SBStatus);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormMain";
            this.Text = "File Mangler";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.tabControl1.ResumeLayout(false);
            this.PageSelectFiles.ResumeLayout(false);
            this.PageSelectFiles.PerformLayout();
            this.PageDebug.ResumeLayout(false);
            this.PageDebug.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage PageSelectFiles;
        private System.Windows.Forms.Button BtnBrowseProjectFolder;
        private System.Windows.Forms.TextBox TProjectFolder;
        private System.Windows.Forms.Label LProjectFolder;
        private System.Windows.Forms.TabPage PageDebug;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.StatusStrip SBStatus;
        private System.Windows.Forms.TextBox TDebug;
        private System.Windows.Forms.CheckedListBox CLBFilesThatMatch;
        private System.Windows.Forms.Label LMatchingFiles;
        private System.Windows.Forms.Button BtnFindFiles;
        private System.Windows.Forms.Button BtnCopyMatchingFiles;
        private System.Windows.Forms.FolderBrowserDialog FolderBrowser;
    }
}

