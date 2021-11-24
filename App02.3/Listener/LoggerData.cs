using Listener.Threshold;
namespace Listener
{
    public class LoggerData
    {
        public LoggerData(string loggerName, object messageObject, LevelValue threshold)
        {
            LoggerName = loggerName;
            MessageObject = messageObject;
            Threshold = threshold;
        }
        public LevelValue Threshold { get; set; }
        public string LoggerName { get; set; }
        public object MessageObject { get; set; }
    }
}
