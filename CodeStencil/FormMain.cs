using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using UtilityClasses;

namespace CodeStencil
{
    public enum enGenerateSource
    {
        Databases = 1,
        Tables = 2,
        StoredProcs = 3,
        Columns = 4
    }

    public partial class FormMain : Form
    {
        private const string BlockRepeatFirstLine = "!";
        private const string BlockRepeatSubsequentLines = ".";
        private const string MacroIncrementingNumber = "@i";
        private const string MacroFieldList = "@l";
        private const string MacroDatabaseName = "@d";
        private const string MacroTableName = "@t";
        private const string MacroTableSchemaName = "@sch";
        private const string MacroColumnName = "@f";
        private const string MacroColMaxLength = "@s";
        private const string MacroColumnType = "@y";
        private const string MacroColumnNull = "@n";
        private const string Key = @"W""W#j`@dG3y4vAeXczj_9%s'x^GiAj<{N\h`SOJY$m>Y{9hOfW""7q^f,&{1OX;*";
        private static AppSettings Settings;
        private string ApplicationFolder;
        private bool ChangesMadeSinceLastSave;
        private List<string> CodeMacros;
        private List<string> CodeTemplate;
        private string CommaDelimitedFieldNames;
        private int? CurrentCodeTemplateIndex;
        private string CurrentDatabase;
        private string CurrentTable;
        private string CurrentTableSchema;
        private SqlConnection db;
        private string DBConnectionsFolder;
        private List<string> FieldNullable;
        private List<int> FieldPrecisions;
        private List<int> FieldScales;
        private List<int> FieldSizes;
        private List<string> FieldsList;
        private List<string> FieldTypes;
        private enGenerateSource? GenerateSource;
        private List<string> IncludePaths;
        private bool Initialising;
        private int? PreviousCodeTemplateIndex;
        private bool ServerCannotSwitchDatabases;
        private bool TemplateChangeReversed;
        private bool TemplateLoaded;
        private string TemplatesFolder;

        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            Text = "Code Stencil - Version " + Assembly.GetExecutingAssembly().GetName().Version;

            // Load settings from ini file
            Settings = AppSettings.Load();

            //TSQLServerNameOrIP.Text = Settings.SQLServerNameOrIP;
            //TSQLUserName.Text = Settings.SQLUserName;
            //TSQLPassword.Text = Settings.SQLPassword;

            // Initialise string list with all replaceable macros
            CodeMacros = new List<string>();
            CodeMacros.Add(MacroDatabaseName);
            CodeMacros.Add(MacroTableName);
            CodeMacros.Add(MacroTableSchemaName);
            CodeMacros.Add(MacroColumnName);
            CodeMacros.Add(MacroFieldList);
            CodeMacros.Add(MacroColMaxLength);
            CodeMacros.Add(MacroColumnType);
            CodeMacros.Add(MacroColumnNull);
            CodeMacros.Add(MacroIncrementingNumber);
            StringComparerLengthAlpha sc = new StringComparerLengthAlpha();
            CodeMacros.Sort(sc);

            FieldsList = new List<string>();
            FieldSizes = new List<int>();
            FieldTypes = new List<string>();
            FieldPrecisions = new List<int>();
            FieldScales = new List<int>();
            FieldNullable = new List<string>();
            CodeTemplate = new List<string>();
            IncludePaths = new List<string>();
            // Get templates folder - by default, folder called Templates inside the executable folder.
            ApplicationFolder = Assembly.GetExecutingAssembly().GetName().CodeBase;
            ApplicationFolder = ApplicationFolder.Substring(8).Replace("/", "\\");
            ApplicationFolder = Path.GetDirectoryName(ApplicationFolder);
            TemplatesFolder = Path.Combine(ApplicationFolder, "Templates");
            DBConnectionsFolder = Path.Combine(ApplicationFolder, "DBConnections");

            if (!Directory.Exists(TemplatesFolder)) Directory.CreateDirectory(TemplatesFolder);

            if (!Directory.Exists(DBConnectionsFolder)) Directory.CreateDirectory(DBConnectionsFolder);

            PopulateCodeTemplates();
            PopulateDatabaseConnections();

            if (!string.IsNullOrEmpty(Settings.LastUsedDBConnection) &&
                CBConnectionName.Items.Contains(Settings.LastUsedDBConnection))
            {
                CBConnectionName.SelectedIndex = CBConnectionName.Items.IndexOf(Settings.LastUsedDBConnection);
                LoadDBConnection();
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExitProgram();
        }

        private void ExitProgram()
        {
            Application.Exit();
        }

        private void BtnSQLServerConnect_Click(object sender, EventArgs e)
        {
            SQLServer_Connect();
        }

        private void SQLServer_Connect(string DefaultDatabase = "master")
        {
            // Disable the dropdown list of databases, to be enabled upon successful population
            CBDatabase.Enabled = false;

            // Disable the list of databases to generate code for
            CLBDatabases.Enabled = false;

            // If we have an open connection, close it and dispose of it
            if (db != null)
            {
                if (db.State == ConnectionState.Open) db.Close();

                db.Dispose();
                db = null;
            }

            // Specify the provider name, server and database.
            string providerName = "System.Data.SqlClient";

            // Initialize the connection string builder for the
            // underlying provider.
            SqlConnectionStringBuilder sqlBuilder =
                new SqlConnectionStringBuilder();

            // Set the properties for the data source.
            sqlBuilder.DataSource = TSQLServerNameOrIP.Text;
            sqlBuilder.InitialCatalog = DefaultDatabase;
            if (!string.IsNullOrEmpty(TSQLUserName.Text) || !string.IsNullOrEmpty(TSQLPassword.Text))
            {
                sqlBuilder.IntegratedSecurity = false;
                sqlBuilder.UserID = TSQLUserName.Text;
                sqlBuilder.Password = TSQLPassword.Text;
            }
            else
            {
                sqlBuilder.IntegratedSecurity = true;
            }

            // Build the SqlConnection connection string.
            string providerString = sqlBuilder.ToString();

            db = new SqlConnection(sqlBuilder.ToString());

            try
            {
                db.Open();
                PSQLServerConnStatus.BackColor = Color.Green;
                DebugLog("Connected to Server: " + TSQLServerNameOrIP.Text, true);
                Settings.IsServerConnected = true;
                Settings.LastUsedDBConnection = CBConnectionName.Text;
                Settings.Save();

                PopulateDatabasesList();
            }
            catch (Exception ex)
            {
                PSQLServerConnStatus.BackColor = Color.Red;
                DebugLog("Error Connecting to Server: " + ex.Message, true);
            }
        }

