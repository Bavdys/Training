using System;
using System.IO;
using Listener;
using Listener.Layout;

namespace TextListener
{
    public class TextListener : IListener
    { 
        public TextListener(string name, string filePath, ILayout layout)
        {
            Name = name;
            Source = filePath;
            Layout = layout;
        }

        public string Name { get; set; }
        public string Source { get; set; }
        public ILayout Layout { get; set; }

        public  void Write(LoggerData loggerData)
        {
            if (loggerData == null)
            {
                throw new ArgumentNullException("Object cannot be null");
            }

            string resultDataString = Layout.Format(loggerData);

            if (!Directory.Exists(Path.GetDirectoryName(Source)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(Source));
            }

            using (StreamWriter streamWriter=new StreamWriter(Source,true))
            {
                streamWriter.WriteLine(resultDataString);
            }
        }
    }
}
