using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Listener;
using Listener.Layout;
using System;
using System.IO;

namespace WordListener
{
    public class WordListener : IListener
    {
        public WordListener(string name, string filePath, ILayout layout)
        {
            Name = name;
            Source = filePath;
            Layout = layout;
        }

        public string Name { get; set; }
        public string Source { get; set; }
        public ILayout Layout { get; set; }


        public void Write(LoggerData loggerData)
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

            if (!File.Exists(Source))
            {
                using (WordprocessingDocument wordprocessingDocument = WordprocessingDocument.Create(Source, WordprocessingDocumentType.Document))
                {
                    MainDocumentPart mainPart = wordprocessingDocument.AddMainDocumentPart();
                    mainPart.Document = new Document();

                    Body body = mainPart.Document.AppendChild(new Body());
                    Paragraph para = body.AppendChild(new Paragraph());
                    Run run = para.AppendChild(new Run());
                    run.AppendChild(new Text(resultDataString));
                }
            }
            else
            {
                using (WordprocessingDocument wordprocessingDocument = WordprocessingDocument.Open(Source, true))
                {
                    {
                        Body body = wordprocessingDocument.MainDocumentPart.Document.Body;
                       
                        Paragraph paragraph =  body.AppendChild(new Paragraph());
                        Run run = paragraph.AppendChild(new Run());
                        run.AppendChild(new Text(resultDataString));
                    }
                }
            }
        }
    }
}
