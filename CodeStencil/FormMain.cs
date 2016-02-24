using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CodeStencil
{
    public partial class FormMain : Form
    {
        public SqlConnection db;

        public FormMain()
        {
            InitializeComponent();
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
            // Empty the list of databases, to be refreshed upon successful connection
            CBDatabaseList.Enabled = false;
            CBDatabaseList.Items.Clear();

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
            CBDatabaseList.Items.Clear();

            // Set up a command with the given query and associate
            // this with the current connection.
            using (SqlCommand cmd = new SqlCommand("SELECT name from master.sys.databases ORDER BY name", db))
            {
                using (IDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        CBDatabaseList.Items.Add(dr[0].ToString());
                    }
                }
            }

            CBDatabaseList.Enabled = true;
        }

        public void DebugLog(string Message, bool UpdateStatusBar = false)
        {
            RTDebug.AppendText(Message);
            if (UpdateStatusBar)
            {
                SBLabel.Text = Message;
            }
        }

        private void CBDatabaseList_SelectedValueChanged(object sender, EventArgs e)
        {
            SQLChangeDatabase();
        }

        public void SQLChangeDatabase()
        {
            int selectedIndex = CBDatabaseList.SelectedIndex;
            Object selectedItem = CBDatabaseList.SelectedItem;
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
                DebugLog("Connected to database: " + DatabaseName, true);
            }
            catch (Exception ex)
            {
                DebugLog("Error trying to change database: " + ex.Message, true);
            }
        }
    }
}
