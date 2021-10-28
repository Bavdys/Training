using App03._1.Models;
using System;
using System.IO;
using System.Text.Json;

namespace App03._1
{
    public class JSONRepository : IRepository
    {
       
        public SensorsCollectionModel LoadFromFile(string path)
        {
            if (!File.Exists(path))
            {
                throw new ArgumentException("File does not exist");
            }

            string jsonString;

            using (StreamReader streamreader = new StreamReader(path))
            {
                jsonString = streamreader.ReadToEnd();
            }

            SensorsCollectionModel sensorsCollectionModel = JsonSerializer.Deserialize<SensorsCollectionModel>(jsonString);

            return sensorsCollectionModel;
        }
    }
}
