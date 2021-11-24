using System.Collections.Generic;
using System.Text;

namespace App02._2
{
    public class Configuration
    {
        public Configuration()
        {
            
        }

        public List<Login> Logins { get; set; } = new List<Login>();

        public void LoadFromFile(string path, IReadRepository repository)
        {
            Logins = new List<Login>(repository.Read(path));
        }
        public void SaveLoginsConfigurationToJson(IWriteRepository repository)
        {
            foreach (var itemLogin in Logins)
            {
                string path = $"Config\\{itemLogin.Name}\\{itemLogin.Name}.json";

                List<Window> windows = new List<Window>();

                foreach (var itemWindow in itemLogin.Windows)
                {
                    windows.Add(itemWindow.GetWindow(itemWindow.Title, itemWindow.Top, itemWindow.Left, itemWindow.Width, itemWindow.Height));
                }

                repository.Write(path, windows);
            }
        }
        public string IncorrectLogins()
        {
            StringBuilder builderStringIncorrectLogin = new StringBuilder();

            foreach (var itemLogin in Logins)
            {
                if (!itemLogin.IsCorrectLogin())
                {
                    builderStringIncorrectLogin.Append($"{itemLogin}");
                }
            }

            return builderStringIncorrectLogin.ToString();
        }
        public override string ToString()
        {
            StringBuilder configurationInformation = new StringBuilder();

            foreach (var itemLogin in Logins)
            {
                configurationInformation.Append($"{itemLogin}");
            }

            return configurationInformation.ToString();
        }
    }
}
