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

namespace CodeStencil
{
    public partial class FormMain : Form
    {
        public SqlConnection db;
        public string CurrentDatabase;
        public string CurrentTable;
        public string CurrentTableSchema;
        public List<string> CodeMacros;
        public List<string> FieldsList;
        public List<string> FieldTypes;
        public List<int> FieldSizes;
        public const string LineRepeatIndicator = ".";
        public const string MacroDatabaseName = "@d";
        public const string MacroTableName = "@t";
        public const string MacroColumnName = "@f";
        public const string MacroColMaxLength = "@s";
        public const string MacroColumnType = "@y";
        public string TemplatesFolder;
        public string ApplicationFolder;
        public bool TemplateLoaded = false;
        public bool ChangesMadeSinceLastSave = false;

        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            // Initialise string list with all replaceable macros
            CodeMacros = new List<string>();
            CodeMacros.Add(MacroDatabaseName);
            CodeMacros.Add(MacroTableName);
            CodeMacros.Add(MacroColumnName);
            CodeMacros.Add(MacroColMaxLength);
            CodeMacros.Add(MacroColumnType);

            FieldsList = new List<string>();
            FieldSizes = new List<int>();
            FieldTypes = new List<string>();
            // Get templates folder - by default, folder called Templates inside the executable folder.
            ApplicationFolder = Assembly.GetExecutingAssembly().GetName().CodeBase;
            ApplicationFolder = ApplicationFolder.Substring(8).Replace("/", "\\");
            ApplicationFolder = Path.GetDirectoryName(ApplicationFolder);
            TemplatesFolder = Path.Combine(ApplicationFolder, "Templates");

            if (!Directory.Exists(TemplatesFolder))
            {
                Directory.CreateDirectory(TemplatesFolder);
            }

            PopulateCodeTemplates();
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
                PopulateDatabasesList();
            }
            catch (Exception ex)
            {
                PSQLServerConnStatus.BackColor = Color.Red;
                DebugLog("Error Connecting to Server: " + ex.Message, true);
            }

        }

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
            }

            PopulateColumns();
        }

        private void PopulateColumns()
        {
            LVColumns.Items.Clear();
            ListViewItem item;

            // Set up a command with the given query and associate
            // this with the current connection.
            using (SqlCommand cmd = new SqlCommand("SELECT name, TYPE_NAME(system_type_id) AS type, max_length, precision, scale, is_nullable, is_identity from sys.columns WHERE object_id = OBJECT_ID('" + CurrentTableSchema + "." + CurrentTable + "') ORDER BY column_id", db))
            {
                using (IDataReader dr = cmd.ExecuteReader())
                {
                    FieldsList.Clear();
                    FieldTypes.Clear();
                    FieldSizes.Clear();
                    while (dr.Read())
                    {
                        item = new ListViewItem();
                        item.Text = dr[0].ToString();
                        FieldsList.Add(dr[0].ToString());
                        item.SubItems.Add(dr[1].ToString());
                        FieldTypes.Add(dr[1].ToString());
                        item.SubItems.Add(dr[2].ToString());
                        FieldSizes.Add(Convert.ToInt32(dr[2]));
                        item.SubItems.Add(dr[3].ToString());
                        item.SubItems.Add(dr[4].ToString());
                        item.SubItems.Add(dr[5].ToString());
                        item.SubItems.Add(dr[6].ToString());
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
                }
            }
            else
            {
                File.WriteAllText(TemplateFileName, RTEditTemplate.Text);
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

        private void CBCodeTemplate_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadTemplate();
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
            }
            else
            {
                MessageBox.Show("The template you are trying to load no longer exists.");
                PopulateCodeTemplates();
            }
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

            while (iGCCurrentLine < RTEditTemplate.Lines.Count())
            {
                iGCNumLines = 1;
                string SGCCurrentLine = RTEditTemplate.Lines[iGCCurrentLine];

                if (SGCCurrentLine.Length != 0)
                {
                    if (SGCCurrentLine.Substring(0, 1) == ".")  //# If this line is a repeating line
                    {

                        iGCNumLines = GetNumberOfLinesThatRepeat(iGCCurrentLine);
                        //# Iterate through the selected field names
                        for (int iGC = 0; iGC < LVColumns.Items.Count; iGC++)
                        {
                            if (LVColumns.Items[iGC].Checked)
                            {

                                //# Iterate through the repeating lines in the template
                                for (int jGC = iGCCurrentLine; jGC < (iGCCurrentLine + iGCNumLines); jGC++)
                                {
                                    SGCCurrentLine = RTEditTemplate.Lines[jGC];
                                    if (ShouldGenerateLine(ref SGCCurrentLine, iGC))
                                    {
                                        GeneratedCode = GeneratedCode + ParseThisLine(SGCCurrentLine, iGC, SpacesToIndent) + "\r\n";
                                    }

                                }
                                
                            }

                        }
                    }
                    else
                    {
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
                    if (SMacro == "@t") SParsedString = SParsedString + CBTable.Text;
                    if (SMacro == "@s") SParsedString = SParsedString + FieldSizes[iPTLCurrentField];
                    if (SMacro == "@f") SParsedString = SParsedString + FieldsList[iPTLCurrentField];
                    if (SMacro == "@d") SParsedString = SParsedString + CBDatabase.Text;
                    if (SMacro == "@y") SParsedString = SParsedString + FieldTypes[iPTLCurrentField];
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

            //# Chop leading "." if present
            if (SParsedString != "")
            {
                if (SParsedString.Substring(0, 1) == ".")
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
                
                if (RTEditTemplate.Lines[iNLR].Substring(0, 1) == ".")
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
    }
}
