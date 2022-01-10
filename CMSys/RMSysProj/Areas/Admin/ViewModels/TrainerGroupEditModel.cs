using CMSys.Core.Entities.Catalog;
using System;
using System.ComponentModel.DataAnnotations;

namespace RMSysProj.Areas.Admin.ViewModels
{
    public class TrainerGroupEditModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(CourseGroup.NameLength, ErrorMessage = "Max length is 64")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Order is required")]
        public int? VisualOrder { get; set; }

        [StringLength(CourseGroup.DescriptionLength, ErrorMessage = "Max length is 256")]
        public string Description { get; set; }
    }
}
