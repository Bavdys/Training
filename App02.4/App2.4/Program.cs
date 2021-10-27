using App2._4.MonitoringSetting;
using System;
using System.Threading;

namespace App2._4
{
    class Program
    {
        private static Mutex _mutex;
        private static readonly string PATH = @"Configuration/AuditsSettings.json";
        
        static void Main(string[] args)
        {
            if (!IsMainInstance())
            {
                return;
            }
            try
            {
                JsonRepository repository = new JsonRepository(PATH);
                FileSetting fileSetting = new FileSetting(repository);
                Monitoring monitoring = new Monitoring();

                monitoring.Initialization(fileSetting);

                monitoring.WatchingFile(PATH);
                monitoring.FileChange += Monitoring_FileChange;

                monitoring.Start();

                Console.WriteLine("If you want to exit please press ENTER or ESC");
                Console.ReadKey();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void Monitoring_FileChange(object sender, FileChangeEventArgs e)
        {
            if(sender is Monitoring monitoring)
            {
                monitoring.Stop();

                JsonRepository repository = new JsonRepository(e.Path);
                FileSetting fileSetting = new FileSetting(repository);
                monitoring.Initialization(fileSetting);

                monitoring.Start();
            }
        }

        private static bool IsMainInstance()
        {
            bool isMainIsntance;
            _mutex = new Mutex(true, "Main App", out isMainIsntance);

            return isMainIsntance;
        }
    }
}
