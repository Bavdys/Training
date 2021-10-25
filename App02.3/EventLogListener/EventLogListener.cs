using Listener;
using Listener.Layout;
using System.Diagnostics;

namespace EventLogListener
{
    public class EventLogListener : IListener
    {
        public EventLogListener(string name, string filePath, ILayout layout)
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
            string resultDataString = Layout.Format(loggerData);

            if (!EventLog.SourceExists(Source))
            {
                EventLog.CreateEventSource(Source, Name);
            }
            
            using (EventLog myLog = new EventLog())
            {
                myLog.Source = Source;

                myLog.WriteEntry(resultDataString, EventLogEntryType.Information);
            }
        }
    }
}
