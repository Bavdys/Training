using System.Collections.Generic;

namespace App2._4
{
    class Monitoring
    {
        List<IAuditor> Audits { get; set; } = new List<IAuditor>();

        public void Initialization(ISetting setting)
        {
            Audits=new List<IAuditor>(setting.SettingMonitoring());
        }
        public void Start()
        {
            foreach (var audit in Audits)
            {
                audit.StartChecking();
            }
        }
        public void Stop()
        {
            foreach(var audit in Audits)
            {
                audit.StopChecking();
            }
        }
    }
}
