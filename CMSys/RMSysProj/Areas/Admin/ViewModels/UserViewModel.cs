using System;

namespace RMSysProj.Areas.Admin.ViewModels
{
    public class UserViewModel
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Department { get; set; }
        public string Position { get; set; }
        public string Office { get; set; }
        public string Photo { get; set; }
        public string Active { get; set; }
    }
}
