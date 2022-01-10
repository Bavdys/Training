using CMSys.Common.Paging;
using CMSys.Core.Entities.Catalog;
using CMSys.Core.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RMSysProj.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace RMSysProj.Controllers
{
    [Authorize]
    public class CourseController : Controller
    {
        private const int PER_PAGE = 10;
        private readonly IUnitOfWork unitOfWork;
        public CourseController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Route("/")]
        [Route("courses")]
        [Route("courses/page/{page}/{filter?}")]
        public IActionResult Courses(int page = 1, Guid? group = null, Guid? type = null)
        {
            PageInfo pageInfo = new(page, PER_PAGE);
            Expression<Func<Course, bool>> expression;

            if (group.HasValue && type.HasValue)
            {
                expression = item => group.Value == item.CourseGroupId && type.Value == item.CourseTypeId;
            }
            else if (group.HasValue || type.HasValue)
            {
                expression = item => (group.HasValue && group.Value == item.CourseGroupId) || (type.HasValue && type.Value == item.CourseTypeId);
            }
            else
            {
                expression = item => true;
            }

            var pagedListCourse = unitOfWork.CourseRepository.GetPagedList(pageInfo, expression);
            if(pagedListCourse == null)
            {
                return NotFound();
            }

            PageListViewModel pageListViewModel = new()
            {
                Total = pagedListCourse.Total,
                Page = pagedListCourse.Page,
                PerPage = pagedListCourse.PerPage,
                TotalPages = pagedListCourse.TotalPages,
                CanNext = pagedListCourse.CanNext,
                CanPrevious = pagedListCourse.CanPrevious,
                From = pagedListCourse.From,
                To = pagedListCourse.To
            };
            IEnumerable<CourseViewModel> courseViewModel = pagedListCourse.Items.Select(itemCourse =>
            {
                return new CourseViewModel()
                {
                    Id = itemCourse.Id,
                    Name = itemCourse.Name,
                    IsNew = itemCourse.IsNew,
                    VisualOrder = itemCourse.VisualOrder,
                    Description = itemCourse.Description,
                    CourseType = itemCourse.CourseType,
                    CourseGroup = itemCourse.CourseGroup,
                    TrainerViewModels = itemCourse.Trainers.Select(itemTrainer =>
                    {
                        return new TrainerViewModel()
                        {
                            Id = itemTrainer.Trainer.Id,
                            VisualOrder = itemTrainer.Trainer.VisualOrder,
                            Description = itemTrainer.Trainer.Description,
                            TrainerGroup = itemTrainer.Trainer.TrainerGroup,
                            FullName = itemTrainer.Trainer.User.FullName,
                            PhotoUrl = Convert.ToBase64String(itemTrainer.Trainer.User.Photo)
                        };
                    })
                };
            });
            CoursePageViewModel coursePageViewModel = new()
            {
                CourseTypes = unitOfWork.CourseTypeRepository.All(),
                CourseGroups = unitOfWork.CourseGroupRepository.All(),
                PageList = pageListViewModel,
                Courses = courseViewModel
            };

            ViewBag.SelectedType = type;
            ViewBag.SelectedGroup = group;

            return View("Courses", coursePageViewModel);
        }

        [HttpGet]
        [Route("courses/{id}")]
        public IActionResult CoursesById(Guid id)
        {
            var course = unitOfWork.CourseRepository.Find(id);
            if(course == null)
            {
                return NotFound();
            }

            IEnumerable<TrainerViewModel> trainerViewModels = course.Trainers.Select(item =>
            {
               return new TrainerViewModel()
               {
                   Id = item.Trainer.Id,
                   VisualOrder = item.Trainer.VisualOrder,
                   Description = item.Trainer.Description,
                   TrainerGroup = item.Trainer.TrainerGroup,
                   FullName = item.Trainer.User.FullName,
                   PhotoUrl = Convert.ToBase64String(item.Trainer.User.Photo)
               };
            });

            CourseViewModel courseViewModel = new ()
            {
                Id = course.Id,
                Name = course.Name,
                IsNew = course.IsNew,
                VisualOrder = course.VisualOrder,
                Description = course.Description,
                CourseType = course.CourseType,
                CourseGroup = course.CourseGroup,
                TrainerViewModels = trainerViewModels
            };

            return View("Details", courseViewModel);
        }
    }
}
