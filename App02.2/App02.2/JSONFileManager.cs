using System;
using System.IO;
using System.Text.Json;

namespace App02._2
{
    class JSONFileManager
    {
        public void WriteJsonFile(object obj, string path)
        {
            if(obj == null)
            {
                throw new ArgumentNullException("The argument should not be null");
            }
            
            if(string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException("The string should not be empty");
            }
            
            string jsonString = JsonSerializer.Serialize(obj);
            
            if(!Directory.Exists(Path.GetDirectoryName(path)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(path));
            }

            File.WriteAllText(path, jsonString);
        }
    }
}
