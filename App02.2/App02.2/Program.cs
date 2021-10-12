using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace App02._2
{
    class Program
    {
        static void Main(string[] args)
        {
            XmlContext xmlConfiguration = new XmlContext();
            Configuration config = (Configuration)xmlConfiguration.Deserialization(@"Config\XMLConfiguration.xml", typeof(Configuration));
            xmlConfiguration.Serialization(@"Config\XMLConfigurationNew.xml", config);
          


        }
    }
}
