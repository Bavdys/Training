using CMSys.Core.Entities.Catalog;
using CMSys.Core.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RMSysProj.Areas.Admin.ViewModels;
using RMSysProj.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RMSysProj.Areas.Api.Controllers
{
    [Area("Api")]
    [Authorize(Roles = "Admin")]
    [ApiController]
    public class CourseTrainerController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        public CourseTrainerController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Route("api/admin/courses/trainers/{id}")]
        public IActionResult CourseTrainers(Guid id)
        {
            var course = unitOfWork.CourseRepository.Find(id);
            if(course == null)
            {
                return NotFound();
            }
            var trainers = course.Trainers.Select(item => item.Trainer);

            IEnumerable<TrainerViewModel> allTrainerViewModels = unitOfWork.TrainerRepository.Filter(item => !trainers.Contains(item)).Select(item =>
            {
                return new TrainerViewModel()
                {
                    Id = item.Id,
                    VisualOrder = item.VisualOrder,
                    Description = item.Description,
                    TrainerGroup = item.TrainerGroup,
                    FullName = item.User.FullName,
                    PhotoUrl = Convert.ToBase64String(item.User.Photo)
                };
            });
            IEnumerable<TrainerViewModel> courseTrainerViewModels = trainers.Select(item =>
            {
                return new TrainerViewModel()
                {
                    Id = item.Id,
                    VisualOrder = item.VisualOrder,
                    Description = item.Description,
                    TrainerGroup = item.TrainerGroup,
                    FullName = item.User.FullName,
                    PhotoUrl = Convert.ToBase64String(item.User.Photo)
                };
            });
            CourseTrainersPageViewModel courseTrainersPageViewModel = new()
            {
                CourseId = id,
                AllTrainers = allTrainerViewModels,
                CourseTrainers = courseTrainerViewModels
            };
           
            return Ok(courseTrainersPageViewModel);
        }

        [HttpPost]
        [Route("api/admin/courses/trainers")]
        public IActionResult UpdateCourseTrainers([FromBody]CourseTrainerEditModel courseTrainerEditModel )
        {
            if (courseTrainerEditModel == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            CourseTrainer courseTrainer = new()
            {
                CourseId = courseTrainerEditModel.IdCourse,
                TrainerId = courseTrainerEditModel.IdTrainer
            };

            unitOfWork.CourseTrainerRepository.Add(courseTrainer);
            unitOfWork.Commit();

            return Ok(courseTrainer);
        }

        [HttpDelete]
        [Route("api/admin/courses/trainers/{id}")]
        public IActionResult DeleteCourseTrainers(Guid id, Guid idTrainer)
        {
            var courseTrainer = unitOfWork.CourseTrainerRepository.Find(id,idTrainer);
            if (courseTrainer == null)
            {
                return NotFound();
            }

            unitOfWork.CourseTrainerRepository.Remove(courseTrainer);
            unitOfWork.Commit();

            return Ok();
        }
    }
}
