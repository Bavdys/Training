using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace App02._2
{
    interface ISerialization
    {
        void Serialization(string path,object obj);
        object Deserialization(string path,Type type);
    }
}
