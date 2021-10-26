namespace App2._4.MonitoringModel
{
    class AuditModel
    {
        public AuditModel()
        { 

        }

        public int Interval { get; set; }
        public int MaximumResponse { get; set; }
        public string Url { get; set; }
        public MailModel Mail { get; set; }
    }
}
