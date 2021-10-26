using App2._4.MonitoringSetting;
using System;
using System.IO;
using System.Threading;

namespace App2._4
{
    class Program
    {
        private static Mutex _mutex;
        private static Monitoring _monitoring = new Monitoring();
        private static readonly string PATH = @"Configuration/AuditsSettings.json";
        static void Main(string[] args)
        {
            if (!IsMainInstance())
            {
                return;
            }
            try
            {
                SettingMonitoring();

                WatchingFile();

                _monitoring.Start();

                Console.WriteLine("If you want to exit please press ENTER or ESC");
                Console.ReadKey();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private static void SettingMonitoring()
        {
            JsonRepository repository = new JsonRepository(PATH);
            FileSetting fileSetting = new FileSetting(repository);
            _monitoring.Initialization(fileSetting);
        }
        private static bool IsMainInstance()
        {
            bool isMainIsntance;
            _mutex = new Mutex(true, "Main App", out isMainIsntance);

            return isMainIsntance;
        }
        private static void WatchingFile()
        {
            string directoryPath =  Path.GetDirectoryName(PATH);
            string filePath = Path.GetFileName(PATH);
            
            FileSystemWatcher fsw = new FileSystemWatcher(directoryPath,filePath);
            
            fsw.Changed += new FileSystemEventHandler(OnChanged);
            fsw.EnableRaisingEvents = true;
        }
        private static void OnChanged(object sender, FileSystemEventArgs e)
        {
            _monitoring.Stop();

            SettingMonitoring();
            
            _monitoring.Start();
        }
    }
}
