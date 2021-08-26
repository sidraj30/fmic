using System;

namespace DatabaseBackup
{
    class DatabaseBackup
    {
        public const string BackupInProgress = "\nBackup of '{0}' database is in progress...\n";
        public const string ErrorOccured = "\nError occured while taking backup: \n{0}";
        public const string PressAnyKeyToContinue = "\nPress any key to continue...";

        static void Main(string[] args)
        {
            string connectionString = ConfigData.ConnectionString;
            string folderPath = ConfigData.BackupFolderPath;
            string dbName = ConfigData.DatabaseName;

            Console.WriteLine(string.Format(BackupInProgress, dbName));
            try
            {
                BackupService bs = new BackupService(connectionString, folderPath, dbName);
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format(ErrorOccured, ex.Message));
            }

            Console.WriteLine(PressAnyKeyToContinue);
            Console.ReadKey();
        }
    }
}
