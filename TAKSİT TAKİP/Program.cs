namespace TAKSİT_TAKİP
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
            Application.Run(new Form1()); 
            Server_Settings.serverayar("127.0.0.1", 5000);
            Client.Start();
        }
    }
}