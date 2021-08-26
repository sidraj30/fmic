using System;
using System.Data.SqlClient;
using System.IO;
using System.Threading;

namespace DatabaseBackup
{
    public class BackupService
    {
        private readonly string _connectionString;
        private readonly string _backupFolderFullPath;
        public const string BackupQuery = "BACKUP DATABASE [{0}] TO DISK='{1}' WITH INIT,STATS=1,COMPRESSION";
        public const string BackupCreatedSuccessfully = "Backup file of '{0}' database created successfully";

        public BackupService(string connectionString, string backupFolderFullPath, string databaseName)
        {
            _connectionString = connectionString;
            _backupFolderFullPath = backupFolderFullPath;

            BackupDatabase(databaseName);
        }

        public void BackupDatabase(string databaseName)
        {
            string filePath = BuildBackupPathWithFilename(databaseName);
            double progressPercent = 0;
            string prevMessage = string.Empty;
            string currentMessage = string.Empty;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = String.Format(BackupQuery, databaseName, filePath);

                using (ProgressBar progress = new ProgressBar())
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        connection.FireInfoMessageEventOnUserErrors = true;

                        connection.InfoMessage += delegate (object sender, SqlInfoMessageEventArgs e)
                        {
                            prevMessage = currentMessage;
                            currentMessage = e.Message;
                            progress.Report(progressPercent / 100);
                            progressPercent++;
                        };

                        command.CommandTimeout = 0;
                        command.ExecuteNonQuery();
                    }

                }
                if (progressPercent < 100 && prevMessage.Length > 0)
                {
                    Console.WriteLine(prevMessage);
                }
                else
                {
                    Console.WriteLine(string.Format(BackupCreatedSuccessfully, databaseName));
                }
            }
        }

        private string BuildBackupPathWithFilename(string databaseName)
        {
            string filename = string.Format("{0}-{1}.bak", databaseName, DateTime.Now.ToString("yyyy-MM-dd"));

            return Path.Combine(_backupFolderFullPath, filename);
        }
    }
}
