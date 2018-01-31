using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileMangler
{
    public partial class FormMain : Form
    {
        public List<string> MatchingFileList;

        public FormMain()
        {
            InitializeComponent();
        }

        private void BtnBrowseProjectFolder_Click(object sender, EventArgs e)
        {
            string _folderName = TProjectFolder.Text;
            _folderName = (System.IO.Directory.Exists(_folderName)) ? _folderName : "";

            FolderBrowser.RootFolder = Environment.SpecialFolder.MyComputer;
            FolderBrowser.SelectedPath = _folderName;

            if (FolderBrowser.ShowDialog() == DialogResult.OK)
            {
                _folderName = FolderBrowser.SelectedPath;
                TProjectFolder.Text = _folderName;

            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            MatchingFileList = new List<string>();
            Text = "File Mangler - version " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }

        private void BtnFindFiles_Click(object sender, EventArgs e)
        {
            // Get files matching extensions
            MatchingFileList.Clear();
            MatchingFileList = Directory.GetFileSystemEntries(TProjectFolder.Text, "*.asp", SearchOption.AllDirectories).ToList();
            //MatchingFileList.AddRange(Directory.GetFileSystemEntries(TProjectFolder.Text, "*.inc", SearchOption.AllDirectories).ToList());
            //MatchingFileList.AddRange(Directory.GetFileSystemEntries(TProjectFolder.Text, "*.htm", SearchOption.AllDirectories).ToList());
            //MatchingFileList.AddRange(Directory.GetFileSystemEntries(TProjectFolder.Text, "*.html", SearchOption.AllDirectories).ToList());
            //MatchingFileList.AddRange(Directory.GetFileSystemEntries(TProjectFolder.Text, "*.asa", SearchOption.AllDirectories).ToList());
            //MatchingFileList.AddRange(Directory.GetFileSystemEntries(TProjectFolder.Text, "*.js", SearchOption.AllDirectories).ToList());
            MatchingFileList.Sort();

            // Filter out files that don't contain the menu include
            for (int i = MatchingFileList.Count - 1; i >= 0; i--)
            {
                if (!FileContains(MatchingFileList[i], @"<!--.*#INCLUDE.*JSMenu\.asp.*-->"))
                {
                    TDebug.AppendText("Removing file " + MatchingFileList[i] + " - it doesn't include the menu.\r\n");
                    MatchingFileList.RemoveAt(i);
                }
            }

            // Filter out files that contain the validate include
            for (int i = MatchingFileList.Count - 1; i >= 0; i--)
            {
                if (FileContains(MatchingFileList[i], @"<!--.*#INCLUDE.*Validate\.asp.*-->"))
                {
                    TDebug.AppendText("Removing file " + MatchingFileList[i] + " - it contains the security validation.\r\n");
                    MatchingFileList.RemoveAt(i);
                }
            }

            // Add the matching files to the check list box
            CLBFilesThatMatch.Items.Clear();
            foreach (var item in MatchingFileList)
            {
                CLBFilesThatMatch.Items.Add(item.Replace(TProjectFolder.Text, "."));
            }
        }

        /// <summary>
        /// Returns true if a file is matched by the Regular Expression specified
        /// </summary>
        /// <param name="FilePath">Path for the file to search</param>
        /// <param name="RegExPattern">RegEx to search for</param>
        /// <returns></returns>
        private bool FileContains(string FilePath, string RegExPattern)
        {
            bool Result = false;
            string FileContents = System.IO.File.ReadAllText(FilePath);
            RegexOptions options = RegexOptions.IgnoreCase 
                | RegexOptions.ECMAScript 
                | RegexOptions.Multiline;
            Regex re = new Regex(RegExPattern, options);

            Result = re.IsMatch(FileContents);

            return Result;
        }

        private void BtnCopyMatchingFiles_Click(object sender, EventArgs e)
        {
            string TextToCopy = "";

            foreach (var thisFile in MatchingFileList)
            {
                if (TextToCopy != "")
                {
                    TextToCopy += "\r\n";
                }
                TextToCopy += thisFile;
            }

            Clipboard.SetText(TextToCopy);
        }
    }
}
