using Listener.LoggerModels;
using System;
using System.IO;
using System.Text.Json;

namespace Listener.SettingLogger
{
    public class JsonRepository : IRepository
    {
        public JsonRepository(string path)
        {
            PathFile = path;
        }

        public string PathFile { get; }

        public LoggerModel LoadFromFile()
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

            LoggerModel loggerModel = JsonSerializer.Deserialize<LoggerModel>(jsonString);

            return loggerModel;
        }
    }
}