        /// <summary>
        ///     Get list of databases on server from master.
        /// </summary>
        public void PopulateDatabasesList()
        {
            CBDatabase.Items.Clear();
            CLBDatabases.Items.Clear();
            SqlConnection connDBList;
            bool ShouldCloseConnection = false;

            if (ServerCannotSwitchDatabases)
            {
                ShouldCloseConnection = true;
                connDBList = OpenSQLServerConnection();
            }
            else
            {
                connDBList = db;
            }

            // Set up a command with the given query and associate
            // this with the current connection.
            using (SqlCommand cmd = new SqlCommand("SELECT name from master.sys.databases ORDER BY name", connDBList))
            {
                using (IDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        CBDatabase.Items.Add(dr[0].ToString());
                        CLBDatabases.Items.Add(dr[0].ToString());
                    }
                }
            }

            if (ShouldCloseConnection)
            {
                connDBList.Close();
                connDBList.Dispose();
            }

            CBDatabase.Enabled = true;
            CLBDatabases.Enabled = true;
        }

        public void DebugLog(string Message, bool UpdateStatusBar = false)
        {
            RTDebug.AppendText(Message + "\r\n");
            if (UpdateStatusBar) SBLabel.Text = Message;
        }

        private void CBDatabase_SelectedValueChanged(object sender, EventArgs e)
        {
            SQLChangeDatabase();
        }

        public void SQLChangeDatabase()
        {
            bool DBChangedSuccessfully = false;
            // Clear out tables and fields
            CBTable.Enabled = false;
            CLBTables.Enabled = false;
            CBTable.Items.Clear();
            CLBTables.Items.Clear();

            int selectedIndex = CBDatabase.SelectedIndex;
            object selectedItem = CBDatabase.SelectedItem;
            string DatabaseName;

            if (selectedIndex != -1)
                DatabaseName = selectedItem.ToString();
            else
                DatabaseName = "master";

            try
            {
                if (!ServerCannotSwitchDatabases)
                    try
                    {
                        db.ChangeDatabase(DatabaseName);
                        DBChangedSuccessfully = true;
                    }
                    catch (Exception e)
                    {
                        if (e.Message.Contains("USE statement is not supported to switch between databases"))
                            ServerCannotSwitchDatabases = true;
                        else
                            DebugLog("Error trying to change database: " + e.Message, true);
                    }

                if (!DBChangedSuccessfully)
                    try
                    {
                        SQLServer_Connect(DatabaseName);
                        DBChangedSuccessfully = true;
                        ServerCannotSwitchDatabases = true;
                    }
                    catch (Exception e)
                    {
                        DebugLog("Error trying to change database: " + e.Message, true);
                    }

                if (DBChangedSuccessfully)
                {
                    CurrentDatabase = DatabaseName;
                    if (!Initialising)
                    {
                        Settings.LastUsedDatabase = DatabaseName;
                        Settings.LastUsedTable = "";
                        Settings.LastSelectedColumns = "";
                        Settings.Save();
                    }

                    DebugLog("Connected to database: " + DatabaseName, true);
                }
            }
            catch (Exception ex)
            {
                DebugLog("Error trying to change database: " + ex.Message, true);
            }

            PopulateTables();
        }

        // Populate the dropdown combobox and the checked list box of tables
        private void PopulateTables()
        {
            // Set up a command with the given query and associate
            // this with the current connection.
            using (SqlCommand cmd = new SqlCommand("SELECT name from sys.tables ORDER BY name", db))
            {
                using (IDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        CBTable.Items.Add(dr[0].ToString());
                        CLBTables.Items.Add(dr[0].ToString());
                    }
                }
            }

