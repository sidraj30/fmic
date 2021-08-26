using System.Configuration;

namespace DatabaseBackup
{
    class ConfigData
    {
        public static string ConnectionString
        {
            get
            {
                object obj = ConfigurationManager.AppSettings["Connection.String"];
                return obj == null ? string.Empty : obj as string;
            }
        }

        public static string BackupFolderPath
        {
            get
            {
                object obj = ConfigurationManager.AppSettings["Backup.Folder.Full.Path"];
                return obj == null ? string.Empty : obj as string;
            }
        }
        public static string DatabaseName
        {
            get
            {
                object obj = ConfigurationManager.AppSettings["Database.Name.To.Backup"];
                return obj == null ? string.Empty : obj as string;
            }
        }
    }
}
