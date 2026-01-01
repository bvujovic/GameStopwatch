namespace GameStopwatch
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            try
            {
                Application.Run(new FrmMain());
            }
            catch (Exception ex)
            {
                Classes.Utils.AddToLogFile(ex.Message, ex.StackTrace);
            }
        }
    }
}