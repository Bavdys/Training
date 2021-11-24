using App2._4.MonitoringModel;
using System.Collections.Generic;
using System;

namespace App2._4.MonitoringSetting
{
    public class FileSetting : ISetting
    {
        private IRepository _repository;

        public FileSetting(IRepository repository)
        {
            if(repository == null)
            {
                throw new ArgumentNullException("Value cannot by null");
            }

            _repository = repository;
        }
       
        public List<IAuditor> SettingMonitoring()
        {
            AuditsCollectionModel auditsCollectionModel = _repository.LoadFromFile();
            List<IAuditor> audits = new List<IAuditor>();
            
            foreach (var audit in auditsCollectionModel.Audits)
            {
                Email mail = new Email(audit.Mail.RecipientName, audit.Mail.RecipientMail);
                audits.Add(new Audit(audit.Interval, audit.MaximumResponse, audit.Url, mail));
            }

            return audits;
        }
    }
}
