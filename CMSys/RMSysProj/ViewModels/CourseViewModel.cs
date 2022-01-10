using CMSys.Core.Entities.Catalog;
using System;
using System.Collections.Generic;

namespace RMSysProj.ViewModels
{
    public class CourseViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int VisualOrder { get; set; }
        public string Description { get; set; }
        public bool IsNew { get; set; }
        public CourseType CourseType { get; set; }
        public CourseGroup CourseGroup { get; set; }
        public IEnumerable<TrainerViewModel> TrainerViewModels { get; set; }


    }
}
