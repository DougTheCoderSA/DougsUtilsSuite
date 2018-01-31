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
    public class AppSettings
    {
        IniFile iniFile;

        // App State
        public string LastUsedDBConnection { get; set; }
        public bool IsServerConnected { get; set; }
        public string LastUsedDatabase { get; set; }
        public string LastUsedTable { get; set; }
        public string LastSelectedColumns { get; set; }
        public string LastUsedTemplate { get; set; }
        public bool ColumnAutoSelectAll { get; set; }

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
                    LastUsedDBConnection = ""
                    ,IsServerConnected = false
                    ,LastUsedDatabase = ""
                    ,LastUsedTable = ""
                    ,LastSelectedColumns = ""
                    ,LastUsedTemplate = ""
                    ,ColumnAutoSelectAll = true
                };
            }
        }
        public void Save()
        {
            iniFile = new IniFile();

            iniFile.AddSection("General").AddKey("LastUsedDBConnection").Value = LastUsedDBConnection;
            iniFile.AddSection("General").AddKey("IsServerConnected").Value = Convert.ToString(IsServerConnected);
            iniFile.AddSection("General").AddKey("LastUsedDatabase").Value = LastUsedDatabase;
            iniFile.AddSection("General").AddKey("LastUsedTable").Value = LastUsedTable;
            iniFile.AddSection("General").AddKey("LastSelectedColumns").Value = LastSelectedColumns;
            iniFile.AddSection("General").AddKey("LastUsedTemplate").Value = LastUsedTemplate;
            iniFile.AddSection("General").AddKey("ColumnAutoSelectAll").Value = Convert.ToString(ColumnAutoSelectAll);

            iniFile.Save(SettingsFile);
        }

        public static AppSettings Load()
        {
            IniFile ini;
            ini = new IniFile();

            if (!File.Exists(SettingsFile))
            {
                AppSettings DefaultAppSettings = DefaultSettings;

                ini.AddSection("General").AddKey("LastUsedDBConnection").Value = DefaultAppSettings.LastUsedDBConnection;
                ini.AddSection("General").AddKey("IsServerConnected").Value = Convert.ToString(DefaultAppSettings.IsServerConnected);
                ini.AddSection("General").AddKey("LastUsedDatabase").Value = DefaultAppSettings.LastUsedDatabase;
                ini.AddSection("General").AddKey("LastUsedTable").Value = DefaultAppSettings.LastUsedTable;
                ini.AddSection("General").AddKey("LastSelectedColumns").Value = DefaultAppSettings.LastSelectedColumns;
                ini.AddSection("General").AddKey("LastUsedTemplate").Value = DefaultAppSettings.LastUsedTemplate;
                ini.AddSection("General").AddKey("ColumnAutoSelectAll").Value = Convert.ToString(DefaultAppSettings.ColumnAutoSelectAll);

                ini.Save(SettingsFile);

                return DefaultAppSettings;
            }
            else
            {
                bool IniFileValueAdded = false; // Keep track of whether we had to add a missing setting and need to save the ini file
                AppSettings DefaultAppSettings = DefaultSettings;
                AppSettings LoadedSettings = new AppSettings();

                ini.Load(SettingsFile);

                LoadedSettings.LastUsedDBConnection = ini.GetKeyValueOrDefault("General", "LastUsedDBConnection", DefaultAppSettings.LastUsedDBConnection, ref IniFileValueAdded);
                LoadedSettings.IsServerConnected = Convert.ToBoolean(ini.GetKeyValueOrDefault("General", "IsServerConnected", Convert.ToString(DefaultAppSettings.IsServerConnected), ref IniFileValueAdded));
                LoadedSettings.LastUsedDatabase = ini.GetKeyValueOrDefault("General", "LastUsedDatabase", DefaultAppSettings.LastUsedDatabase, ref IniFileValueAdded);
                LoadedSettings.LastUsedTable = ini.GetKeyValueOrDefault("General", "LastUsedTable", DefaultAppSettings.LastUsedTable, ref IniFileValueAdded);
                LoadedSettings.LastSelectedColumns = ini.GetKeyValueOrDefault("General", "LastSelectedColumns", DefaultAppSettings.LastSelectedColumns, ref IniFileValueAdded);
                LoadedSettings.LastUsedTemplate = ini.GetKeyValueOrDefault("General", "LastUsedTemplate", DefaultAppSettings.LastUsedTemplate, ref IniFileValueAdded);
                LoadedSettings.ColumnAutoSelectAll = Convert.ToBoolean(ini.GetKeyValueOrDefault("General", "ColumnAutoSelectAll", Convert.ToString(DefaultAppSettings.ColumnAutoSelectAll), ref IniFileValueAdded));

                if (IniFileValueAdded) ini.Save(SettingsFile); // Save the file if we had to add a missing setting

                return LoadedSettings;
            }

        }

    }
}
