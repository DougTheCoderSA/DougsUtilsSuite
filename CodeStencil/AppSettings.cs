using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UtilityClasses;

namespace CodeStencil
{
    public enum SQLAuthenticationTypeEnum
    {
        WindowsAuthentication, SQLServerAuthentication
    }

    public class AppSettings
    {
        IniFile iniFile;

        // Database connection settings
        public string SQLServerNameOrIP { get; set; }
        public string SQLUserName { get; set; }
        public string SQLPassword { get; set; }
        public string SQLDatabaseName { get; set; }

        // Folder to store the INI file - folder the executable is running in.
        private static string SettingsFolder
        {
            get
            {
                string folder = Assembly.GetExecutingAssembly().GetName().CodeBase;
                folder = folder.Substring(8).Replace("/", "\\");
                folder = Path.GetDirectoryName(folder);

                return folder;
            }
        }

        // Full path to the INI file
        private static string SettingsFile
        {
            get
            {
                return Path.Combine(SettingsFolder, "Settings.ini");
            }
        }

        private static AppSettings DefaultSettings
        {
            get
            {
                return new AppSettings
                {
                    SQLServerNameOrIP = "192.168.0.11",
                    SQLUserName = "sa",
                    SQLPassword = "BMAsql2",
                    SQLDatabaseName = "Forecasting_Dev",
                };
            }
        }
        public void Save()
        {
            iniFile = new IniFile();

            iniFile.AddSection("Database").AddKey("SQLServerNameOrIP").Value = SQLServerNameOrIP;
            iniFile.AddSection("Database").AddKey("SQLUserName").Value = SQLUserName;
            iniFile.AddSection("Database").AddKey("SQLPassword").Value = SQLPassword;
            iniFile.AddSection("Database").AddKey("SQLDatabaseName").Value = SQLDatabaseName;

            iniFile.Save(SettingsFile);
        }

        public static AppSettings Load()
        {
            IniFile ini;
            ini = new IniFile();

            if (!File.Exists(SettingsFile))
            {
                AppSettings DefaultAppSettings = DefaultSettings;

                ini.AddSection("Database").AddKey("SQLServerNameOrIP").Value = DefaultAppSettings.SQLServerNameOrIP;
                ini.AddSection("Database").AddKey("SQLUserName").Value = DefaultAppSettings.SQLUserName;
                ini.AddSection("Database").AddKey("SQLPassword").Value = DefaultAppSettings.SQLPassword;
                ini.AddSection("Database").AddKey("SQLDatabaseName").Value = DefaultAppSettings.SQLDatabaseName;

                ini.Save(SettingsFile);

                return DefaultAppSettings;
            }
            else
            {
                bool IniFileValueAdded = false; // Keep track of whether we had to add a missing setting and need to save the ini file
                AppSettings DefaultAppSettings = DefaultSettings;
                AppSettings LoadedSettings = new AppSettings();

                ini.Load(SettingsFile);

                LoadedSettings.SQLServerNameOrIP = ini.GetKeyValueOrDefault("Database", "SQLServerNameOrIP", DefaultAppSettings.SQLServerNameOrIP, ref IniFileValueAdded);
                LoadedSettings.SQLUserName = ini.GetKeyValueOrDefault("Database", "SQLUserName", DefaultAppSettings.SQLUserName, ref IniFileValueAdded);
                LoadedSettings.SQLPassword = ini.GetKeyValueOrDefault("Database", "SQLPassword", DefaultAppSettings.SQLPassword, ref IniFileValueAdded);
                LoadedSettings.SQLDatabaseName = ini.GetKeyValueOrDefault("Database", "SQLDatabaseName", DefaultAppSettings.SQLDatabaseName, ref IniFileValueAdded);

                if (IniFileValueAdded) ini.Save(SettingsFile); // Save the file if we had to add a missing setting

                return LoadedSettings;
            }

        }

    }
}
