using Listener.Threshold;
using System.Collections.Generic;

namespace Listener
{
    public interface ILogger
    {
        string Name { get; set; }
        ILevel Threshold { get; set; }
        List<IListener> Listener { get; set; }
    }
}
