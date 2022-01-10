using CMSys.Core.Entities.Catalog;
using System.Collections.Generic;

namespace RMSysProj.ViewModels
{
    public class CoursePageViewModel
    {
        public IEnumerable<CourseType> CourseTypes { get; set; }
        public IEnumerable<CourseGroup> CourseGroups { get; set; }
        public PageListViewModel PageList { get; set; }
        public IEnumerable<CourseViewModel> Courses { get; set; }
    }
}
