using System;
using System.Reflection;
using Listener.LoggerModels;
using Listener.Threshold;
using Listener.Layout;
using Listener.SettingLogger;

namespace Listener
{
    public class FileSettingLogger: ICreating
    {
        private IRepository _repository;

        public FileSettingLogger(IRepository repository)
        {
            _repository = repository;
        }

        public Logger CreateLogger()
        {
            LoggerModel loggerModel = _repository.LoadFromFile();
            
            Logger logger = new Logger(loggerModel.Name) { Threshold = new Level((LevelValue)Enum.Parse(typeof(LevelValue), loggerModel.Level)) };

            foreach (var listenerModel in loggerModel.Listeners)
            {    
                Assembly assembly = Assembly.LoadFrom($"{listenerModel.Assembly}.dll");

                Type type = assembly.GetType($"{listenerModel.Type}",true,true);

                IListener listener = (IListener)Activator.CreateInstance(type,
                    new object[] {listenerModel.Name, listenerModel.Source, new PatternLayout(listenerModel.Layout.PatternString) });
                    
                logger.Listener.Add(listener);
            }

            return logger;
        }
    }
}
