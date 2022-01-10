using CMSys.Core.Entities.Catalog;
using System.Collections.Generic;

namespace RMSysProj.ViewModels
{
    public class TrainerPageViewModel
    {
        public TrainerGroup TrainerGroup { get; set; }
        public IEnumerable<TrainerViewModel> Trainers { get; set; }
    }
}
