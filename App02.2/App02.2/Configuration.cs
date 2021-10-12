using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace App02._2
{
    [Serializable]
    [XmlRoot("config")]
    public class Configuration
    {
        [XmlElement(ElementName = "login")]
        public List<Login> Logins { get; set; }

        public Configuration() { }
        
        public string IncorrectLoginToString()
        {
            StringBuilder builderStringIncorrectLogin = new StringBuilder();

            foreach(var itemLogin in Logins)
            {
               if(!itemLogin.IsCorrectLogin)
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
