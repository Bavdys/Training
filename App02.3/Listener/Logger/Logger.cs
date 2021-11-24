using Listener.Attributes;
using Listener.Threshold;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Listener
{
    public class Logger:ILog
    {
        private void WriteMessage(object message,LevelValue levelValue)
        {
            foreach (var listener in Listener)
            {
                LoggerData loggingData = new LoggerData(Name, message, levelValue);

                listener.Write(loggingData);
            }
        }
        private TrackingPropertyAttribute[] GetAttributes(MemberInfo[] members)
        {
            List<TrackingPropertyAttribute> tempAttributes = new List<TrackingPropertyAttribute>();

            foreach (var itemMember in members)
            {
                TrackingPropertyAttribute itemTempAttribute = (TrackingPropertyAttribute)itemMember.GetCustomAttribute(typeof(TrackingPropertyAttribute));

                if (itemTempAttribute != null)
                {
                    tempAttributes.Add(itemTempAttribute);
                }
            }

            return tempAttributes.ToArray();
        }
        private string GetDataFromAttributes(TrackingPropertyAttribute[] trackingPropertyAttributes, object obj)
        {
            StringBuilder stringBuilder = new StringBuilder();

            foreach (var itemAttributes in trackingPropertyAttributes)
            {
                stringBuilder.Append($"{itemAttributes.Name} = " +
                    $"{((itemAttributes.IsProperty) ? obj.GetType().GetProperty(itemAttributes.Name).GetValue(obj) : obj.GetType().GetField(itemAttributes.Name).GetValue(obj))}   ");
            }

            return stringBuilder.ToString();
        }

        public Logger(string name)
        {
            Name = name;
        }

        public List<IListener> Listener { get; set; } = new List<IListener>();
        public string Name { get; set; }
        public ILevel Threshold { get; set; }
        public bool IsDebugEnabled
        {
            get
            {
                return !(Threshold.Threshold > LevelValue.DEBUG);
            }
        }
        public bool IsInfoEnabled
        {
            get
            {
                return !(Threshold.Threshold > LevelValue.INFO);
            }
        }
        public bool IsWarnEnabled
        {
            get
            {
                return !(Threshold.Threshold > LevelValue.WARN);
            }
        }
        public bool IsErrorEnabled
        {
            get
            {
                return !(Threshold.Threshold > LevelValue.ERROR);
            }
        }
        public bool IsFatalEnabled
        {
            get
            {
                return !(Threshold.Threshold > LevelValue.FATAL);
            }
        }

        public  void Debug(object message)
        {
            if(IsDebugEnabled)
            {
                WriteMessage(message,LevelValue.DEBUG);
            }
        }
        public  void Error(object message)
        {
            if(IsErrorEnabled)
            {
                WriteMessage(message,LevelValue.ERROR);
            }
        }
        public  void Fatal(object message)
        {
            if(IsFatalEnabled)
            {
                WriteMessage(message,LevelValue.FATAL);
            }
        }
        public  void Info(object message)
        {
            if(IsInfoEnabled)
            {
                WriteMessage(message,LevelValue.INFO);
            }
        }
        public  void Warn(object message)
        {
            if(IsWarnEnabled)
            {
                WriteMessage(message,LevelValue.WARN);
            }
        }
        public void Track(object obj)
        {
            Type type = obj.GetType();
            
            TrackingEntityAttribute entity =(TrackingEntityAttribute)Attribute.GetCustomAttribute(type, typeof(TrackingEntityAttribute));

            if(entity != null)
            {
                StringBuilder stringBuilder = new StringBuilder();

                TrackingPropertyAttribute[] properties = GetAttributes(type.GetProperties());
                TrackingPropertyAttribute[] fields = GetAttributes(type.GetFields());

                stringBuilder.Append(GetDataFromAttributes(properties,obj));
                stringBuilder.Append(GetDataFromAttributes(fields, obj));

                Info(stringBuilder.ToString());
            }
        }
    }
}
