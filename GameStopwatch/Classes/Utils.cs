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

        public static string GetDataSetBackupFileName()
        {
            //if (idxFolder == -1)
            //    SetOneDriveAppFolder();
            //Ds.WriteXml($"{backupFolder}/{DateTime.Now:yyyy.MM.dd_HH.mm}.xml");
            return Path.Combine(folders[idxFolder], backupFolder, $"{DateTime.Now:yyyy.MM.dd_HH.mm}_ds.xml");
        }

        public static void AddToLogFile(string message, string? stackTrace)
        {
            File.AppendAllText(Path.Combine(folders[idxFolder], "messages.log")
                , $"{DateTime.Now}: {message}\n{stackTrace}\n\n");
        }
    }
}
