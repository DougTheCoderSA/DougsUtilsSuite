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
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
            "Hello",
            "varchar",
            "50"}, -1);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.SBLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.TPDBServer = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.BtnDBInvertSelection = new System.Windows.Forms.Button();
            this.BtnDBSelectNone = new System.Windows.Forms.Button();
            this.BtnDBSelectAll = new System.Windows.Forms.Button();
            this.CLBDatabases = new System.Windows.Forms.CheckedListBox();
            this.GBServerDetails = new System.Windows.Forms.GroupBox();
            this.PSQLServerConnStatus = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.CBDatabase = new System.Windows.Forms.ComboBox();
            this.BtnSQLServerConnect = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.TSQLPassword = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.TSQLUserName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.TSQLServerNameOrIP = new System.Windows.Forms.TextBox();
            this.TPTable = new System.Windows.Forms.TabPage();
            this.label8 = new System.Windows.Forms.Label();
            this.CBTable = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.BtnTableInvertSelection = new System.Windows.Forms.Button();
            this.BtnTableSelectNone = new System.Windows.Forms.Button();
            this.BtnTableSelectAll = new System.Windows.Forms.Button();
            this.CLBTables = new System.Windows.Forms.CheckedListBox();
            this.TPColumn = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.LVColumns = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.BtnColumnInvertSelection = new System.Windows.Forms.Button();
            this.BtnColumnSelectNone = new System.Windows.Forms.Button();
            this.BtnColumnSelectAll = new System.Windows.Forms.Button();
            this.TPGenerate = new System.Windows.Forms.TabPage();
            this.BtnDeleteTemplate = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.RTEditTemplate = new System.Windows.Forms.RichTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.RTGeneratedCode = new System.Windows.Forms.RichTextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.BtnGenerateCode = new System.Windows.Forms.Button();
            this.BtnSaveCodeTemplate = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.CBCodeTemplate = new System.Windows.Forms.ComboBox();
            this.TPDebug = new System.Windows.Forms.TabPage();
            this.RTDebug = new System.Windows.Forms.RichTextBox();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.TPDBServer.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.GBServerDetails.SuspendLayout();
            this.TPTable.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.TPColumn.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.TPGenerate.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.TPDebug.SuspendLayout();
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
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(92, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.TPDBServer);
            this.tabControl1.Controls.Add(this.TPTable);
            this.tabControl1.Controls.Add(this.TPColumn);
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
            this.TPDBServer.Controls.Add(this.groupBox1);
            this.TPDBServer.Controls.Add(this.GBServerDetails);
            this.TPDBServer.Location = new System.Drawing.Point(4, 22);
            this.TPDBServer.Name = "TPDBServer";
            this.TPDBServer.Padding = new System.Windows.Forms.Padding(3);
            this.TPDBServer.Size = new System.Drawing.Size(858, 538);
            this.TPDBServer.TabIndex = 0;
            this.TPDBServer.Text = "SQL Server, DB";
            this.TPDBServer.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.BtnDBInvertSelection);
            this.groupBox1.Controls.Add(this.BtnDBSelectNone);
            this.groupBox1.Controls.Add(this.BtnDBSelectAll);
            this.groupBox1.Controls.Add(this.CLBDatabases);
            this.groupBox1.Location = new System.Drawing.Point(6, 167);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(846, 365);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Databases to Use in Generate";
            // 
            // BtnDBInvertSelection
            // 
            this.BtnDBInvertSelection.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.BtnDBInvertSelection.Location = new System.Drawing.Point(231, 336);
            this.BtnDBInvertSelection.Name = "BtnDBInvertSelection";
            this.BtnDBInvertSelection.Size = new System.Drawing.Size(105, 23);
            this.BtnDBInvertSelection.TabIndex = 3;
            this.BtnDBInvertSelection.Text = "Invert Selection";
            this.BtnDBInvertSelection.UseVisualStyleBackColor = true;
            this.BtnDBInvertSelection.Click += new System.EventHandler(this.BtnDBInvertSelection_Click);
            // 
            // BtnDBSelectNone
            // 
            this.BtnDBSelectNone.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.BtnDBSelectNone.Location = new System.Drawing.Point(120, 336);
            this.BtnDBSelectNone.Name = "BtnDBSelectNone";
            this.BtnDBSelectNone.Size = new System.Drawing.Size(105, 23);
            this.BtnDBSelectNone.TabIndex = 2;
            this.BtnDBSelectNone.Text = "Select None";
            this.BtnDBSelectNone.UseVisualStyleBackColor = true;
            this.BtnDBSelectNone.Click += new System.EventHandler(this.BtnDBSelectNone_Click);
            // 
            // BtnDBSelectAll
            // 
            this.BtnDBSelectAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.BtnDBSelectAll.Location = new System.Drawing.Point(9, 336);
            this.BtnDBSelectAll.Name = "BtnDBSelectAll";
            this.BtnDBSelectAll.Size = new System.Drawing.Size(105, 23);
            this.BtnDBSelectAll.TabIndex = 1;
            this.BtnDBSelectAll.Text = "Select All";
            this.BtnDBSelectAll.UseVisualStyleBackColor = true;
            this.BtnDBSelectAll.Click += new System.EventHandler(this.BtnDBSelectAll_Click);
            // 
            // CLBDatabases
            // 
            this.CLBDatabases.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CLBDatabases.FormattingEnabled = true;
            this.CLBDatabases.Location = new System.Drawing.Point(9, 19);
            this.CLBDatabases.Name = "CLBDatabases";
            this.CLBDatabases.Size = new System.Drawing.Size(831, 304);
            this.CLBDatabases.TabIndex = 0;
            // 
            // GBServerDetails
            // 
            this.GBServerDetails.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GBServerDetails.Controls.Add(this.PSQLServerConnStatus);
            this.GBServerDetails.Controls.Add(this.label4);
            this.GBServerDetails.Controls.Add(this.CBDatabase);
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
            this.GBServerDetails.Size = new System.Drawing.Size(846, 155);
            this.GBServerDetails.TabIndex = 0;
            this.GBServerDetails.TabStop = false;
            this.GBServerDetails.Text = "SQL Server Details";
            // 
            // PSQLServerConnStatus
            // 
            this.PSQLServerConnStatus.Location = new System.Drawing.Point(288, 92);
            this.PSQLServerConnStatus.Name = "PSQLServerConnStatus";
            this.PSQLServerConnStatus.Size = new System.Drawing.Size(25, 24);
            this.PSQLServerConnStatus.TabIndex = 9;
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
            // CBDatabase
            // 
            this.CBDatabase.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.CBDatabase.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.CBDatabase.Enabled = false;
            this.CBDatabase.FormattingEnabled = true;
            this.CBDatabase.Location = new System.Drawing.Point(165, 122);
            this.CBDatabase.Name = "CBDatabase";
            this.CBDatabase.Size = new System.Drawing.Size(363, 21);
            this.CBDatabase.TabIndex = 7;
            this.CBDatabase.SelectedValueChanged += new System.EventHandler(this.CBDatabase_SelectedValueChanged);
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
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "SQL Password";
            // 
            // TSQLPassword
            // 
            this.TSQLPassword.Location = new System.Drawing.Point(165, 66);
            this.TSQLPassword.Name = "TSQLPassword";
            this.TSQLPassword.Size = new System.Drawing.Size(176, 20);
            this.TSQLPassword.TabIndex = 4;
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
            // TSQLUserName
            // 
            this.TSQLUserName.Location = new System.Drawing.Point(165, 40);
            this.TSQLUserName.Name = "TSQLUserName";
            this.TSQLUserName.Size = new System.Drawing.Size(176, 20);
            this.TSQLUserName.TabIndex = 2;
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
            // TSQLServerNameOrIP
            // 
            this.TSQLServerNameOrIP.Location = new System.Drawing.Point(165, 14);
            this.TSQLServerNameOrIP.Name = "TSQLServerNameOrIP";
            this.TSQLServerNameOrIP.Size = new System.Drawing.Size(363, 20);
            this.TSQLServerNameOrIP.TabIndex = 0;
            this.TSQLServerNameOrIP.Text = "(local)";
            // 
            // TPTable
            // 
            this.TPTable.Controls.Add(this.label8);
            this.TPTable.Controls.Add(this.CBTable);
            this.TPTable.Controls.Add(this.groupBox2);
            this.TPTable.Location = new System.Drawing.Point(4, 22);
            this.TPTable.Name = "TPTable";
            this.TPTable.Padding = new System.Windows.Forms.Padding(3);
            this.TPTable.Size = new System.Drawing.Size(858, 538);
            this.TPTable.TabIndex = 3;
            this.TPTable.Text = "Table";
            this.TPTable.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(9, 514);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(67, 13);
            this.label8.TabIndex = 2;
            this.label8.Text = "Select Table";
            // 
            // CBTable
            // 
            this.CBTable.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CBTable.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.CBTable.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.CBTable.FormattingEnabled = true;
            this.CBTable.Location = new System.Drawing.Point(82, 511);
            this.CBTable.Name = "CBTable";
            this.CBTable.Size = new System.Drawing.Size(770, 21);
            this.CBTable.TabIndex = 1;
            this.CBTable.SelectedIndexChanged += new System.EventHandler(this.CBTable_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.BtnTableInvertSelection);
            this.groupBox2.Controls.Add(this.BtnTableSelectNone);
            this.groupBox2.Controls.Add(this.BtnTableSelectAll);
            this.groupBox2.Controls.Add(this.CLBTables);
            this.groupBox2.Location = new System.Drawing.Point(6, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(846, 499);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Tables to Use in Generate";
            // 
            // BtnTableInvertSelection
            // 
            this.BtnTableInvertSelection.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.BtnTableInvertSelection.Location = new System.Drawing.Point(228, 470);
            this.BtnTableInvertSelection.Name = "BtnTableInvertSelection";
            this.BtnTableInvertSelection.Size = new System.Drawing.Size(105, 23);
            this.BtnTableInvertSelection.TabIndex = 6;
            this.BtnTableInvertSelection.Text = "Invert Selection";
            this.BtnTableInvertSelection.UseVisualStyleBackColor = true;
            this.BtnTableInvertSelection.Click += new System.EventHandler(this.BtnTableInvertSelection_Click);
            // 
            // BtnTableSelectNone
            // 
            this.BtnTableSelectNone.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.BtnTableSelectNone.Location = new System.Drawing.Point(117, 470);
            this.BtnTableSelectNone.Name = "BtnTableSelectNone";
            this.BtnTableSelectNone.Size = new System.Drawing.Size(105, 23);
            this.BtnTableSelectNone.TabIndex = 5;
            this.BtnTableSelectNone.Text = "Select None";
            this.BtnTableSelectNone.UseVisualStyleBackColor = true;
            this.BtnTableSelectNone.Click += new System.EventHandler(this.BtnTableSelectNone_Click);
            // 
            // BtnTableSelectAll
            // 
            this.BtnTableSelectAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.BtnTableSelectAll.Location = new System.Drawing.Point(6, 470);
            this.BtnTableSelectAll.Name = "BtnTableSelectAll";
            this.BtnTableSelectAll.Size = new System.Drawing.Size(105, 23);
            this.BtnTableSelectAll.TabIndex = 4;
            this.BtnTableSelectAll.Text = "Select All";
            this.BtnTableSelectAll.UseVisualStyleBackColor = true;
            this.BtnTableSelectAll.Click += new System.EventHandler(this.BtnTableSelectAll_Click);
            // 
            // CLBTables
            // 
            this.CLBTables.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CLBTables.FormattingEnabled = true;
            this.CLBTables.Location = new System.Drawing.Point(6, 19);
            this.CLBTables.Name = "CLBTables";
            this.CLBTables.Size = new System.Drawing.Size(834, 439);
            this.CLBTables.TabIndex = 0;
            // 
            // TPColumn
            // 
            this.TPColumn.Controls.Add(this.groupBox3);
            this.TPColumn.Location = new System.Drawing.Point(4, 22);
            this.TPColumn.Name = "TPColumn";
            this.TPColumn.Size = new System.Drawing.Size(858, 538);
            this.TPColumn.TabIndex = 4;
            this.TPColumn.Text = "Column";
            this.TPColumn.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.LVColumns);
            this.groupBox3.Controls.Add(this.BtnColumnInvertSelection);
            this.groupBox3.Controls.Add(this.BtnColumnSelectNone);
            this.groupBox3.Controls.Add(this.BtnColumnSelectAll);
            this.groupBox3.Location = new System.Drawing.Point(6, 6);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(846, 529);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Columns to Use in Generate";
            // 
            // LVColumns
            // 
            this.LVColumns.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LVColumns.CheckBoxes = true;
            this.LVColumns.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7});
            listViewItem1.StateImageIndex = 0;
            this.LVColumns.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1});
            this.LVColumns.Location = new System.Drawing.Point(6, 19);
            this.LVColumns.Name = "LVColumns";
            this.LVColumns.Size = new System.Drawing.Size(834, 469);
            this.LVColumns.TabIndex = 7;
            this.LVColumns.UseCompatibleStateImageBehavior = false;
            this.LVColumns.View = System.Windows.Forms.View.Details;
            this.LVColumns.Click += new System.EventHandler(this.LVColumns_Click);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Column Name";
            this.columnHeader1.Width = 199;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Data Type";
            this.columnHeader2.Width = 83;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Max Length";
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Precision";
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Scale";
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Nullable";
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Identity";
            // 
            // BtnColumnInvertSelection
            // 
            this.BtnColumnInvertSelection.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.BtnColumnInvertSelection.Location = new System.Drawing.Point(228, 500);
            this.BtnColumnInvertSelection.Name = "BtnColumnInvertSelection";
            this.BtnColumnInvertSelection.Size = new System.Drawing.Size(105, 23);
            this.BtnColumnInvertSelection.TabIndex = 6;
            this.BtnColumnInvertSelection.Text = "Invert Selection";
            this.BtnColumnInvertSelection.UseVisualStyleBackColor = true;
            this.BtnColumnInvertSelection.Click += new System.EventHandler(this.BtnColumnInvertSelection_Click);
            // 
            // BtnColumnSelectNone
            // 
            this.BtnColumnSelectNone.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.BtnColumnSelectNone.Location = new System.Drawing.Point(117, 500);
            this.BtnColumnSelectNone.Name = "BtnColumnSelectNone";
            this.BtnColumnSelectNone.Size = new System.Drawing.Size(105, 23);
            this.BtnColumnSelectNone.TabIndex = 5;
            this.BtnColumnSelectNone.Text = "Select None";
            this.BtnColumnSelectNone.UseVisualStyleBackColor = true;
            this.BtnColumnSelectNone.Click += new System.EventHandler(this.BtnColumnSelectNone_Click);
            // 
            // BtnColumnSelectAll
            // 
            this.BtnColumnSelectAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.BtnColumnSelectAll.Location = new System.Drawing.Point(6, 500);
            this.BtnColumnSelectAll.Name = "BtnColumnSelectAll";
            this.BtnColumnSelectAll.Size = new System.Drawing.Size(105, 23);
            this.BtnColumnSelectAll.TabIndex = 4;
            this.BtnColumnSelectAll.Text = "Select All";
            this.BtnColumnSelectAll.UseVisualStyleBackColor = true;
            this.BtnColumnSelectAll.Click += new System.EventHandler(this.BtnColumnSelectAll_Click);
            // 
            // TPGenerate
            // 
            this.TPGenerate.Controls.Add(this.BtnDeleteTemplate);
            this.TPGenerate.Controls.Add(this.splitContainer1);
            this.TPGenerate.Controls.Add(this.BtnGenerateCode);
            this.TPGenerate.Controls.Add(this.BtnSaveCodeTemplate);
            this.TPGenerate.Controls.Add(this.label5);
            this.TPGenerate.Controls.Add(this.CBCodeTemplate);
            this.TPGenerate.Location = new System.Drawing.Point(4, 22);
            this.TPGenerate.Name = "TPGenerate";
            this.TPGenerate.Padding = new System.Windows.Forms.Padding(3);
            this.TPGenerate.Size = new System.Drawing.Size(858, 538);
            this.TPGenerate.TabIndex = 2;
            this.TPGenerate.Text = "Generate Code";
            this.TPGenerate.UseVisualStyleBackColor = true;
            // 
            // BtnDeleteTemplate
            // 
            this.BtnDeleteTemplate.Location = new System.Drawing.Point(296, 34);
            this.BtnDeleteTemplate.Name = "BtnDeleteTemplate";
            this.BtnDeleteTemplate.Size = new System.Drawing.Size(95, 23);
            this.BtnDeleteTemplate.TabIndex = 6;
            this.BtnDeleteTemplate.Text = "Delete Template";
            this.BtnDeleteTemplate.UseVisualStyleBackColor = true;
            this.BtnDeleteTemplate.Click += new System.EventHandler(this.BtnDeleteTemplate_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(9, 63);
            this.splitContainer1.MinimumSize = new System.Drawing.Size(50, 50);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.RTEditTemplate);
            this.splitContainer1.Panel1.Controls.Add(this.label6);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.RTGeneratedCode);
            this.splitContainer1.Panel2.Controls.Add(this.label7);
            this.splitContainer1.Size = new System.Drawing.Size(843, 469);
            this.splitContainer1.SplitterDistance = 234;
            this.splitContainer1.TabIndex = 5;
            // 
            // RTEditTemplate
            // 
            this.RTEditTemplate.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RTEditTemplate.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RTEditTemplate.Location = new System.Drawing.Point(6, 16);
            this.RTEditTemplate.MinimumSize = new System.Drawing.Size(25, 25);
            this.RTEditTemplate.Name = "RTEditTemplate";
            this.RTEditTemplate.Size = new System.Drawing.Size(834, 215);
            this.RTEditTemplate.TabIndex = 1;
            this.RTEditTemplate.Text = "";
            this.RTEditTemplate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.RTEditTemplate_KeyPress);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(72, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Edit Template";
            // 
            // RTGeneratedCode
            // 
            this.RTGeneratedCode.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RTGeneratedCode.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RTGeneratedCode.Location = new System.Drawing.Point(6, 16);
            this.RTGeneratedCode.MinimumSize = new System.Drawing.Size(25, 25);
            this.RTGeneratedCode.Name = "RTGeneratedCode";
            this.RTGeneratedCode.Size = new System.Drawing.Size(834, 212);
            this.RTGeneratedCode.TabIndex = 1;
            this.RTGeneratedCode.Text = "";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(85, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "Generated Code";
            // 
            // BtnGenerateCode
            // 
            this.BtnGenerateCode.Location = new System.Drawing.Point(91, 34);
            this.BtnGenerateCode.Name = "BtnGenerateCode";
            this.BtnGenerateCode.Size = new System.Drawing.Size(95, 23);
            this.BtnGenerateCode.TabIndex = 4;
            this.BtnGenerateCode.Text = "Generate";
            this.BtnGenerateCode.UseVisualStyleBackColor = true;
            this.BtnGenerateCode.Click += new System.EventHandler(this.BtnGenerateCode_Click);
            // 
            // BtnSaveCodeTemplate
            // 
            this.BtnSaveCodeTemplate.Location = new System.Drawing.Point(195, 34);
            this.BtnSaveCodeTemplate.Name = "BtnSaveCodeTemplate";
            this.BtnSaveCodeTemplate.Size = new System.Drawing.Size(95, 23);
            this.BtnSaveCodeTemplate.TabIndex = 3;
            this.BtnSaveCodeTemplate.Text = "Save Template";
            this.BtnSaveCodeTemplate.UseVisualStyleBackColor = true;
            this.BtnSaveCodeTemplate.Click += new System.EventHandler(this.BtnSaveCodeTemplate_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 10);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(79, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Code Template";
            // 
            // CBCodeTemplate
            // 
            this.CBCodeTemplate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CBCodeTemplate.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.CBCodeTemplate.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.CBCodeTemplate.FormattingEnabled = true;
            this.CBCodeTemplate.Location = new System.Drawing.Point(91, 7);
            this.CBCodeTemplate.MinimumSize = new System.Drawing.Size(50, 0);
            this.CBCodeTemplate.Name = "CBCodeTemplate";
            this.CBCodeTemplate.Size = new System.Drawing.Size(761, 21);
            this.CBCodeTemplate.TabIndex = 1;
            this.CBCodeTemplate.SelectedIndexChanged += new System.EventHandler(this.CBCodeTemplate_SelectedIndexChanged);
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
            // RTDebug
            // 
            this.RTDebug.Location = new System.Drawing.Point(6, 6);
            this.RTDebug.Name = "RTDebug";
            this.RTDebug.Size = new System.Drawing.Size(846, 526);
            this.RTDebug.TabIndex = 0;
            this.RTDebug.Text = "";
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
            this.MinimumSize = new System.Drawing.Size(100, 100);
            this.Name = "FormMain";
            this.Text = "Code Stencil 2016";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.TPDBServer.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.GBServerDetails.ResumeLayout(false);
            this.GBServerDetails.PerformLayout();
            this.TPTable.ResumeLayout(false);
            this.TPTable.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.TPColumn.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.TPGenerate.ResumeLayout(false);
            this.TPGenerate.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.TPDebug.ResumeLayout(false);
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
        private System.Windows.Forms.ComboBox CBDatabase;
        private System.Windows.Forms.Panel PSQLServerConnStatus;
        private System.Windows.Forms.RichTextBox RTDebug;
        private System.Windows.Forms.TabPage TPGenerate;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.RichTextBox RTEditTemplate;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.RichTextBox RTGeneratedCode;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button BtnGenerateCode;
        private System.Windows.Forms.Button BtnSaveCodeTemplate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox CBCodeTemplate;
        private System.Windows.Forms.Button BtnDeleteTemplate;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckedListBox CLBDatabases;
        private System.Windows.Forms.Button BtnDBInvertSelection;
        private System.Windows.Forms.Button BtnDBSelectNone;
        private System.Windows.Forms.Button BtnDBSelectAll;
        private System.Windows.Forms.TabPage TPTable;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckedListBox CLBTables;
        private System.Windows.Forms.Button BtnTableInvertSelection;
        private System.Windows.Forms.Button BtnTableSelectNone;
        private System.Windows.Forms.Button BtnTableSelectAll;
        private System.Windows.Forms.ComboBox CBTable;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TabPage TPColumn;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button BtnColumnInvertSelection;
        private System.Windows.Forms.Button BtnColumnSelectNone;
        private System.Windows.Forms.Button BtnColumnSelectAll;
        private System.Windows.Forms.ListView LVColumns;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
    }
}

