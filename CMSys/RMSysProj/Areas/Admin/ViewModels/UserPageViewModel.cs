using RMSysProj.ViewModels;
using System.Collections.Generic;

namespace RMSysProj.Areas.Admin.ViewModels
{
    public class UserPageViewModel
    {
        public PageListViewModel PageListViewModel { get; set; }
        public IEnumerable<UserViewModel> Users { get; set; }

    }
}
