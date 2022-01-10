using CMSys.Core.Entities.Catalog;
using System;

namespace RMSysProj.ViewModels
{
    public class TrainerViewModel
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string PhotoUrl { get; set; }
        public int VisualOrder { get; set; }
        public string Description { get; set; }
        public TrainerGroup TrainerGroup { get; set; }
    }
}