            CBTable.Enabled = true;
            CLBTables.Enabled = true;
        }

        private void BtnDBSelectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < CLBDatabases.Items.Count; i++) CLBDatabases.SetItemChecked(i, true);
        }

        private void BtnDBSelectNone_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < CLBDatabases.Items.Count; i++) CLBDatabases.SetItemChecked(i, false);
        }

        private void BtnDBInvertSelection_Click(object sender, EventArgs e)
        {
            bool CheckedState;

            for (int i = 0; i < CLBDatabases.Items.Count; i++)
            {
                CheckedState = CLBDatabases.GetItemChecked(i);
                CLBDatabases.SetItemChecked(i, !CheckedState);
            }
        }

        private void BtnTableSelectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < CLBTables.Items.Count; i++) CLBTables.SetItemChecked(i, true);
        }

        private void BtnTableSelectNone_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < CLBTables.Items.Count; i++) CLBTables.SetItemChecked(i, false);
        }

        private void BtnTableInvertSelection_Click(object sender, EventArgs e)
        {
            bool CheckedState;

            for (int i = 0; i < CLBTables.Items.Count; i++)
            {
                CheckedState = CLBTables.GetItemChecked(i);
                CLBTables.SetItemChecked(i, !CheckedState);
            }
        }

        private void CBTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            LVColumns.Enabled = false;

            CurrentTable = CBTable.SelectedItem.ToString();
            if (!Initialising)
            {
                Settings.LastUsedTable = CurrentTable;
                Settings.LastSelectedColumns = "";
                Settings.Save();
            }

            // Get the schema for the selected table
            using (SqlCommand cmd =
                new SqlCommand(
                    "SELECT s.name FROM sys.tables t INNER JOIN sys.schemas s ON s.schema_id = t.schema_id WHERE t.name = '" +
                    CurrentTable + "'", db))
            {
                using (IDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read()) CurrentTableSchema = dr[0].ToString();
                }

                PopulateColumns();
                GenerateSource = enGenerateSource.Columns;
            }
        }

        private void PopulateColumns()
        {
            LVColumns.Items.Clear();
            ListViewItem item;

            // Set up a command with the given query and associate
            // this with the current connection.
            using (SqlCommand cmd = new SqlCommand(
                "SELECT name, TYPE_NAME(system_type_id) AS type, max_length, precision, scale, is_nullable, is_identity, OBJECT_DEFINITION(default_object_id) AS DefaultValue from sys.columns WHERE object_id = OBJECT_ID('" +
                CurrentTableSchema + "." + CurrentTable + "') ORDER BY column_id", db))
            {
                using (IDataReader dr = cmd.ExecuteReader())
                {
                    FieldsList.Clear();
                    FieldTypes.Clear();
                    FieldSizes.Clear();
                    FieldPrecisions.Clear();
                    FieldScales.Clear();
                    while (dr.Read())
                    {
                        item = new ListViewItem();
                        if (Settings.ColumnAutoSelectAll) item.Checked = true;
                        item.Text = dr[0].ToString(); // Column Name
                        FieldsList.Add(dr[0].ToString());
                        item.SubItems.Add(dr[1].ToString()); // Type
                        FieldTypes.Add(dr[1].ToString());
                        item.SubItems.Add(dr[2].ToString()); // Max Length
                        FieldSizes.Add(Convert.ToInt32(dr[2]));
                        item.SubItems.Add(dr[3].ToString()); // Precision
                        FieldPrecisions.Add(Convert.ToInt32(dr[3]));
                        item.SubItems.Add(dr[4].ToString()); // Scale
                        FieldScales.Add(Convert.ToInt32(dr[4]));
                        item.SubItems.Add(dr[5].ToString()); // Nullable
                        FieldNullable.Add(dr[5].ToString() == "True" ? "NULL" : "NOT NULL");
                        item.SubItems.Add(dr[6].ToString()); // Identity
                        // Default Value
                        if (dr[7] == null)
                            item.SubItems.Add("(NULL)");
                        else
                            item.SubItems.Add(dr[7].ToString());
                        LVColumns.Items.Add(item);
                    }
                }
            }

            ResizeListViewColumns(LVColumns);
            LVColumns.Enabled = true;
        }

        private void ResizeListViewColumns(ListView lv)
        {
            foreach (ColumnHeader column in lv.Columns) column.Width = -2;
        }

        private void BtnColumnSelectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < LVColumns.Items.Count; i++) LVColumns.Items[i].Checked = true;
        }

        private void BtnColumnSelectNone_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < LVColumns.Items.Count; i++) LVColumns.Items[i].Checked = false;
        }

        private void BtnColumnInvertSelection_Click(object sender, EventArgs e)
        {
            bool Checked;

            for (int i = 0; i < LVColumns.Items.Count; i++)
            {
                Checked = LVColumns.Items[i].Checked;
                LVColumns.Items[i].Checked = !Checked;
            }
        }

        private void BtnSaveCodeTemplate_Click(object sender, EventArgs e)
        {
            SaveTemplate();
        }

        private void SaveTemplate()
        {
            string TemplateName, TemplateFileName;
            TemplateName = CBCodeTemplate.Text;

            if (string.IsNullOrEmpty(TemplateName))
            {
                MessageBox.Show("Please enter a name for the template, then click Save.");
                return;
            }

            TemplateFileName = TemplateName + ".txt";

            TemplateFileName = Path.Combine(TemplatesFolder, TemplateFileName);

            if (File.Exists(TemplateFileName))
            {
                DialogResult confirmResult = MessageBox.Show("Are you sure to overwrite this code template?",
                    "Confirm Overwrite",
                    MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {
                    File.WriteAllText(TemplateFileName, RTEditTemplate.Text);
                    TemplateLoaded =
                        true; // We now have a valid template and we want to track changes, even if no template was loaded until now.
                }
            }
            else
            {
                File.WriteAllText(TemplateFileName, RTEditTemplate.Text);
                TemplateLoaded =
                    true; // We now have a valid template and we want to track changes, even if no template was loaded until now.
                PopulateCodeTemplates();
            }

            ChangesMadeSinceLastSave = false;
        }

        private void PopulateCodeTemplates()
        {
            CBCodeTemplate.Enabled = false;
            CBCodeTemplate.Items.Clear();
            string TemplateName;

            foreach (string TemplateFileName in Directory.EnumerateFiles(TemplatesFolder, "*.txt"))
            {
                TemplateName = Path.GetFileNameWithoutExtension(TemplateFileName);
                CBCodeTemplate.Items.Add(TemplateName);
            }

            CBCodeTemplate.Enabled = true;
        }

        private void PopulateDatabaseConnections()
        {
            CBConnectionName.Enabled = false;
            CBConnectionName.Items.Clear();
            string DBConnectionName;

            foreach (string DBConnectionFileName in Directory.EnumerateFiles(DBConnectionsFolder, "*.ini"))
            {
                DBConnectionName = Path.GetFileNameWithoutExtension(DBConnectionFileName);
                CBConnectionName.Items.Add(DBConnectionName);
            }

            CBConnectionName.Enabled = true;
        }

        private void CBCodeTemplate_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool ProceedWithChange = false;
            if (TemplateLoaded)
            {
                if (!TemplateChangeReversed)
                {
                    if (ChangesMadeSinceLastSave)
                    {
                        DialogResult confirmResult = MessageBox.Show(
                            "You have made changes to the code template. Are you sure you want to discard them and load another template?",
                            "Confirm Load Without Saving",
                            MessageBoxButtons.YesNo);
                        if (confirmResult != DialogResult.Yes)
                        {
                            TemplateChangeReversed = true;
                            if (PreviousCodeTemplateIndex != null)
                                CBCodeTemplate.SelectedIndex = PreviousCodeTemplateIndex.GetValueOrDefault();
                            TemplateChangeReversed = false;
                        }
                        else
                        {
                            ProceedWithChange = true;
                        }
                    }
                    else
                    {
                        ProceedWithChange = true;
                    }
                }
            }
            else
            {
                ProceedWithChange = true;
            }

            if (ProceedWithChange)
            {
                PreviousCodeTemplateIndex = CurrentCodeTemplateIndex;
                CurrentCodeTemplateIndex = CBCodeTemplate.SelectedIndex;
                TemplateChangeReversed = false;
                LoadTemplate();
            }
        }

        private void LoadTemplate()
        {
            string TemplateName, TemplateFileName;
            TemplateName = CBCodeTemplate.Text;

            if (string.IsNullOrEmpty(TemplateName)) return;

            TemplateFileName = TemplateName + ".txt";
            TemplateFileName = Path.Combine(TemplatesFolder, TemplateFileName);

            if (File.Exists(TemplateFileName))
            {
                CodeTemplate.Clear();
                IncludePaths.Clear();
                RTEditTemplate.Text = File.ReadAllText(TemplateFileName);
                TemplateLoaded = true;
                ChangesMadeSinceLastSave = false;
                if (!Initialising)
                {
                    Settings.LastUsedTemplate = CBCodeTemplate.Text;
                    Settings.Save();
                }
            }
            else
            {
                MessageBox.Show("The template you are trying to load no longer exists.");
                PopulateCodeTemplates();
            }
        }

        private void LoadDBConnection()
        {
            string DBConnectionName, DBConnectionFileName;
            DBConnectionName = CBConnectionName.Text;

            if (string.IsNullOrEmpty(DBConnectionName)) return;

            DBConnectionFileName = DBConnectionName + ".ini";
            DBConnectionFileName = Path.Combine(DBConnectionsFolder, DBConnectionFileName);

            if (File.Exists(DBConnectionFileName))
            {
                IniFile ini = new IniFile();
                ini.Load(DBConnectionFileName);
                bool DefaultUsed = false;
                HidePassword();
                TSQLServerNameOrIP.Text = ini.GetKeyValue("DBConnectionSettings", "SQLServerNameOrIP");
                TSQLUserName.Text = StringCipher.Decrypt(ini.GetKeyValue("DBConnectionSettings", "SQLUserName"), Key);
                TSQLPassword.Text = StringCipher.Decrypt(ini.GetKeyValue("DBConnectionSettings", "SQLPassword"), Key);
                ServerCannotSwitchDatabases = Convert.ToBoolean(ini.GetKeyValueOrDefault("DBConnectionSettings",
                    "ServerCannotSwitchDatabases", "False", ref DefaultUsed));
                if (DefaultUsed) ini.Save(DBConnectionFileName);
                Settings.LastUsedDBConnection = DBConnectionName;
                Settings.Save();
            }
            else
            {
                MessageBox.Show("The connection you are trying to load no longer exists.");
                PopulateDatabaseConnections();
            }
        }

        private void SaveDBConnection()
        {
            string DBConnectionName, DBConnectionFileName;
            DBConnectionName = CBConnectionName.Text;

            if (string.IsNullOrEmpty(DBConnectionName))
            {
                MessageBox.Show("Please enter a name for the connection.");
                return;
            }

            DBConnectionFileName = DBConnectionName + ".ini";
            DBConnectionFileName = Path.Combine(DBConnectionsFolder, DBConnectionFileName);

            if (File.Exists(DBConnectionFileName))
            {
                DialogResult confirmResult = MessageBox.Show("Are you sure to overwrite this database connection?",
                    "Confirm Overwrite",
                    MessageBoxButtons.YesNo);
                if (confirmResult != DialogResult.Yes) return;
            }

            IniFile ini = new IniFile();
            ini.AddSection("DBConnectionSettings").AddKey("SQLServerNameOrIP").Value = TSQLServerNameOrIP.Text;
            ini.AddSection("DBConnectionSettings").AddKey("ServerCannotSwitchDatabases").Value =
                ServerCannotSwitchDatabases.ToString();
            ini.AddSection("DBConnectionSettings").AddKey("SQLUserName").Value =
                StringCipher.Encrypt(TSQLUserName.Text, Key);
            ini.AddSection("DBConnectionSettings").AddKey("SQLPassword").Value =
                StringCipher.Encrypt(TSQLPassword.Text, Key);
            ini.Save(DBConnectionFileName);
            HidePassword();
            PopulateDatabaseConnections();
        }

        private void RTEditTemplate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (TemplateLoaded) ChangesMadeSinceLastSave = true;
        }

        private void BtnDeleteTemplate_Click(object sender, EventArgs e)
        {
            if (CBCodeTemplate.SelectedIndex == -1)
            {
                MessageBox.Show("No template selected. Please select a template to delete.");
                return;
            }

            string TemplateName, TemplateFileName;
            TemplateName = CBCodeTemplate.Text;

            TemplateFileName = TemplateName + ".txt";

            TemplateFileName = Path.Combine(TemplatesFolder, TemplateFileName);

            if (File.Exists(TemplateFileName))
            {
                DialogResult confirmResult = MessageBox.Show("Are you sure to delete this code template?",
                    "Confirm Delete",
                    MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {
                    File.Delete(TemplateFileName);
                    RTEditTemplate.Clear();
                    PopulateCodeTemplates();
                }
            }
            else
            {
                MessageBox.Show("The template you are trying to delete no longer exists.");
                PopulateCodeTemplates();
            }

            ChangesMadeSinceLastSave = false;
        }

        private void BtnGenerateCode_Click(object sender, EventArgs e)
        {
            GenerateCode();
        }

        private void IncludeTemplates()
        {
            CodeTemplate.Clear();
            IncludePaths.Clear();
            bool Done = false;
            bool IncludesFound = false;
            long iCurrentLine = 0;

            //while (!Done)
            //{
            //    if (string.IsNullOrEmpty(CodeTemplate.ToString()))
            //    {
            //        if (!string.IsNullOrEmpty(RTEditTemplate.ToString()))
            //        {
            //            CodeTemplate.AddRange(RTEditTemplate.Lines);
            //        }
            //        else
            //        {
            //            Done = true;
            //        }
            //    }

            //    Done = true;
            //}

            // Initialise in-memory code template by copying the text from the window.
            CodeTemplate.AddRange(RTEditTemplate.Lines);
        }

        private void GenerateCode()
        {
            IncludeTemplates();

            string GeneratedCode = "";
            int SpacesToIndent = 0;
            List<string> FieldsList = new List<string>();

            int iGCCurrentLine = 0;
            int iGCNumLines = 1; // Number of repeating lines
            bool InRepeatingSection = false;
            bool FirstLineIsDifferent = false;
            bool OnDifferentFirstLine = false;
            bool FirstLineGenerated = false;

            // Loop through lines
            while (iGCCurrentLine < CodeTemplate.Count())
            {
                iGCNumLines = 1;
                string SGCCurrentLine = CodeTemplate[iGCCurrentLine];

                // Process non-empty lines
                if (SGCCurrentLine.Length != 0)
                {
                    // Check for repeating lines
                    if (SGCCurrentLine.Substring(0, 1) == BlockRepeatSubsequentLines ||
                        SGCCurrentLine.Substring(0, 1) == BlockRepeatFirstLine) //# If this line is a repeating line
                    {
                        InRepeatingSection = true;
                        if (SGCCurrentLine.Substring(0, 1) == BlockRepeatFirstLine)
                        {
                            FirstLineIsDifferent = true;
                            OnDifferentFirstLine = true;
                        }

                        iGCNumLines = GetNumberOfLinesThatRepeat(iGCCurrentLine);

                        bool FirstSelectedField = true;

                        int iIncrementingNumber = 1;

                        //# Iterate through fields
                        for (int iGC = 0; iGC < LVColumns.Items.Count; iGC++)
                            // Generate if the field is checked
                            if (LVColumns.Items[iGC].Checked)
                            {
                                int iCurLine = 1;
                                //# Iterate through the repeating lines in the template
                                for (int jGC = iGCCurrentLine; jGC < iGCCurrentLine + iGCNumLines; jGC++)
                                {
                                    if (!FirstLineIsDifferent
                                        || FirstLineIsDifferent && OnDifferentFirstLine && FirstSelectedField
                                        || FirstLineIsDifferent && !OnDifferentFirstLine && !FirstSelectedField &&
                                        iCurLine > 1)
                                    {
                                        SGCCurrentLine = CodeTemplate[jGC];
                                        if (ShouldGenerateLine(ref SGCCurrentLine, iGC))
                                        {
                                            GeneratedCode =
                                                GeneratedCode + ParseThisLine(SGCCurrentLine, iGC, SpacesToIndent, iIncrementingNumber) +
                                                "\r\n";
                                            iIncrementingNumber++;
                                        }
                                    }

                                    iCurLine++;
                                    OnDifferentFirstLine = false;
                                }

                                FirstSelectedField = false;
                            }
                    }
                    else
                    {
                        InRepeatingSection = false;
                        FirstLineIsDifferent = false;
                        FirstLineGenerated = false;
                        GeneratedCode = GeneratedCode + ParseThisLine(SGCCurrentLine, 0, SpacesToIndent) + "\r\n";
                    }
                }
                else
                {
                    GeneratedCode = GeneratedCode + "\r\n";
                }

                iGCCurrentLine = iGCCurrentLine + iGCNumLines;
            }

            RTGeneratedCode.Text = GeneratedCode;

            Clipboard.SetText(RTGeneratedCode.Text);

            //return GeneratedCode;
        }

        //########  FUNCTION TO RETURN THE FIRST MACRO FOUND ON A TEMPLATE LINE  #########
        private string FindFirstMacro(string SFFMTemplate, List<string> CodeMacros)
        {
            string SReturn = ""; //# Macro which is first in the string gets stuck in here
            int iFFMPos1 = 0;
            int iFFMPos2 = 0;

            iFFMPos1 = SFFMTemplate.IndexOf(CodeMacros[0]);
            if (iFFMPos1 != -1) SReturn = CodeMacros[0];

            for (int iFFM = 0; iFFM < CodeMacros.Count; iFFM++)
            {
                iFFMPos2 = SFFMTemplate.IndexOf(CodeMacros[iFFM]);
                if (iFFMPos1 != -1)
                {
                    if (iFFMPos2 != -1 && iFFMPos2 < iFFMPos1) //# if macro was found and it was closer to start
                    {
                        iFFMPos1 = iFFMPos2; //# set new first macro position
                        SReturn = CodeMacros[iFFM];
                    }
                }
                else if (iFFMPos2 != -1)
                {
                    iFFMPos1 = iFFMPos2;
                    SReturn = CodeMacros[iFFM];
                }
            }

            return SReturn;
        }

        //########  FUNCTION TO REPLACE THE CODE MACROS WITH APPROPRIATE VALUES  #########
        private string ParseThisLine(string STemplate, int iPTLCurrentField, int iIndentSpaces,
            int iIncrementingNumber = 1)
        {
            string SParsedString = "";
            string SMacro = "";
            int iPTLCurrentChar = 0; //# The current character
            int iPTLNumChars = 1; //# The number of characters to copy
            int iPTLPos = 0; //# Position of macro string

            SMacro = FindFirstMacro(STemplate, CodeMacros);

            while (iPTLCurrentChar < STemplate.Length && SMacro != "")
            {
                //# Copy all characters up till macro
                if (SMacro != "")
                {
                    iPTLPos = STemplate.IndexOf(SMacro);
                    iPTLNumChars = iPTLPos - iPTLCurrentChar; //# Get from current to just before macro
                    SParsedString += STemplate.Substring(iPTLCurrentChar, iPTLNumChars);

                    //# Replace the macro string
                    switch (SMacro)
                    {
                        case MacroIncrementingNumber:
                            SParsedString += iIncrementingNumber;
                            break;
                        case MacroTableName:
                            SParsedString += CBTable.Text;
                            break;
                        case MacroTableSchemaName:
                            SParsedString += CurrentTableSchema;
                            break;
                    }

                    if (FieldsList.Count > 0)
                    {
                        switch (SMacro)
                        {
                            case MacroColMaxLength:
                                SParsedString += FieldSizes[iPTLCurrentField];
                                break;
                            case MacroColumnName:
                                SParsedString += FieldsList[iPTLCurrentField];
                                break;
                            case MacroFieldList:
                                SParsedString += CommaDelimitedFieldNames;
                                break;
                            case MacroDatabaseName:
                                SParsedString += CBDatabase.Text;
                                break;
                            case MacroColumnType:
                            {
                                SParsedString += FieldTypes[iPTLCurrentField];
                                string sColumnSize = "";
                                if (FieldSizes[iPTLCurrentField] == -1)
                                    sColumnSize = "max";
                                else
                                    sColumnSize = FieldSizes[iPTLCurrentField].ToString();
                                switch (FieldTypes[iPTLCurrentField])
                                {
                                    case "varchar":
                                        SParsedString += "(" + sColumnSize + ")";
                                        break;
                                    case "nvarchar":
                                        SParsedString += "(" + sColumnSize + ")";
                                        break;
                                    case "char":
                                        SParsedString += "(" + sColumnSize + ")";
                                        break;
                                    case "nchar":
                                        SParsedString += "(" + sColumnSize + ")";
                                        break;
                                    case "binary":
                                        SParsedString += "(" + sColumnSize + ")";
                                        break;
                                    case "varbinary":
                                        SParsedString += "(" + sColumnSize + ")";
                                        break;
                                    case "numeric":
                                        SParsedString += "(" + FieldPrecisions[iPTLCurrentField] + "," +
                                                        FieldScales[iPTLCurrentField] + ")";
                                        break;
                                    case "decimal":
                                        SParsedString += "(" + FieldPrecisions[iPTLCurrentField] + "," +
                                                        FieldScales[iPTLCurrentField] + ")";
                                        break;
                                }

                                break;
                            }
                            case MacroColumnNull:
                                SParsedString += FieldNullable[iPTLCurrentField];
                                break;
                        }
                    }

                    //# Trim the copied characters from the template string
                    iPTLPos = iPTLPos + SMacro.Length;
                    iPTLNumChars = STemplate.Length - iPTLPos;
                    if (iPTLPos < STemplate.Length)
                    {
                        if (iPTLPos + iPTLNumChars >= STemplate.Length) iPTLNumChars = STemplate.Length - iPTLPos;
                        STemplate = STemplate.Substring(iPTLPos, iPTLNumChars);
                    }
                    else
                    {
                        STemplate = "";
                    }

                    iPTLCurrentChar = 0;
                }

                SMacro = FindFirstMacro(STemplate, CodeMacros);
            }

            //# Add the rest of the template line after all macros.
            if (STemplate.Length != 0) SParsedString += STemplate;

            //# Chop leading "." or "!" if present
            if (SParsedString != "")
                if (SParsedString.Substring(0, 1) == BlockRepeatSubsequentLines || SParsedString.Substring(0, 1) == "!")
                    SParsedString = SParsedString.Substring(1);

            //# Add spaces at beginning of line to indent
            if (iIndentSpaces > 0)
                for (int i = 0; i < iIndentSpaces; i++)
                    SParsedString = " " + SParsedString;

            if (SParsedString.Trim() == "") SParsedString = " ";

            return SParsedString;
        }

        //##########  FUNCTION TO COUNT THE REPEATING LINES IN A REPEAT BLOCK  ###########
        private int GetNumberOfLinesThatRepeat(int StartLine)
        {
            int iCount = 0;

            for (int iNLR = StartLine; iNLR < RTEditTemplate.Lines.Count(); iNLR++)
            {
                if (RTEditTemplate.Lines[iNLR].Length == 0) break;

                if (RTEditTemplate.Lines[iNLR].Substring(0, 1) == BlockRepeatSubsequentLines ||
                    RTEditTemplate.Lines[iNLR].Substring(0, 1) == BlockRepeatFirstLine)
                    iCount = iCount + 1;
                else
                    break;
            }

            return iCount;
        }

        //#####  FUNCTION TO DETERMINE WHETHER TO GENERATE THIS LINE BASED ON THE CURRENT FIELD TYPE  #####
        private bool ShouldGenerateLine(ref string STemplate, int iCurrentField)
        {
            bool Result = false;
            Regex re;
            string sFindPattern = "\\{t:(.*)\\}";

            string FieldType = FieldTypes[iCurrentField].ToUpper();
            re = new Regex(sFindPattern);
            if (re.IsMatch(STemplate))
            {
                string AllowedFieldTypesString = re.Match(STemplate).Groups[1].ToString();
                if (AllowedFieldTypesString.Length > 0)
                {
                    List<string> AllowedFieldTypesList = AllowedFieldTypesString.ToUpper().Split(',').ToList();
                    if (AllowedFieldTypesList.IndexOf(FieldType) != -1)
                        Result = true; // Found the current field type in the list of allowed types. Should generate line.
                }
                else
                {
                    Result = false; // No allowed field types found, don't generate line.
                }

                STemplate = re.Replace(STemplate, "");
            }
            else
            {
                Result = true; //# No type specifier found, always generate this line
            }

            return Result;
        }

        // If a column is selected, copy its name to the clipboard
        private void LVColumns_Click(object sender, EventArgs e)
        {
            if (LVColumns.SelectedItems.Count > 0)
            {
                string SelectedColumnName;
                SelectedColumnName = LVColumns.SelectedItems[0].Text;
                Clipboard.SetText(SelectedColumnName);
            }
        }

        private void CLBTables_Click(object sender, EventArgs e)
        {
            if (CLBTables.SelectedItems.Count > 0)
            {
                string SelectedColumnName;
                SelectedColumnName = CLBTables.SelectedItems[0].ToString();
                Clipboard.SetText(SelectedColumnName);
            }
        }

        private void CBConnectionName_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDBConnection();
        }

        private void BtnSaveConnectionDetails_Click(object sender, EventArgs e)
        {
            SaveDBConnection();
        }

        private void BtnRepeatingLine_Click(object sender, EventArgs e)
        {
            ToggleRepeatingLines();
        }

        /// <summary>
        ///     Add or remove repeating indicator from all selected lines.
        /// </summary>
        /// <remarks>If some lines are repeating and some not, all will be made repeating.</remarks>
        private void ToggleRepeatingLines()
        {
            string SelectedText = RTEditTemplate.SelectedText;

            // Check whether some lines in the selected text are already repeating lines
            if (AllLinesRepeat(SelectedText))
                SelectedText = RemoveRepeatingLines(SelectedText);
            else
                SelectedText = AddRepeatingLines(SelectedText);

            RTEditTemplate.SelectedText = SelectedText;
        }

        private bool StringContains(string StrToSearch, string RegExPattern)
        {
            bool Result = false;

            RegexOptions options = RegexOptions.IgnoreCase
                                   | RegexOptions.ECMAScript
                                   | RegexOptions.Multiline;
            Regex re = new Regex(RegExPattern, options);

            Result = re.IsMatch(StrToSearch);

            return Result;
        }

        private bool AllLinesRepeat(string StrToSearch)
        {
            bool Result = true;
            string RegExPattern = @"^(\.)?(.*)$";

            RegexOptions options = RegexOptions.IgnoreCase
                                   | RegexOptions.ECMAScript
                                   | RegexOptions.Multiline;
            Regex re = new Regex(RegExPattern, options);

            MatchCollection LinesMatched = re.Matches(StrToSearch);

            int i = 0;
            foreach (Match curLine in LinesMatched)
            {
                if (curLine.Groups[1].Value != BlockRepeatSubsequentLines)
                    if (curLine.Value != "" && i != LinesMatched.Count - 1)
                        Result = false;
                i++;
            }

            return Result;
        }

        private string RemoveRepeatingLines(string StrToSearch)
        {
            string Result = "";
            string RegExPattern = @"^(\.)?(.*)$";
            string ReplaceWith = @"${2}";

            RegexOptions options = RegexOptions.IgnoreCase
                                   | RegexOptions.ECMAScript
                                   | RegexOptions.Multiline;
            Regex re = new Regex(RegExPattern, options);

            Result = re.Replace(StrToSearch, ReplaceWith);

            return Result;
        }

        private string AddRepeatingLines(string StrToSearch)
        {
            string Result = "";
            string RegExPattern = @"^(\.)?(.*)$";
            string ReplaceWith = @".${2}";

            RegexOptions options = RegexOptions.IgnoreCase
                                   | RegexOptions.ECMAScript
                                   | RegexOptions.Multiline;
            Regex re = new Regex(RegExPattern, options);
            Regex re2 = new Regex(RegExPattern, options);

            MatchCollection LinesMatched = re.Matches(StrToSearch);

            int i = 0;
            foreach (Match curLine in LinesMatched)
            {
                if (!(curLine.Value == "" && i == LinesMatched.Count - 1))
                    Result += re2.Replace(curLine.Value, ReplaceWith);
                else
                    Result += curLine.Value;
                if (i != LinesMatched.Count - 1) Result += "\r\n";
                i++;
            }

            return Result;
        }

        // Take the selected macro from the dropdown and insert it into the code template. Regex used to strip out descriptive text.
        private void InsertMacro()
        {
            string SelectedMacro = CBMacros.Text;
            string InsertMacro = "";
            string RegExPattern = @"^(.*)\s-\s(.*)$";
            string ReplaceWith = @"${1}";


            RegexOptions options = RegexOptions.IgnoreCase
                                   | RegexOptions.ECMAScript
                                   | RegexOptions.Multiline;
            Regex re = new Regex(RegExPattern, options);

            InsertMacro = re.Replace(SelectedMacro, ReplaceWith);

            RTEditTemplate.SelectedText = RTEditTemplate.SelectedText + InsertMacro;
        }

        private void BtnInsertMacro_Click(object sender, EventArgs e)
        {
            InsertMacro();
        }

        private void LVColumns_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (!Initialising) BuildSelectedColumnsList();
        }

        private void BuildSelectedColumnsList()
        {
            List<string> SelectedColumns = new List<string>();
            foreach (ListViewItem item in LVColumns.CheckedItems) SelectedColumns.Add(item.Text);
            CommaDelimitedFieldNames = string.Join(",", SelectedColumns.ToArray());
            Settings.LastSelectedColumns = CommaDelimitedFieldNames;
            Settings.Save();
        }

        private void FormMain_Shown(object sender, EventArgs e)
        {
            Initialising = true;

            CColumnAutoSelectAll.Checked = Settings.ColumnAutoSelectAll;

            // Try to get everything set up as per the last time the app was run - ie selected database, table, columns
            if (Settings.IsServerConnected)
            {
                string DatabaseName = "master";
                if (!string.IsNullOrEmpty(Settings.LastUsedDatabase)) DatabaseName = Settings.LastUsedDatabase;

                SQLServer_Connect(DatabaseName);

                if (CBDatabase.Items.Count > 0 && !string.IsNullOrEmpty(Settings.LastUsedDatabase) &&
                    CBDatabase.Items.Contains(Settings.LastUsedDatabase))
                {
                    // Select the last used database in the dropdown list
                    HidePassword();
                    CBDatabase.SelectedIndex = CBDatabase.Items.IndexOf(Settings.LastUsedDatabase);
                }

                if (CBTable.Items.Count > 0 && !string.IsNullOrEmpty(Settings.LastUsedTable) &&
                    CBTable.Items.Contains(Settings.LastUsedTable))
                    CBTable.SelectedIndex = CBTable.Items.IndexOf(Settings.LastUsedTable);

                if (LVColumns.Items.Count > 0 && !string.IsNullOrEmpty(Settings.LastSelectedColumns))
                {
                    // Split up the list of selected columns into an array, and use that to select the columns in the listview
                    List<string> ListSelectedColumns = new List<string>();
                    ListSelectedColumns.AddRange(Settings.LastSelectedColumns.Split(',').ToList());
                    foreach (string selectedColumn in ListSelectedColumns)
                    {
                        ListViewItem item1 = LVColumns.FindItemWithText(selectedColumn);
                        if (item1 != null) item1.Checked = true;
                    }
                }

                if (CBCodeTemplate.Items.Count > 0 && !string.IsNullOrEmpty(Settings.LastUsedTemplate) &&
                    CBCodeTemplate.Items.Contains(Settings.LastUsedTemplate))
                {
                    // Select the last used code template in the dropdown list, and generate code from the template if columns selected
                    CBCodeTemplate.SelectedIndex = CBCodeTemplate.Items.IndexOf(Settings.LastUsedTemplate);
                    if (LVColumns.CheckedItems.Count > 0
                    ) // TODO: Change this condition when other sources can be used for code generation
                    {
                        BuildSelectedColumnsList();
                        GenerateCode();
                    }
                }
            }

            Initialising = false;
        }

        private void CColumnAutoSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            Settings.ColumnAutoSelectAll = CColumnAutoSelectAll.Checked;
            Settings.Save();
        }

        private void BtnExecuteQuery_Click(object sender, EventArgs e)
        {
            ExecuteQuery();
        }

        private void ExecuteQuery()
        {
            ListViewItem item;
            LVColumns.Items.Clear();
            LVData.Items.Clear();
            LVData.Columns.Clear();
            using (SqlCommand cmd = new SqlCommand(TCustomQuery.Text, db))
            {
                using (IDataReader dr = cmd.ExecuteReader())
                {
                    FieldsList.Clear();
                    FieldTypes.Clear();
                    FieldSizes.Clear();
                    FieldPrecisions.Clear();
                    FieldScales.Clear();

                    // Column names and types
                    for (int col = 0; col < dr.FieldCount; col++)
                    {
                        item = new ListViewItem();
                        if (Settings.ColumnAutoSelectAll) item.Checked = true;
                        item.Text = dr.GetName(col); // Gets the column name
                        FieldsList.Add(dr.GetName(col));
                        LVData.Columns.Add(col.ToString(), dr.GetName(col));
                        item.SubItems.Add(dr.GetDataTypeName(col));
                        FieldTypes.Add(dr.GetDataTypeName(col));
                        FieldSizes.Add(0);
                        FieldPrecisions.Add(0);
                        FieldScales.Add(0);
                        // Console.Write(dr.GetFieldType(col).ToString());    // Gets the column type
                        // Console.Write(dr.GetDataTypeName(col).ToString()); // Gets the column database type
                        LVColumns.Items.Add(item);
                    }

                    int iRow = 1;
                    int iMaxRows = 200;

                    // Populate Data
                    while (dr.Read() && iRow <= iMaxRows)
                    {
                        item = new ListViewItem();
                        for (int col = 0; col < dr.FieldCount; col++)
                        {
                            string value = dr[col].ToString();
                            if (col == 0)
                                item.Text = value;
                            else
                                item.SubItems.Add(value);
                        }

                        LVData.Items.Add(item);
                    }

                    ResizeListViewColumns(LVData);
                }
            }
        }

        private void BtnShowHidePassword_Click(object sender, EventArgs e)
        {
            ToggleShowHidePassword();
        }

        private void ToggleShowHidePassword()
        {
            if (BtnShowHidePassword.Text == "Hide")
                HidePassword();
            else
                ShowPassword();
        }

        private void HidePassword()
        {
            BtnShowHidePassword.Text = "Show";
            TSQLPassword.PasswordChar = '*';
        }

        private void ShowPassword()
        {
            BtnShowHidePassword.Text = "Hide";
            TSQLPassword.PasswordChar = '\0';
        }

        public SqlConnection OpenSQLServerConnection(string DefaultDatabase = "master")
        {
            SqlConnection dbconn;

            // Specify the provider name, server and database.
            string providerName = "System.Data.SqlClient";

            // Initialize the connection string builder for the
            // underlying provider.
            SqlConnectionStringBuilder sqlBuilder =
                new SqlConnectionStringBuilder();

            // Set the properties for the data source.
            sqlBuilder.DataSource = TSQLServerNameOrIP.Text;
            sqlBuilder.InitialCatalog = DefaultDatabase;
            if (!string.IsNullOrEmpty(TSQLUserName.Text) || !string.IsNullOrEmpty(TSQLPassword.Text))
            {
                sqlBuilder.IntegratedSecurity = false;
                sqlBuilder.UserID = TSQLUserName.Text;
                sqlBuilder.Password = TSQLPassword.Text;
            }
            else
            {
                sqlBuilder.IntegratedSecurity = true;
            }

            dbconn = new SqlConnection(sqlBuilder.ToString());

            try
            {
                dbconn.Open();
            }
            catch (Exception e)
            {
                DebugLog("Error in OpenSQLServerConnection(): " + e.Message, true);
            }

            return dbconn;
        }
    }

    internal class StringComparerLengthAlpha : IComparer<string>
    {
        public int Compare(string x, string y)
        {
            if (x == null)
            {
                if (y == null)
                    return 0;
                return -1;
            }

            // If x is not null...
            //
            if (y == null)
                // ...and y is null, x is greater.
                return 1;

            // ...and y is not null, compare the 
            // lengths of the two strings.
            //
            int retval = y.Length.CompareTo(x.Length);

            if (retval != 0)
                return retval;
            return x.CompareTo(y);
        }
    }
}