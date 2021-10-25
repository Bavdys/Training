using System;
using System.Text;

namespace Listener.Layout
{
    public class PatternLayout : ILayout
    {
        public PatternLayout(string patternString)
        {
            PatternString = patternString;
        }

        public string PatternString { get; set; }

        public string Format(LoggerData loggingData)
        {
            string[] splitString = PatternString.Split('%');
            StringBuilder resultString = new StringBuilder();

            foreach (var tempString in splitString)
            {
                if (string.IsNullOrEmpty(tempString))
                {
                    continue;
                }

                PatternValue patternValue = (PatternValue)Enum.Parse(typeof(PatternValue), tempString);

                switch (patternValue)
                {
                    case PatternValue.Date:
                        resultString.Append($"{DateTime.Now}   ");
                        break;
                    case PatternValue.Level:
                        resultString.Append($"[{loggingData.Threshold}]   ");
                        break;
                    case PatternValue.Logger:
                        resultString.Append($"{loggingData.LoggerName}   ");
                        break;
                    case PatternValue.Message:
                        resultString.Append($"{loggingData.MessageObject}   ");
                        break;
                }
            }

            return resultString.ToString();
        }
    }
}
