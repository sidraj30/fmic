using System;
using System.Data.SqlClient;
using System.IO;

namespace DatabaseBackup
{
    public class BackupService
    {
        private readonly string _connectionString;
        private readonly string _backupFolderFullPath;

        public BackupService(string connectionString, string backupFolderFullPath, string databaseName)
        {
            _connectionString = connectionString;
            _backupFolderFullPath = backupFolderFullPath;

            BackupDatabase(databaseName);
        }

        public void BackupDatabase(string databaseName)
        {
            string filePath = BuildBackupPathWithFilename(databaseName);

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = String.Format("BACKUP DATABASE [{0}] TO DISK='{1}' WITH INIT,STATS=1,COMPRESSION", databaseName, filePath);

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    command.CommandTimeout = 0;
                    command.ExecuteNonQuery();
                }

            }
        }

        private void Command_StatementCompleted(object sender, System.Data.StatementCompletedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private string BuildBackupPathWithFilename(string databaseName)
        {
            string filename = string.Format("{0}-{1}.bak", databaseName, DateTime.Now.ToString("yyyy-MM-dd"));

            return Path.Combine(_backupFolderFullPath, filename);
        }
    }
}
