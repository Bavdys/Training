using CMSys.Core.Entities.Catalog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RMSysProj.Areas.Admin.ViewModels
{
    public class TrainerEditViewModel
    {
        [DisplayName("Name")]
        public Guid Id { get; set; }

        [DisplayName("Order")]
        [Required(ErrorMessage = "Order is required")]
        public int VisualOrder { get; set; }

        [DisplayName("Description")]
        [StringLength(Trainer.DescriptionLength, ErrorMessage = "Max length is 4000")]
        public string Description { get; set; }
        
        [DisplayName("Trainer Groups")]
        public Guid GroupId { get; set; }

        public IEnumerable<TrainerGroup> TrainerGroups { get; set; }
        public IEnumerable<UserViewModel> UserViewModels { get; set; }
    }
}
