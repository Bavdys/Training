using App2._4.Message;
using Listener;
using Listener.SettingLogger;
using System.Net;
using System.Threading;

namespace App2._4
{
    public class Audit:IAuditor
    {
        private static Logger _logger;
        private static readonly string PATH = @"Configuration/LoggerSettings.json";
        private object _Logginlocker = new object();
        private object _Maillocker = new object();
        private Timer _timer;

        static Audit()
        {
            JsonRepository jsonRepository = new JsonRepository(PATH);
            FileSettingLogger fileSetting = new FileSettingLogger(jsonRepository);
            _logger = LoggerManager.CreateLogger(fileSetting);
        }

        private void CheckingSiteForAvailability(object obj)
        {
            try
            {
                WebRequest myWebRequest = WebRequest.Create(Url);

                myWebRequest.Timeout = MaximumResponse;

                WebResponse myWebResponse = myWebRequest.GetResponse();

                Logging($"Site {Url} is available");
            }
            catch (WebException ex)
            {
                //Send($"Site {Url} is not available");
            }
        }
        private void Logging(object message)
        {
            lock (_Logginlocker)
            {
                _logger.Info(message);
            }
        }
        private void Send(object message)
        {
            lock (_Maillocker)
            {
                Mail.Send(message);
            }
        }
        
        public Audit(int interval, int maximumResponse, string url, IMessage mail)
        {
            Interval = interval * 1000;
            MaximumResponse = maximumResponse * 1000;
            Url = url;
            Mail = mail;
        }
        
        public int Interval { get; set; }
        public int MaximumResponse { get; set; }
        public string Url { get; set; }
        public IMessage Mail { get; set; }

        public void StartChecking()
        {
            _timer = new Timer(CheckingSiteForAvailability, null, Interval, Interval);

        }
        public void StopChecking()
        {
            _timer.Dispose();
        }
    }
}
