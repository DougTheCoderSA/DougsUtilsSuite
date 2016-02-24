using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilityClasses;

// This console app will take a snapshot of a live SQL database and store it in a form
// that can later be used to compare and generate synchronisation scripts.
namespace zzSnapshotDB
{
    class Program
    {
        public static AppSettings Settings;
        public static LogFile log;
        public static SqlConnection db; // Database connection to use when retrieving schema
        public static bool ContinueProcessing; // If this is set to false at any point, abort the process.

        static void Main(string[] args)
        {
            Settings = AppSettings.Load();
            log = new LogFile(Settings.LogFilePath);
            log.MaxFileSizeInKb = 1024;
            DebugMessage(log, "Starting schema dump");

            OpenDBConnection();

            ListSchemas();

            Console.ReadKey();
        }

        public static void OpenDBConnection()
        {
            if (db == null)
            {
                try
                {
                    // Specify the provider name, server and database.
                    string providerName = "System.Data.SqlClient";

                    // Initialize the connection string builder for the
                    // underlying provider.
                    SqlConnectionStringBuilder sqlBuilder =
                    new SqlConnectionStringBuilder();

                    // Set the properties for the data source.
                    sqlBuilder.DataSource = Settings.SQLServerNameOrIP;
                    sqlBuilder.InitialCatalog = Settings.SQLDatabaseName;
                    sqlBuilder.IntegratedSecurity = false;
                    sqlBuilder.UserID = Settings.SQLUserName;
                    sqlBuilder.Password = Settings.SQLPassword;

                    // Build the SqlConnection connection string.
                    string providerString = sqlBuilder.ToString();

                    db = new SqlConnection(sqlBuilder.ToString());

                    db.Open();
                }
                catch (Exception ex)
                {
                    ContinueProcessing = false;
                    string Message = "Error while trying to open a connection to the database.";
                    DebugMessage(log, Message, "Database", ex, true);
                }
            }
        }

        // Write debug message to console and log file
        public static void DebugMessage(LogFile log, string Message, string Category = "", Exception ex = null, bool WriteStackTrace = false)
        {
            if (!string.IsNullOrEmpty(Category))
            {
                Message = Category + ": " + Message;
            }

            if (ex != null)
            {
                Message = Message + "\r\nEXCEPTION: " + ex.Message;

                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;

                    Message = Message + "\r\nINNER EXCEPTION: " + ex.Message;
                }

                if (WriteStackTrace)
                {
                    Message = Message + "\r\nSTACK TRACE: " + ex.StackTrace;
                }
            }

            Console.WriteLine(Message);
            log.Write(Message);
        }

        public static void ListSchemas()
        {
            DataTable SchemaList;

            SqlCommand command = db.CreateCommand();
            command.CommandText = "SELECT * FROM sys.schemas ORDER BY schema_id";
            command.CommandTimeout = 30;
            command.CommandType = CommandType.Text;

            SqlDataReader reader = command.ExecuteReader();

            // Display the data within the reader.
            while (reader.Read())
            {
                // Display all the columns. 
                for (int i = 0; i < reader.FieldCount; i++)
                    Console.Write("{0} ", reader.GetValue(i));
                Console.WriteLine();
            }
        }
    }
}
