using System;
using System.Data;
using System.IO;
using System.Linq;

namespace CodeStencil
{
    /// <summary>
    /// Class to keep specified number of previous versions of an overwritten file.
    /// <example>For example:
    /// <code>
    ///    FileHelper.MakeBackup("web.config", 5);
    /// </code>
    /// The first time you called it, you would get web.config.20140314_01. The next time would get web.config.20140314_02.
    /// When web.config.14031406 was created, web.config.14031401 would be deleted to keep the number of backups limited to 5.
    /// </example>
    /// </summary>
    public static class FileHelper
    {
        /// <summary>
        /// Make a numbered backup copy of the specified files.  Backup files have the name filename.exe.yymmdd##, where yymmdd is the date and ## is a zero justified sequence number starting at 1
        /// </summary>
        /// <param name="fileName">Name of the file to backup.</param>
        /// <param name="maxBackups">The maximum backups to keep.</param>
        public static void MakeBackup(string fileName, int maxBackups)
        {
            // Make sure that the file exists, you don't backup a new file
            if (File.Exists(fileName))
            {
                // First backup copy of the day starts at 1
                int newSequence = 1;

                // Get the list of previous backups of the file, skipping the current file
                var backupFiles = Directory.GetFiles(Path.GetDirectoryName(fileName), Path.GetFileName(fileName) + ".*")
                    .ToList()
                    .Where(d => !d.Equals(fileName))
                    .OrderBy(d => d);

                // Get the name of the last backup performed
                var lastBackupFilename = backupFiles.LastOrDefault();

                // If we have at least one previous backup copy
                if (lastBackupFilename != null)
                {
                    // Get the last sequence number back taking the last 2 characters and convert them to an int. And add 1 to that number
                    if (Int32.TryParse(Path.GetExtension(lastBackupFilename).GetLast(2), out newSequence))
                        newSequence++;

                    // If we have more backups than we need to keep
                    if (backupFiles.Count() >= maxBackups)
                    {
                        // Get a list of the oldest files to delele
                        var expiredFiles = backupFiles.Take(backupFiles.Count() - maxBackups + 1);

                        foreach (var expiredFile in expiredFiles)
                        {
                            File.Delete(expiredFile);
                        }
                    }
                }

                // Create the file name for the newest back up file.
                var latestBackup = String.Format("{0}.{1:yyyyMMdd}_{2:00}", fileName, DateTime.Now, newSequence);

                // Copy the current file to the new backup name and overwrite any existing copy
                File.Copy(fileName, latestBackup, true);
            }
        }
    }
}