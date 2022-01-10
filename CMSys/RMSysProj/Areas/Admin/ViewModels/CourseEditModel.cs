using CMSys.Core.Entities.Catalog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RMSysProj.Areas.Admin.ViewModels
{
    public class CourseEditModel
    {
        public Guid Id { get; set; }

        [DisplayName("Name")]
        [Required(ErrorMessage = "Name is required")]
        [StringLength(Course.NameLength, ErrorMessage = "Max length is 64")]
        public string Name { get; set; }

        [DisplayName("Is New")]
        [Required(ErrorMessage = "IsNew is required")]
        public bool IsNew { get; set; }

        [DisplayName("Course Type")]
        [Required(ErrorMessage ="CourseTypeId is required")]
        public Guid CourseTypeId { get; set; }

        [DisplayName("Course Group")]
        [Required(ErrorMessage = "CourseGroupId is required")]
        public Guid CourseGroupId { get; set; }

        [DisplayName("Order")]
        [Required(ErrorMessage = "Order is required")]
        public int VisualOrder { get; set; }

        [DisplayName("Description")]
        [StringLength(Course.DescriptionLength, ErrorMessage = "Max length is 4000")]
        public string Description { get; set; }
       
        public IEnumerable<CourseType> CourseTypes { get; set; }
        public IEnumerable<CourseGroup> CourseGroups { get; set; }
    }
}
