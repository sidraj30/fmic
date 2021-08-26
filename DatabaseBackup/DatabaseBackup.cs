using System;

namespace DatabaseBackup
{
    class DatabaseBackup
    {
        static void Main(string[] args)
        {
            string connectionString = ConfigData.ConnectionString;
            string folderPath = ConfigData.BackupFolderPath;
            string dbName = ConfigData.DatabaseName;

            Console.WriteLine(string.Format("\nBackup of {0} is in progress...", dbName));
            try
            {
                BackupService bs = new BackupService(connectionString, folderPath, dbName);
                Console.WriteLine(string.Format("\nBackup of {0} created successfully", dbName));
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("\nError occured while taking backup \n {0}", ex.Message));

            }

            Console.WriteLine("\n\nPress any key to continue...");
            Console.ReadKey();
        }
    }
}
