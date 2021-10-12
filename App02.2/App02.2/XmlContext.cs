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
     class XmlContext : ISerialization
    {
        public  object Deserialization(string path, Type type)
        {
            XmlSerializer serializer = new XmlSerializer(type);

            using (Stream reader = new FileStream(path, FileMode.Open))
            {
                return serializer.Deserialize(reader);
            }
        }

        public  void Serialization(string path, object obj)
        {
            XmlSerializer serializer = new XmlSerializer(obj.GetType());

            using (Stream writer = new FileStream(path, FileMode.OpenOrCreate))
            {
                serializer.Serialize(writer, obj);
            }
        }
    }
}
