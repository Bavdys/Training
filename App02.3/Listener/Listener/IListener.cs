using Listener.Layout;

namespace Listener
{
    public interface IListener
    {
        string Name { get; set; }
        string Source { get; set; }
        ILayout Layout { get; set; }

        void Write(LoggerData loggingData);
    }
}
