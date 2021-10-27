using System.Collections.Generic;

namespace Listener.LoggerModels
{
    public class LoggerModel
    {
        public LoggerModel()
        {

        }

        public string Name { get; set; }
        public string Level { get; set; }
        public List<ListenerModel> Listeners { get; set; } = new List<ListenerModel>();
    }
}
