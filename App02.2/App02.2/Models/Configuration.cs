using System.Collections.Generic;
using System.Text;

namespace App02._2
{
    public class Configuration
    {
        public Configuration()
        {
            Logins = new List<Login>();
        }

        public List<Login> Logins { get; set; }

        public string IncorrectLoginToString()
        {
            StringBuilder builderStringIncorrectLogin = new StringBuilder();

            foreach (var itemLogin in Logins)
            {
                if (!itemLogin.IsCorrectLogin)
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
