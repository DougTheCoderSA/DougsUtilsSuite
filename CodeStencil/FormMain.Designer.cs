namespace CodeStencil
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
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.SBLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.TPDBServer = new System.Windows.Forms.TabPage();
            this.TPDebug = new System.Windows.Forms.TabPage();
            this.GBServerDetails = new System.Windows.Forms.GroupBox();
            this.TSQLServerNameOrIP = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.TSQLUserName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.TSQLPassword = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.BtnSQLServerConnect = new System.Windows.Forms.Button();
            this.CBDatabaseList = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.PSQLServerConnStatus = new System.Windows.Forms.Panel();
            this.RTDebug = new System.Windows.Forms.RichTextBox();
            this.TPGenerate = new System.Windows.Forms.TabPage();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.TPDBServer.SuspendLayout();
            this.TPDebug.SuspendLayout();
            this.GBServerDetails.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SBLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 594);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(890, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // SBLabel
            // 
            this.SBLabel.Name = "SBLabel";
            this.SBLabel.Size = new System.Drawing.Size(875, 17);
            this.SBLabel.Spring = true;
            this.SBLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(890, 24);
            this.menuStrip1.TabIndex = 1;
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
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.TPDBServer);
            this.tabControl1.Controls.Add(this.TPGenerate);
            this.tabControl1.Controls.Add(this.TPDebug);
            this.tabControl1.Location = new System.Drawing.Point(12, 27);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(866, 564);
            this.tabControl1.TabIndex = 2;
            // 
            // TPDBServer
            // 
            this.TPDBServer.Controls.Add(this.GBServerDetails);
            this.TPDBServer.Location = new System.Drawing.Point(4, 22);
            this.TPDBServer.Name = "TPDBServer";
            this.TPDBServer.Padding = new System.Windows.Forms.Padding(3);
            this.TPDBServer.Size = new System.Drawing.Size(858, 538);
            this.TPDBServer.TabIndex = 0;
            this.TPDBServer.Text = "SQL Server";
            this.TPDBServer.UseVisualStyleBackColor = true;
            // 
            // TPDebug
            // 
            this.TPDebug.Controls.Add(this.RTDebug);
            this.TPDebug.Location = new System.Drawing.Point(4, 22);
            this.TPDebug.Name = "TPDebug";
            this.TPDebug.Padding = new System.Windows.Forms.Padding(3);
            this.TPDebug.Size = new System.Drawing.Size(858, 538);
            this.TPDebug.TabIndex = 1;
            this.TPDebug.Text = "Debug";
            this.TPDebug.UseVisualStyleBackColor = true;
            // 
            // GBServerDetails
            // 
            this.GBServerDetails.Controls.Add(this.PSQLServerConnStatus);
            this.GBServerDetails.Controls.Add(this.label4);
            this.GBServerDetails.Controls.Add(this.CBDatabaseList);
            this.GBServerDetails.Controls.Add(this.BtnSQLServerConnect);
            this.GBServerDetails.Controls.Add(this.label3);
            this.GBServerDetails.Controls.Add(this.TSQLPassword);
            this.GBServerDetails.Controls.Add(this.label2);
            this.GBServerDetails.Controls.Add(this.TSQLUserName);
            this.GBServerDetails.Controls.Add(this.label1);
            this.GBServerDetails.Controls.Add(this.TSQLServerNameOrIP);
            this.GBServerDetails.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GBServerDetails.Location = new System.Drawing.Point(6, 6);
            this.GBServerDetails.Name = "GBServerDetails";
            this.GBServerDetails.Size = new System.Drawing.Size(576, 231);
            this.GBServerDetails.TabIndex = 0;
            this.GBServerDetails.TabStop = false;
            this.GBServerDetails.Text = "SQL Server Details";
            // 
            // TSQLServerNameOrIP
            // 
            this.TSQLServerNameOrIP.Location = new System.Drawing.Point(165, 14);
            this.TSQLServerNameOrIP.Name = "TSQLServerNameOrIP";
            this.TSQLServerNameOrIP.Size = new System.Drawing.Size(363, 20);
            this.TSQLServerNameOrIP.TabIndex = 0;
            this.TSQLServerNameOrIP.Text = "(local)";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(135, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Server Name or IP Address";
            // 
            // TSQLUserName
            // 
            this.TSQLUserName.Location = new System.Drawing.Point(165, 40);
            this.TSQLUserName.Name = "TSQLUserName";
            this.TSQLUserName.Size = new System.Drawing.Size(176, 20);
            this.TSQLUserName.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "SQL User Name";
            // 
            // TSQLPassword
            // 
            this.TSQLPassword.Location = new System.Drawing.Point(165, 66);
            this.TSQLPassword.Name = "TSQLPassword";
            this.TSQLPassword.Size = new System.Drawing.Size(100, 20);
            this.TSQLPassword.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "SQL Password";
            // 
            // BtnSQLServerConnect
            // 
            this.BtnSQLServerConnect.Location = new System.Drawing.Point(165, 92);
            this.BtnSQLServerConnect.Name = "BtnSQLServerConnect";
            this.BtnSQLServerConnect.Size = new System.Drawing.Size(116, 23);
            this.BtnSQLServerConnect.TabIndex = 6;
            this.BtnSQLServerConnect.Text = "Connect to Server";
            this.BtnSQLServerConnect.UseVisualStyleBackColor = true;
            this.BtnSQLServerConnect.Click += new System.EventHandler(this.BtnSQLServerConnect_Click);
            // 
            // CBDatabaseList
            // 
            this.CBDatabaseList.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.CBDatabaseList.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.CBDatabaseList.Enabled = false;
            this.CBDatabaseList.FormattingEnabled = true;
            this.CBDatabaseList.Location = new System.Drawing.Point(165, 122);
            this.CBDatabaseList.Name = "CBDatabaseList";
            this.CBDatabaseList.Size = new System.Drawing.Size(363, 21);
            this.CBDatabaseList.TabIndex = 7;
            this.CBDatabaseList.SelectedValueChanged += new System.EventHandler(this.CBDatabaseList_SelectedValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 125);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Select Database";
            // 
            // PSQLServerConnStatus
            // 
            this.PSQLServerConnStatus.Location = new System.Drawing.Point(288, 92);
            this.PSQLServerConnStatus.Name = "PSQLServerConnStatus";
            this.PSQLServerConnStatus.Size = new System.Drawing.Size(25, 24);
            this.PSQLServerConnStatus.TabIndex = 9;
            // 
            // RTDebug
            // 
            this.RTDebug.Location = new System.Drawing.Point(6, 6);
            this.RTDebug.Name = "RTDebug";
            this.RTDebug.Size = new System.Drawing.Size(846, 526);
            this.RTDebug.TabIndex = 0;
            this.RTDebug.Text = "";
            // 
            // TPGenerate
            // 
            this.TPGenerate.Location = new System.Drawing.Point(4, 22);
            this.TPGenerate.Name = "TPGenerate";
            this.TPGenerate.Padding = new System.Windows.Forms.Padding(3);
            this.TPGenerate.Size = new System.Drawing.Size(858, 538);
            this.TPGenerate.TabIndex = 2;
            this.TPGenerate.Text = "Generate Code";
            this.TPGenerate.UseVisualStyleBackColor = true;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(890, 616);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormMain";
            this.Text = "Code Stencil 2016";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.TPDBServer.ResumeLayout(false);
            this.TPDebug.ResumeLayout(false);
            this.GBServerDetails.ResumeLayout(false);
            this.GBServerDetails.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel SBLabel;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage TPDBServer;
        private System.Windows.Forms.TabPage TPDebug;
        private System.Windows.Forms.GroupBox GBServerDetails;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TSQLServerNameOrIP;
        private System.Windows.Forms.TextBox TSQLUserName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button BtnSQLServerConnect;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox TSQLPassword;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox CBDatabaseList;
        private System.Windows.Forms.Panel PSQLServerConnStatus;
        private System.Windows.Forms.RichTextBox RTDebug;
        private System.Windows.Forms.TabPage TPGenerate;
    }
}

