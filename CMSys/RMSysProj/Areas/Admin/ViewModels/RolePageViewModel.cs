using System;
using System.Collections.Generic;

namespace RMSysProj.Areas.Admin.ViewModels
{
    public class RolePageViewModel
    {
        public Guid UserId { get; set; }
        public IEnumerable<RoleViewModel> AllRoles { get; set; }
        public IEnumerable<RoleViewModel> UserRoles { get; set; }
    }
}
