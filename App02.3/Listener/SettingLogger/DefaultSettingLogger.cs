using Listener.Layout;
using Listener.Threshold;
using System;
using System.Reflection;

namespace Listener
{
    public class DefaultSettingLogger: ICreating
    {
        private string ASSEMBLY = "TextListener.dll";
        private string TYPE_LISTENER = "TextListener.TextListener";
        private string DEFAULT_LOGGER_NAME = "Default Logger";
        private LevelValue DEFAULT_LEVEL_VALUE = LevelValue.DEBUG;
        private string DEFAULT_LISTENER_NAME = "Default TextListener";
        private string DEFAULT_LESTENER_SOURCE = "Log/Log.txt";
        private string DEFAULT_LAYOUT_PATTERN_STRING = "%Date%Level%Logger%Message";

        public Logger CreateLogger()
        {
            Assembly assembly = Assembly.LoadFrom(ASSEMBLY);

            Type type = assembly.GetType(TYPE_LISTENER, true, true);
            
            Logger logger = new Logger(DEFAULT_LOGGER_NAME) { Threshold = new Level(DEFAULT_LEVEL_VALUE) };

            IListener listener = (IListener)Activator.CreateInstance(type,
                   new object[] { DEFAULT_LISTENER_NAME, DEFAULT_LESTENER_SOURCE, new PatternLayout(DEFAULT_LAYOUT_PATTERN_STRING) });

            logger.Listener.Add(listener);

            return logger;
        }
    }
}
