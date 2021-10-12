using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace App02._2
{
    
    class JsonContext : ISerialization
    {
        public object Deserialization(string path, Type type)
        {
            throw new NotImplementedException();
        }

        public  void Serialization(string path, object obj)
        {
           
        }
    }
}
