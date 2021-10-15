using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace App02._2
{
    class Program
    {
        static void Main(string[] args)
        {
            XMLFileManager xmlConfiguration = new XMLFileManager();

            XDocument xmlDocument = xmlConfiguration.ReadXMLFile(@"Config\XMLConfiguration.xml");

            Configuration configuration = new Configuration();

            configuration=ConfigurationContext.Initialization(xmlDocument);
            
            ConfigurationContext.MigrationConfigurationToJson(configuration);
        }
         
    };
}
