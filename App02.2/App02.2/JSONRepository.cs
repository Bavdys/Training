using System;
using System.IO;
using System.Text.Json;

namespace App02._2
{
    public class JSONRepository:IWriteRepository
    {
        public void Write(string path, object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("The argument should not be null");
            }

            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException("The string should not be empty");
            }

            string jsonString = JsonSerializer.Serialize(obj);

           if (!Directory.Exists(Path.GetDirectoryName(path)))
           {
               Directory.CreateDirectory(Path.GetDirectoryName(path));
           }

           File.WriteAllText(path, jsonString);
        }
    }
}
