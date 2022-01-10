using System;
using System.Collections.Generic;

namespace RMSysProj.Areas.Admin.ViewModels
{
    public class UserDetailsViewModel
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Department { get; set; }
        public string Position { get; set; }
        public string Office { get; set; }
        public string Photo { get; set; }
        public DateTime StartDate { get; set; }
        public IEnumerable<RoleViewModel> Roles { get; set; }
    }
}
