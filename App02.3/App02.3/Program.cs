using Listener;
using Listener.SettingLogger;
using System;

namespace App02._3
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                JsonRepository jsonRepository = new JsonRepository(@"Configuration/appsettings.json");
                FileSettingLogger jSONConfiguration = new FileSettingLogger(jsonRepository);
                Logger logger = LoggerManager.CreateLogger(jSONConfiguration);

                logger.Warn("This is Warn Text");
                logger.Debug("This is Debug Text");

                ExampleClass person = new ExampleClass("Tom", "Harrison", 35);
                logger.Track(person);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
