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
    public class TrainerGroupsController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        public TrainerGroupsController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet("api/admin/trainergroups")]
        public IEnumerable<TrainerGroup> TrainerGroups()
        {
            return unitOfWork.TrainerGroupRepository.All();
        }

        [HttpGet("api/admin/trainergroups/{id}")]
        public ActionResult<CourseGroup> TrainerGroupsById(Guid id)
        {
            var trainerGroup = unitOfWork.TrainerGroupRepository.Find(id);
            if (trainerGroup == null)
            {
                return NotFound();
            }

            return Ok(trainerGroup);
        }

        [HttpPost("api/admin/trainergroups")]
        public IActionResult CreateTrainerGroup([FromBody] TrainerGroupEditModel trainerGroupEditModel)
        {
            if (trainerGroupEditModel == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var trainerGroup = new TrainerGroup()
            {
                Id = Guid.NewGuid(),
                Name = trainerGroupEditModel.Name,
                VisualOrder = trainerGroupEditModel.VisualOrder.Value,
                Description = trainerGroupEditModel.Description
            };

            unitOfWork.TrainerGroupRepository.Add(trainerGroup);
            unitOfWork.Commit();

            return Ok(trainerGroup);
        }

        [HttpPut("api/admin/trainergroups")]
        public IActionResult UpdateTrainerGroup([FromBody] TrainerGroupEditModel trainerGroupEditModel)
        {
            if (trainerGroupEditModel == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var trainerGroup = unitOfWork.TrainerGroupRepository.Find(trainerGroupEditModel.Id);
            if (trainerGroup == null)
            {
                return NotFound();
            }

            trainerGroup.Name = trainerGroupEditModel.Name;
            trainerGroup.VisualOrder = trainerGroupEditModel.VisualOrder.Value;
            trainerGroup.Description = trainerGroupEditModel.Description;

            unitOfWork.Commit();

            return Ok(trainerGroup);
        }

        [HttpDelete("api/admin/trainergroups/{id}")]
        public IActionResult DeleteTrainerGroup(Guid id)
        {
            var trainerGroup = unitOfWork.TrainerGroupRepository.Find(id);
            if (trainerGroup == null)
            {
                return NotFound();
            }

            unitOfWork.TrainerGroupRepository.Remove(trainerGroup);
            unitOfWork.Commit();

            return Ok();
        }
    }
}
