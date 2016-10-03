using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using KBCsv;

namespace SnapshotCompare
{
    public partial class Form1 : Form
    {
        public string OutputFolder;
        public List<string> SnapshotDescriptionList;
        
        public Form1()
        {
            InitializeComponent();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            OutputFolder = "C:\\DataPersonal\\Projects\\DougsUtilsSuite\\zzSnapshotDB\\bin\\Debug\\Output";

            SnapshotDescriptionList = new List<string>();

            // Recurse into snapshot folders, get each snapshot's info.
            string SnapshotInfoFileName = "SnapshotInfo.csv";
            string CSVSnapshotInfoFilePath;
            string[] SnapshotDirs;
            SnapshotDirs = Directory.GetDirectories(OutputFolder);
            foreach (string CurrentSnapshotDirectory in SnapshotDirs)
            {
                CSVSnapshotInfoFilePath = Path.Combine(CurrentSnapshotDirectory, SnapshotInfoFileName);

                if (File.Exists(CSVSnapshotInfoFilePath))
                {
                    using (var streamReader = new StreamReader(CSVSnapshotInfoFilePath))
                    using (var reader = new CsvReader(streamReader))
                    {
                        // the CSV file has a header record, so we read that first
                        reader.ReadHeaderRecord();

                        while (reader.HasMoreRecords)
                        {
                            var dataRecord = reader.ReadDataRecord();

                            SnapshotDescriptionList.Add(dataRecord["SnapshotDescription"]);
                        }
                    }            
                }
            }


            // Initialise the Snapshot dropdowns.
            CBSnapshot1.Items.AddRange(SnapshotDescriptionList.ToArray());
            CBSnapshot2.Items.AddRange(SnapshotDescriptionList.ToArray());
        }

        public void CompareSnapshots()
        {
            // Validate selected snapshots
            if ((CBSnapshot1.SelectedIndex != -1) && (CBSnapshot2.SelectedIndex != -1))
            {
                if (CBSnapshot1.SelectedIndex == CBSnapshot2.SelectedIndex)
                {
                    MessageBox.Show("Cannot compare a snapshot to itself. Please select 2 different snapshots to compare.");
                    return;
                }
            }
            else
            {
                return;
            }

            StatusText.Text = "Compare Beginning";

            string Snapshot1Path, Snapshot2Path, SPList1, SPList2;
            Snapshot1Path = CBSnapshot1.SelectedItem.ToString();
            Snapshot1Path = Path.Combine(OutputFolder, Snapshot1Path);
            Snapshot2Path = CBSnapshot2.SelectedItem.ToString();
            Snapshot2Path = Path.Combine(OutputFolder, Snapshot2Path);
            SPList1 = Path.Combine(Snapshot1Path, "StoredProcList.csv");
            SPList2 = Path.Combine(Snapshot2Path, "StoredProcList.csv");

            ListViewItem item;

            LVStoredProcs1.Items.Clear();
            LVStoredProcs2.Items.Clear();

            // Populate List Views from CSV Stored Proc Lists
            using (var streamReader = new StreamReader(SPList1))
            using (var reader = new CsvReader(streamReader))
            {
                // the CSV file has a header record, so we read that first
                reader.ReadHeaderRecord();

                while (reader.HasMoreRecords)
                {
                    var dataRecord = reader.ReadDataRecord();

                    item = new ListViewItem();
                    //item.UseItemStyleForSubItems = false;
                    item.Text = dataRecord["Schema"];
                    item.SubItems.Add(dataRecord["ProcName"]);
                    item.SubItems.Add(dataRecord["CreateDate"]);
                    item.SubItems.Add(dataRecord["ModifyDate"]);
                    LVStoredProcs1.Items.Add(item);
                }
            }

            foreach (ColumnHeader column in LVStoredProcs1.Columns)
            {
                column.Width = -2;
            }

            using (var streamReader = new StreamReader(SPList2))
            using (var reader = new CsvReader(streamReader))
            {
                // the CSV file has a header record, so we read that first
                reader.ReadHeaderRecord();

                while (reader.HasMoreRecords)
                {
                    var dataRecord = reader.ReadDataRecord();

                    item = new ListViewItem();
                    //item.UseItemStyleForSubItems = false;
                    item.Text = dataRecord["Schema"];
                    item.SubItems.Add(dataRecord["ProcName"]);
                    item.SubItems.Add(dataRecord["CreateDate"]);
                    item.SubItems.Add(dataRecord["ModifyDate"]);
                    LVStoredProcs2.Items.Add(item);
                }
            }

            foreach (ColumnHeader column in LVStoredProcs2.Columns)
            {
                column.Width = -2;
            }

            // Highlight modified dates of more recent procs
            foreach (ListViewItem itemLeft in LVStoredProcs1.Items)
            {
                DateTime LeftLastModified, RightLastModified;
                string LeftSchema, RightSchema;
                string LeftProcName, RightProcName;
                
                // Get left side info
                LeftSchema = itemLeft.Text;
                LeftProcName = itemLeft.SubItems[1].Text;
                LeftLastModified = Convert.ToDateTime(itemLeft.SubItems[3].Text);

                foreach (ListViewItem itemRight in LVStoredProcs2.Items)
                {
                    // Get Right side info
                    RightSchema = itemRight.Text;
                    RightProcName = itemRight.SubItems[1].Text;
                    RightLastModified = Convert.ToDateTime(itemRight.SubItems[3].Text);

                    if ((LeftSchema.ToLower() == RightSchema.ToLower()) && (LeftProcName.ToLower() == RightProcName.ToLower()))
                    {
                        if (LeftLastModified > RightLastModified)
                        {
                            itemLeft.ForeColor = Color.Green;
                        }
                        if (LeftLastModified < RightLastModified)
                        {
                            itemRight.ForeColor = Color.Green;
                        }
                    }
                }
            }
        }

