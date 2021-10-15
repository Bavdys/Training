using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace App02._2
{
    class ConfigurationContext
    {   
        public static Configuration Initialization(XDocument xmlData)
        {
            if(xmlData == null)
            {
                throw new ArgumentNullException("The argument should not be null");
            }

            Configuration configuration = new Configuration
            {
                Logins = new List<Login>
                (from itemLogin in xmlData.Element("config").Elements("login")
                 select new Login
                 {
                     Name = itemLogin.Attribute("name").Value,
                     Windows = new List<Window>
                     (from itemWindow in itemLogin.Elements("window")
                      select new Window
                      {
                          Title = itemWindow.Attribute("title").Value,
                          Top = itemWindow.Element("top")?.Value != null ? Convert.ToInt32(itemWindow.Element("top").Value) : (int?)null,
                          Left = itemWindow.Element("left")?.Value != null ? Convert.ToInt32(itemWindow.Element("left").Value) : (int?)null,
                          Width = itemWindow.Element("width")?.Value != null ? Convert.ToInt32(itemWindow.Element("width").Value) : (int?)null,
                          Height = itemWindow.Element("height")?.Value != null ? Convert.ToInt32(itemWindow.Element("height").Value) : (int?)null
                      })
                 })
            };

            return configuration;
        }
        public static void MigrationConfigurationToJson(Configuration configuration)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException("The argument should not be null");
            }

            JSONFileManager jSONFileManager = new JSONFileManager();
            
            foreach (var itemLogin in configuration.Logins)
            {
                string path = $"Config\\{itemLogin.Name}\\{itemLogin.Name}.json";
                
                List <Window> windows = new List<Window>();
                
                foreach (var itemWindow in itemLogin.Windows)
                {
                    windows.Add(new Window(itemWindow.Title, itemWindow.Top, itemWindow.Left, itemWindow.Width, itemWindow.Height));
                }

                jSONFileManager.WriteJsonFile(windows, path);
            }
        }
    }
}
