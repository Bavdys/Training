using RMSysProj.ViewModels;
using System;
using System.Collections.Generic;

namespace RMSysProj.Areas.Admin.ViewModels
{
    public class CourseTrainersPageViewModel
    {
        public Guid CourseId { get; set; }
        public IEnumerable<TrainerViewModel> AllTrainers { get; set; }
        public IEnumerable<TrainerViewModel> CourseTrainers { get; set; }
    }
}