        private void CBSnapshot1_SelectedIndexChanged(object sender, EventArgs e)
        {
            CompareSnapshots();
        }

        private void CBSnapshot2_SelectedIndexChanged(object sender, EventArgs e)
        {
            CompareSnapshots();
        }

        private void OnSelectionChanged(ListView lv1, ListView lv2)
        {
            if (lv1.SelectedItems.Count > 0)
            {
                ListViewItem item = lv1.SelectedItems[0];
            }
        }

        private void LVStoredProcs1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectCorrespondingItem(LVStoredProcs1, LVStoredProcs2);
        }
 
        // Find name of currently selected item in ListView, select corresponding item in 2nd listview.
        private void SelectCorrespondingItem(ListView currListView, ListView destListView)
        {
            if (currListView.SelectedItems.Count > 0)
            {
                ListViewItem SelectedItem = currListView.SelectedItems[0];
                string ItemName = SelectedItem.SubItems[1].Text;
                foreach (ListViewItem item in destListView.Items)
                {
                    string DestItemName = item.SubItems[1].Text;
                    if (DestItemName.ToLowerInvariant() == ItemName.ToLowerInvariant())
                    {
                        // Found the item in the dest list view, unselect prev and select this item.
                        foreach (ListViewItem selectedItem in destListView.SelectedItems)
                        {
                            selectedItem.Selected = false;
                        }
                        item.Selected = true;
                        item.EnsureVisible();
                    }
                }
            }

            
        }

        private void BtnMergeProcs_Click(object sender, EventArgs e)
        {
            string WinMergePath = "C:\\Program Files (x86)\\WinMerge\\WinMergeU.exe";
            string LeftCompareFilePath = "\"C:\\DataPersonal\\Projects\\DougsUtilsSuite\\zzSnapshotDB\\bin\\Debug\\Output\\(local) Forecasting_Dev 2016-04-08 09_08_55\\SP_DataTransfer_Channel_Agg_Insert.sql\"";
            string RightCompareFilePath = "\"C:\\DataPersonal\\Projects\\DougsUtilsSuite\\zzSnapshotDB\\bin\\Debug\\Output\\192.168.0.11 Forecasting_Dev 2016-04-08 09_10_24\\SP_DataTransfer_Channel_Agg_Insert.sql\"";
            string CmdLineArguments = LeftCompareFilePath + " " + RightCompareFilePath;
            Process.Start(WinMergePath, CmdLineArguments);
        }

        private void LVStoredProcs2_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectCorrespondingItem(LVStoredProcs2, LVStoredProcs1);
        }
    }
}
