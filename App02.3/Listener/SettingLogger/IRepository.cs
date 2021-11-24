using Listener.LoggerModels;

namespace Listener.SettingLogger
{
    public interface IRepository
    {
        string PathFile { get; }
        LoggerModel LoadFromFile();
    }
}
