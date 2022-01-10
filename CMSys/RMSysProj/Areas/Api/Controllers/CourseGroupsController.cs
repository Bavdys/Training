using CMSys.Core.Entities.Catalog;
using CMSys.Core.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RMSysProj.Areas.Admin.ViewModels;
using System;
using System.Collections.Generic;

namespace RMSysProj.Areas.Api.Controllers
{
    [Area("Api")]
    [Authorize(Roles = "Admin")]
    [ApiController]
    public class CourseGroupsController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        public CourseGroupsController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet("api/admin/coursegroups")]
        public IEnumerable<CourseGroup> CourseGroups()
        {
            return unitOfWork.CourseGroupRepository.All();      
        }

        [HttpGet("api/admin/coursegroups/{id}")]
        public ActionResult<CourseGroup> CourseGroupsById(Guid id)
        {
            var courseGroup = unitOfWork.CourseGroupRepository.Find(id);
            if (courseGroup == null)
            {
                return NotFound();
            }

            return Ok(courseGroup);
        }

        [HttpPost("api/admin/coursegroups")]
        public IActionResult CreateCourseGroup([FromBody]CourseGroupEditModel courseGroupEditModel)
        {
            if (courseGroupEditModel == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var courseGroup = new CourseGroup() 
            {
                Id = Guid.NewGuid(),
                Name = courseGroupEditModel.Name,
                VisualOrder = courseGroupEditModel.VisualOrder.Value,
                Description = courseGroupEditModel.Description
            };

            unitOfWork.CourseGroupRepository.Add(courseGroup);
            unitOfWork.Commit();

            return Ok(courseGroup);
        }

        [HttpPut("api/admin/coursegroups")]
        public IActionResult UpdateCourseGroup([FromBody] CourseGroupEditModel courseGroupEditModel)
        {
            if (courseGroupEditModel == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
           
            var courseGroup = unitOfWork.CourseGroupRepository.Find(courseGroupEditModel.Id);
            
            if (courseGroup == null)
            {
                return NotFound();
            }

            courseGroup.Name = courseGroupEditModel.Name;
            courseGroup.VisualOrder = courseGroupEditModel.VisualOrder.Value;
            courseGroup.Description = courseGroupEditModel.Description;

            unitOfWork.Commit();
            
            return Ok(courseGroup);
        }

        [HttpDelete("api/admin/coursegroups/{id}")]
        public IActionResult DeleteCourseGroup(Guid id)
        {
            var courseGroup = unitOfWork.CourseGroupRepository.Find(id);
            if (courseGroup == null)
            {
                return NotFound();
            }

            unitOfWork.CourseGroupRepository.Remove(courseGroup);
            unitOfWork.Commit();

            return Ok();
        }
    }
}
