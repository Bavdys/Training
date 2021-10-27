using App2._4.MonitoringModel;
using System;
using System.IO;
using System.Text.Json;

namespace App2._4.MonitoringSetting
{
    public class JsonRepository : IRepository
    {
        public JsonRepository(string path)
        {
            if(string.IsNullOrEmpty(path))
            {
                throw new ArgumentException("Value cannot by empty");
            }

            PathFile = path;
        }

        public string PathFile { get; }

        public AuditsCollectionModel LoadFromFile()
        {
            if (!File.Exists(PathFile))
            {
                throw new ArgumentException();
            }

            string jsonString;

            using (StreamReader streamreader = new StreamReader(PathFile))
            {
                jsonString = streamreader.ReadToEnd();
            }

            AuditsCollectionModel auditsCollectionModel = JsonSerializer.Deserialize<AuditsCollectionModel>(jsonString);

            return auditsCollectionModel;
        }
    }
}
