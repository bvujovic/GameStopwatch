namespace GameStopwatch.Classes
{
    public static class Utils
    {
        private static readonly string[] folders = [
            "c:\\Users\\bvnet\\OneDrive\\x\\AppData\\GameStopwatch\\",
            "c:\\Users\\sosos\\OneDrive\\x\\AppData\\GameStopwatch\\"
            ];

        private const string backupFolder = "backup";

        // Index of the found OneDrive/x folder in the folders array
        private static int idxFolder = -1;

        private static void SetOneDriveAppFolder()
        {
            for (int i = 0; i < folders.Length; i++)
                if (Directory.Exists(folders[i]))
                    idxFolder = i;
            if (idxFolder == -1)
                throw new Exception("GameStopwatch folder on OneDrive/x is not found.");
        }

        private const string dataSetFileName = "ds.xml";

        public static string GetDataSetFileName()
        {
            if (idxFolder == -1)
                SetOneDriveAppFolder();
            return Path.Combine(folders[idxFolder], dataSetFileName);
        }

        public static string GetDataSetBackupFolder()
        {
            return Path.Combine(folders[idxFolder], backupFolder);
        }

        public static string GetDataSetBackupFileName()
        {
            return Path.Combine(GetDataSetBackupFolder(), $"{DateTime.Now:yyyy.MM.dd_HH.mm}_ds.xml");
        }

        // Exctract last date from files in `backupFolder`
        public static DateTime? GetLastBackupDate()
        {
            string backupPath = GetDataSetBackupFolder();
            if (!Directory.Exists(backupPath))
                return null;
            var files = Directory.GetFiles(backupPath, "????.??.??_??.??_ds.xml");
            DateTime? lastDate = null;
            foreach (var file in files)
            {
                var fileName = Path.GetFileName(file);
                var datePart = fileName.Substring(0, "yyyy.MM.dd_HH.mm".Length);
                if (DateTime.TryParseExact(datePart, "yyyy.MM.dd_HH.mm", null
                    , System.Globalization.DateTimeStyles.None, out DateTime dt))
                {
                    if (lastDate == null || dt > lastDate)
                        lastDate = dt;
                }
            }
            return lastDate;
        }

        public static void AddToLogFile(string message, string? stackTrace)
        {
            SetOneDriveAppFolder();
            File.AppendAllText(Path.Combine(folders[idxFolder], "messages.log")
                , $"{DateTime.Now}: {message}\n{stackTrace}\n\n");
        }
    }
}
