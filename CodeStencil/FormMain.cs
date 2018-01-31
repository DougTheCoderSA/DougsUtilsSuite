using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
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
        public static AppSettings Settings;
        public SqlConnection db;
        public string CurrentDatabase;
        public string CurrentTable;
        public string CurrentTableSchema;
        public List<string> CodeMacros;
        public List<string> FieldsList;
        public List<string> FieldTypes;
        public string CommaDelimitedFieldNames;
        public List<int> FieldSizes;
        public List<int> FieldPrecisions;
        public List<int> FieldScales;
        public const string LineRepeatIndicator = ".";
        public const string MacroFieldList = "@l";
        public const string MacroDatabaseName = "@d";
        public const string MacroTableName = "@t";
        public const string MacroColumnName = "@f";
        public const string MacroColMaxLength = "@s";
        public const string MacroColumnType = "@y";
        public string TemplatesFolder;
        public string DBConnectionsFolder;
        public string ApplicationFolder;
        public bool TemplateLoaded = false;
        public bool TemplateChangeReversed = false;
        public bool ChangesMadeSinceLastSave = false;
        public enGenerateSource? GenerateSource = null;
        public int? PreviousCodeTemplateIndex = null;
        public int? CurrentCodeTemplateIndex = null;
        public const string Key = "W\"W#j`@dG3y4vAeXczj_9%s'x^GiAj<{N\\h`SOJY$m>Y{9hOfW\"7q^f,&{1OX;*";
        public bool Initialising = false;

        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            Text = "Code Stencil - Version " + Assembly.GetExecutingAssembly().GetName().Version.ToString();

            // Load settings from ini file
            Settings = AppSettings.Load();

            //TSQLServerNameOrIP.Text = Settings.SQLServerNameOrIP;
            //TSQLUserName.Text = Settings.SQLUserName;
            //TSQLPassword.Text = Settings.SQLPassword;
            
            // Initialise string list with all replaceable macros
            CodeMacros = new List<string>();
            CodeMacros.Add(MacroDatabaseName);
            CodeMacros.Add(MacroTableName);
            CodeMacros.Add(MacroColumnName);
            CodeMacros.Add(MacroFieldList);
            CodeMacros.Add(MacroColMaxLength);
            CodeMacros.Add(MacroColumnType);

            FieldsList = new List<string>();
            FieldSizes = new List<int>();
            FieldTypes = new List<string>();
            FieldPrecisions = new List<int>();
            FieldScales = new List<int>();
            // Get templates folder - by default, folder called Templates inside the executable folder.
            ApplicationFolder = Assembly.GetExecutingAssembly().GetName().CodeBase;
            ApplicationFolder = ApplicationFolder.Substring(8).Replace("/", "\\");
            ApplicationFolder = Path.GetDirectoryName(ApplicationFolder);
            TemplatesFolder = Path.Combine(ApplicationFolder, "Templates");
            DBConnectionsFolder = Path.Combine(ApplicationFolder, "DBConnections");

            if (!Directory.Exists(TemplatesFolder))
            {
                Directory.CreateDirectory(TemplatesFolder);
            }

            if (!Directory.Exists(DBConnectionsFolder))
            {
                Directory.CreateDirectory(DBConnectionsFolder);
            }

            PopulateCodeTemplates();
            PopulateDatabaseConnections();

            if (!string.IsNullOrEmpty(Settings.LastUsedDBConnection) && CBConnectionName.Items.Contains(Settings.LastUsedDBConnection))
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

        private void SQLServer_Connect()
        {
            // Disable the dropdown list of databases, to be enabled upon successful population
            CBDatabase.Enabled = false;

            // Disable the list of databases to generate code for
            CLBDatabases.Enabled = false;

            // If we have an open connection, close it and dispose of it
            if (db != null)
            {
                db.Close();
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
            //sqlBuilder.InitialCatalog = Settings.SQLDatabaseName;
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
 
        //private void SaveConnectionInfo()
        //{
        //    Settings.SQLServerNameOrIP = TSQLServerNameOrIP.Text;
        //    Settings.SQLUserName = TSQLUserName.Text;
        //    Settings.SQLPassword = TSQLPassword.Text;
        //    Settings.Save();
        //}

        public void PopulateDatabasesList()
        {
            CBDatabase.Items.Clear();
            CLBDatabases.Items.Clear();

            // Set up a command with the given query and associate
            // this with the current connection.
            using (SqlCommand cmd = new SqlCommand("SELECT name from master.sys.databases ORDER BY name", db))
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

            CBDatabase.Enabled = true;
            CLBDatabases.Enabled = true;
        }

        public void DebugLog(string Message, bool UpdateStatusBar = false)
        {
            RTDebug.AppendText(Message);
            if (UpdateStatusBar)
            {
                SBLabel.Text = Message;
            }
        }

        private void CBDatabase_SelectedValueChanged(object sender, EventArgs e)
        {
            SQLChangeDatabase();
        }

        public void SQLChangeDatabase()
        {
            // Clear out tables and fields
            CBTable.Enabled = false;
            CLBTables.Enabled = false;
            CBTable.Items.Clear();
            CLBTables.Items.Clear();

            int selectedIndex = CBDatabase.SelectedIndex;
            Object selectedItem = CBDatabase.SelectedItem;
            string DatabaseName;

            if (selectedIndex != -1)
            {
                DatabaseName = selectedItem.ToString();
            }
            else
            {
                DatabaseName = "master";
            }

            try
            {
                db.ChangeDatabase(DatabaseName);
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
            for (int i = 0; i < CLBDatabases.Items.Count; i++)
            {
                CLBDatabases.SetItemChecked(i, true);
            }
        }

        private void BtnDBSelectNone_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < CLBDatabases.Items.Count; i++)
            {
                CLBDatabases.SetItemChecked(i, false);
            }
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
            for (int i = 0; i < CLBTables.Items.Count; i++)
            {
                CLBTables.SetItemChecked(i, true);
            }
        }

        private void BtnTableSelectNone_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < CLBTables.Items.Count; i++)
            {
                CLBTables.SetItemChecked(i, false);
            }
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
            using (SqlCommand cmd = new SqlCommand("SELECT s.name FROM sys.tables t INNER JOIN sys.schemas s ON s.schema_id = t.schema_id WHERE t.name = '" + CurrentTable + "'", db))
            {
                using (IDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        CurrentTableSchema = dr[0].ToString();
                    }
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
            using (SqlCommand cmd = new SqlCommand("SELECT name, TYPE_NAME(system_type_id) AS type, max_length, precision, scale, is_nullable, is_identity, OBJECT_DEFINITION(default_object_id) AS DefaultValue from sys.columns WHERE object_id = OBJECT_ID('" + CurrentTableSchema + "." + CurrentTable + "') ORDER BY column_id", db))
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
                        if (Settings.ColumnAutoSelectAll)
                        {
                            item.Checked = true;
                        }
                        item.Text = dr[0].ToString();
                        FieldsList.Add(dr[0].ToString());
                        item.SubItems.Add(dr[1].ToString());
                        FieldTypes.Add(dr[1].ToString());
                        item.SubItems.Add(dr[2].ToString());
                        FieldSizes.Add(Convert.ToInt32(dr[2]));
                        item.SubItems.Add(dr[3].ToString());
                        FieldPrecisions.Add(Convert.ToInt32(dr[3]));
                        item.SubItems.Add(dr[4].ToString());
                        FieldScales.Add(Convert.ToInt32(dr[4]));
                        item.SubItems.Add(dr[5].ToString());
                        item.SubItems.Add(dr[6].ToString());
                        if (dr[7] == null)
                        {
                            item.SubItems.Add("(NULL)");
                        }
                        else
                        {
                            item.SubItems.Add(dr[7].ToString());
                        }
                        LVColumns.Items.Add(item);
                    }
                }
            }

            ResizeListViewColumns(LVColumns);
            LVColumns.Enabled = true;
        }

        private void ResizeListViewColumns(ListView lv)
        {
            foreach (ColumnHeader column in lv.Columns)
            {
                column.Width = -2;
            }
        }

        private void BtnColumnSelectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < LVColumns.Items.Count; i++)
            {
                LVColumns.Items[i].Checked = true;
            }
        }

        private void BtnColumnSelectNone_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < LVColumns.Items.Count; i++)
            {
                LVColumns.Items[i].Checked = false;
            }
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
                var confirmResult = MessageBox.Show("Are you sure to overwrite this code template?",
                                                     "Confirm Overwrite",
                                                     MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {
                    File.WriteAllText(TemplateFileName, RTEditTemplate.Text);
                    TemplateLoaded = true; // We now have a valid template and we want to track changes, even if no template was loaded until now.
                }
            }
            else
            {
                File.WriteAllText(TemplateFileName, RTEditTemplate.Text);
                TemplateLoaded = true; // We now have a valid template and we want to track changes, even if no template was loaded until now.
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
                        var confirmResult = MessageBox.Show("You have made changes to the code template. Are you sure you want to discard them and load another template?",
                            "Confirm Load Without Saving",
                            MessageBoxButtons.YesNo);
                        if (confirmResult != DialogResult.Yes)
                        {
                            TemplateChangeReversed = true;
                            if (PreviousCodeTemplateIndex != null)
                            {
                                CBCodeTemplate.SelectedIndex = PreviousCodeTemplateIndex.GetValueOrDefault();
                            }
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

            if (string.IsNullOrEmpty(TemplateName))
            {
                return;
            }

            TemplateFileName = TemplateName + ".txt";
            TemplateFileName = Path.Combine(TemplatesFolder, TemplateFileName);

            if (File.Exists(TemplateFileName))
            {
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

            if (string.IsNullOrEmpty(DBConnectionName))
            {
                return;
            }

            DBConnectionFileName = DBConnectionName + ".ini";
            DBConnectionFileName = Path.Combine(DBConnectionsFolder, DBConnectionFileName);

            if (File.Exists(DBConnectionFileName))
            {
                IniFile ini = new IniFile();
                ini.Load(DBConnectionFileName);
                TSQLServerNameOrIP.Text = ini.GetKeyValue("DBConnectionSettings", "SQLServerNameOrIP");
                TSQLUserName.Text = StringCipher.Decrypt(ini.GetKeyValue("DBConnectionSettings", "SQLUserName"), Key);
                TSQLPassword.Text = StringCipher.Decrypt(ini.GetKeyValue("DBConnectionSettings", "SQLPassword"), Key);
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
                var confirmResult = MessageBox.Show("Are you sure to overwrite this database connection?",
                    "Confirm Overwrite",
                    MessageBoxButtons.YesNo);
                if (confirmResult != DialogResult.Yes)
                {
                    return;
                }
            }

            IniFile ini = new IniFile();
            ini.AddSection("DBConnectionSettings").AddKey("SQLServerNameOrIP").Value = TSQLServerNameOrIP.Text;
            ini.AddSection("DBConnectionSettings").AddKey("SQLUserName").Value = StringCipher.Encrypt(TSQLUserName.Text, Key);
            ini.AddSection("DBConnectionSettings").AddKey("SQLPassword").Value = StringCipher.Encrypt(TSQLPassword.Text, Key);
            ini.Save(DBConnectionFileName);
            PopulateDatabaseConnections();
        }

        private void RTEditTemplate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (TemplateLoaded)
            {
                ChangesMadeSinceLastSave = true;
            }
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
                var confirmResult = MessageBox.Show("Are you sure to delete this code template?",
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

        private void GenerateCode()
        {
            string GeneratedCode = "";
            int SpacesToIndent = 0;
            List<string> FieldsList = new List<string>();

            int iGCCurrentLine = 0;
            int iGCNumLines = 1;  // Number of repeating lines
            bool InRepeatingSection = false;
            bool FirstLineIsDifferent = false;
            bool OnDifferentFirstLine = false;
            bool FirstLineGenerated = false;

            // Loop through lines
            while (iGCCurrentLine < RTEditTemplate.Lines.Count())
            {
                iGCNumLines = 1;
                string SGCCurrentLine = RTEditTemplate.Lines[iGCCurrentLine];

                // Process non-empty lines
                if (SGCCurrentLine.Length != 0)
                {
                    // Check for repeating lines
                    if ((SGCCurrentLine.Substring(0, 1) == ".") || (SGCCurrentLine.Substring(0, 1) == "!"))  //# If this line is a repeating line
                    {
                        InRepeatingSection = true;
                        if (SGCCurrentLine.Substring(0, 1) == "!")
                        {
                            FirstLineIsDifferent = true;
                            OnDifferentFirstLine = true;
                        }
                        iGCNumLines = GetNumberOfLinesThatRepeat(iGCCurrentLine);

                        bool FirstSelectedField = true;

                        //# Iterate through fields
                        for (int iGC = 0; iGC < LVColumns.Items.Count; iGC++)
                        {
                            // Generate if the field is checked
                            if (LVColumns.Items[iGC].Checked)
                            {
                                int iCurLine = 1;
                                //# Iterate through the repeating lines in the template
                                for (int jGC = iGCCurrentLine; jGC < (iGCCurrentLine + iGCNumLines); jGC++)
                                {
                                    if ((!FirstLineIsDifferent) 
                                        || (FirstLineIsDifferent && OnDifferentFirstLine && FirstSelectedField)
                                        || (FirstLineIsDifferent && !OnDifferentFirstLine && !FirstSelectedField && iCurLine > 1))
                                    {
                                        SGCCurrentLine = RTEditTemplate.Lines[jGC];
                                        if (ShouldGenerateLine(ref SGCCurrentLine, iGC))
                                        {
                                            GeneratedCode = GeneratedCode + ParseThisLine(SGCCurrentLine, iGC, SpacesToIndent) + "\r\n";
                                        }
                                    }
                                    iCurLine++;
                                    OnDifferentFirstLine = false;
                                }

                                FirstSelectedField = false;
                            }

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

            string SReturn = "";  //# Macro which is first in the string gets stuck in here
            int iFFMPos1 = 0;
            int iFFMPos2 = 0;

            iFFMPos1 = SFFMTemplate.IndexOf(CodeMacros[0]);
            if (iFFMPos1 != -1) SReturn = CodeMacros[0];

            for (int iFFM = 0; iFFM < CodeMacros.Count; iFFM++)
            {
                iFFMPos2 = SFFMTemplate.IndexOf(CodeMacros[iFFM]);
                if (iFFMPos1 != -1)
                {
                    if ((iFFMPos2 != -1) && (iFFMPos2 < iFFMPos1))   //# if macro was found and it was closer to start
                    {
                        iFFMPos1 = iFFMPos2;  //# set new first macro position
                        SReturn = CodeMacros[iFFM];
                    }

                }
                else
                    if (iFFMPos2 != -1)
                    {
                        iFFMPos1 = iFFMPos2;
                        SReturn = CodeMacros[iFFM];
                    }

            }

            return SReturn;
        }

        //########  FUNCTION TO REPLACE THE CODE MACROS WITH APPROPRIATE VALUES  #########
        private string ParseThisLine(string STemplate, int iPTLCurrentField, int iIndentSpaces)
        {
            string SParsedString = "";
            string SMacro = "";
            int iPTLCurrentChar = 0;  //# The current character
            int iPTLNumChars = 1;     //# The number of characters to copy
            int iPTLPos = 0;          //# Position of macro string

            SMacro = FindFirstMacro(STemplate, CodeMacros);

            while ((iPTLCurrentChar < STemplate.Length) && (SMacro != ""))
            {

                //# Copy all characters up till macro
                if (SMacro != "")
                {
                    iPTLPos = STemplate.IndexOf(SMacro);
                    iPTLNumChars = iPTLPos - iPTLCurrentChar;  //# Get from current to just before macro
                    SParsedString = SParsedString + STemplate.Substring(iPTLCurrentChar, iPTLNumChars);
                    //# Replace the macro string
                    if (SMacro == MacroTableName) SParsedString = SParsedString + CBTable.Text;
                    if (FieldsList.Count > 0)
                    {
                        if (SMacro == MacroColMaxLength) SParsedString = SParsedString + FieldSizes[iPTLCurrentField];
                        if (SMacro == MacroColumnName) SParsedString = SParsedString + FieldsList[iPTLCurrentField];
                        if (SMacro == MacroFieldList) SParsedString = SParsedString + CommaDelimitedFieldNames;
                        if (SMacro == MacroDatabaseName) SParsedString = SParsedString + CBDatabase.Text;
                        if (SMacro == MacroColumnType)
                        {
                            SParsedString = SParsedString + FieldTypes[iPTLCurrentField];
                            string sColumnSize = "";
                            if (FieldSizes[iPTLCurrentField] == -1)
                            {
                                sColumnSize = "max";
                            }
                            else
                            {
                                sColumnSize = FieldSizes[iPTLCurrentField].ToString();
                            }
                            switch (FieldTypes[iPTLCurrentField])
                            {
                                case "varchar":
                                    SParsedString = SParsedString + "(" + sColumnSize + ")";
                                    break;
                                case "nvarchar":
                                    SParsedString = SParsedString + "(" + sColumnSize + ")";
                                    break;
                                case "char":
                                    SParsedString = SParsedString + "(" + sColumnSize + ")";
                                    break;
                                case "nchar":
                                    SParsedString = SParsedString + "(" + sColumnSize + ")";
                                    break;
                                case "binary":
                                    SParsedString = SParsedString + "(" + sColumnSize + ")";
                                    break;
                                case "varbinary":
                                    SParsedString = SParsedString + "(" + sColumnSize + ")";
                                    break;
                                case "numeric":
                                    SParsedString = SParsedString + "(" + FieldPrecisions[iPTLCurrentField] + "," + FieldScales[iPTLCurrentField] + ")";
                                    break;
                                case "decimal":
                                    SParsedString = SParsedString + "(" + FieldPrecisions[iPTLCurrentField] + "," + FieldScales[iPTLCurrentField] + ")";
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    //# Trim the copied characters from the template string
                    iPTLPos = iPTLPos + SMacro.Length;
                    iPTLNumChars = STemplate.Length - iPTLPos;
                    if (iPTLPos < STemplate.Length)
                    {
                        if ((iPTLPos + iPTLNumChars) >= STemplate.Length)
                        {
                            iPTLNumChars = STemplate.Length - iPTLPos;
                        }
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
            if (STemplate.Length != 0) SParsedString = SParsedString + STemplate;

            //# Chop leading "." or "!" if present
            if (SParsedString != "")
            {
                if ((SParsedString.Substring(0, 1) == ".") || (SParsedString.Substring(0, 1) == "!"))
                    SParsedString = SParsedString.Substring(1);
            }

            //# Add spaces at beginning of line to indent
            if (iIndentSpaces > 0)
            {
                for (int i = 0; i < iIndentSpaces; i++)
                {
                    SParsedString = " " + SParsedString;
                }
            }

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
                
                if ((RTEditTemplate.Lines[iNLR].Substring(0, 1) == ".") || (RTEditTemplate.Lines[iNLR].Substring(0, 1) == "!"))
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
            List<string> AllowedFieldTypesList = new List<string>();

            int iPos1 = 0;
            int iPos2 = 0;
            string FieldType = FieldTypes[iCurrentField].ToUpper();
            string AllowedFieldTypesString;
            re = new Regex(sFindPattern);
            if (re.IsMatch(STemplate))
            {
                AllowedFieldTypesString = re.Match(STemplate).Groups[1].ToString();
                if (AllowedFieldTypesString.Length > 0)
                {
                    AllowedFieldTypesList = AllowedFieldTypesString.ToUpper().Split(',').ToList();
                    if (AllowedFieldTypesList.IndexOf(FieldType) != -1)
                    {
                        Result = true; // Found the current field type in the list of allowed types. Should generate line.
                    }
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

        private void BtnSaveConnectionDetails_Click(object sender, EventArgs e)
        {
            SaveDBConnection();
        }

        private void CBConnectionName_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDBConnection();
        }

        private void BtnRepeatingLine_Click(object sender, EventArgs e)
        {
            ToggleRepeatingLines();
        }

        /// <summary>
        /// Add or remove repeating indicator from all selected lines.
        /// </summary>
        /// <remarks>If some lines are repeating and some not, all will be made repeating.</remarks>
        private void ToggleRepeatingLines()
        {
            string SelectedText = RTEditTemplate.SelectedText;

            // Check whether some lines in the selected text are already repeating lines
            if (AllLinesRepeat(SelectedText))
            {
                SelectedText = RemoveRepeatingLines(SelectedText);
            }
            else
            {
                SelectedText = AddRepeatingLines(SelectedText);
            }

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
                if (curLine.Groups[1].Value != ".")
                {
                    if ((curLine.Value != "") && (i != (LinesMatched.Count - 1)))
                    {
                        Result = false;
                    }
                }
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
                if (!((curLine.Value == "") && (i == (LinesMatched.Count - 1))))
                {
                    Result += re2.Replace(curLine.Value, ReplaceWith);
                }
                else
                {
                    Result += curLine.Value;
                }
                if (i != (LinesMatched.Count - 1))
                {
                    Result += "\r\n";
                }
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
            if (!Initialising)
            {
                BuildSelectedColumnsList();
            }
        }

        private void BuildSelectedColumnsList()
        {
            List<string> SelectedColumns = new List<string>();
            foreach (ListViewItem item in LVColumns.CheckedItems)
            {
                SelectedColumns.Add(item.Text);
            }
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
                SQLServer_Connect();

                if ((CBDatabase.Items.Count > 0) && !string.IsNullOrEmpty(Settings.LastUsedDatabase) && CBDatabase.Items.Contains(Settings.LastUsedDatabase))
                {
                    // Select the last used database in the dropdown list
                    CBDatabase.SelectedIndex = CBDatabase.Items.IndexOf(Settings.LastUsedDatabase);
                }

                if ((CBTable.Items.Count > 0) && !string.IsNullOrEmpty(Settings.LastUsedTable) && CBTable.Items.Contains(Settings.LastUsedTable))
                {
                    // Select the last used table in the dropdown list
                    CBTable.SelectedIndex = CBTable.Items.IndexOf(Settings.LastUsedTable);
                }

                if ((LVColumns.Items.Count > 0) && (!string.IsNullOrEmpty(Settings.LastSelectedColumns)))
                {
                    // Split up the list of selected columns into an array, and use that to select the columns in the listview
                    List<string> ListSelectedColumns = new List<string>();
                    ListSelectedColumns.AddRange(Settings.LastSelectedColumns.Split(',').ToList());
                    foreach (string selectedColumn in ListSelectedColumns)
                    {
                        ListViewItem item1 = LVColumns.FindItemWithText(selectedColumn);
                        if (item1 != null)
                        {
                            item1.Checked = true;
                        }
                    }
                }

                if ((CBCodeTemplate.Items.Count > 0) && (!string.IsNullOrEmpty(Settings.LastUsedTemplate)) && (CBCodeTemplate.Items.Contains(Settings.LastUsedTemplate)))
                {
                    // Select the last used code template in the dropdown list, and generate code from the template if columns selected
                    CBCodeTemplate.SelectedIndex = CBCodeTemplate.Items.IndexOf(Settings.LastUsedTemplate);
                    if (LVColumns.CheckedItems.Count > 0) // TODO: Change this condition when other sources can be used for code generation
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
                        if (Settings.ColumnAutoSelectAll)
                        {
                            item.Checked = true;
                        }
                        item.Text = dr.GetName(col).ToString();         // Gets the column name
                        FieldsList.Add(dr.GetName(col).ToString());
                        LVData.Columns.Add(col.ToString(), dr.GetName(col).ToString());
                        item.SubItems.Add(dr.GetDataTypeName(col).ToString());
                        FieldTypes.Add(dr.GetDataTypeName(col).ToString());
                        FieldSizes.Add(0);
                        FieldPrecisions.Add(0);
                        FieldScales.Add(0);
                        // Console.Write(dr.GetFieldType(col).ToString());    // Gets the column type
                        // Console.Write(dr.GetDataTypeName(col).ToString()); // Gets the column database type
                        LVColumns.Items.Add(item);
                    }

                    int iRow = 1;


                }
            }
        }
    }
}
