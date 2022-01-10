using CMSys.Common.Paging;
using CMSys.Core.Entities.Catalog;
using CMSys.Core.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RMSysProj.Areas.Admin.ViewModels;
using RMSysProj.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace RMSysProj.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles="Admin")]
    public class CourseController : Controller
    {
        private const int PER_PAGE = 10;
        private readonly IUnitOfWork unitOfWork;
        public CourseController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Route("admin/courses")]
        [Route("admin/courses/page/{page}/{filter?}")]
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
            if (pagedListCourse == null)
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
                            Id = itemTrainer.TrainerId,
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
        [Route("admin/courses/{id}")]
        public IActionResult CoursesById(Guid id)
        {
            var course = unitOfWork.CourseRepository.Find(id);
            if (course == null)
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

            CourseViewModel courseViewModel = new()
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

        [HttpGet]
        [Route("admin/courses/trainers/{id}")]
        public IActionResult CourseTrainers(Guid id)
        {
            var course = unitOfWork.CourseRepository.Find(id);
            if (course == null)
            {
                return NotFound();
            }
            
            ViewData["TitleCourse"] = course.Name;

            return View("UpdateTrainers");
        }

        [HttpGet]
        [Route("admin/courses/create")]
        public IActionResult Create()
        {
            CourseEditModel courseListViewModel = new()
            {
                CourseTypes = unitOfWork.CourseTypeRepository.All(),
                CourseGroups = unitOfWork.CourseGroupRepository.All()
            };

            return View("CreateCourse", courseListViewModel);
        }

        [HttpPost]
        [Route("admin/courses/create")]
        public IActionResult Create(CourseEditModel courseEditModel)
        {
            if (!ModelState.IsValid)
            {
                return View("CreateCourse", courseEditModel);
            }
            
            var course = new Course
            {
                Id = Guid.NewGuid(),
                Name = courseEditModel.Name,
                VisualOrder = courseEditModel.VisualOrder,
                IsNew = courseEditModel.IsNew,
                CourseTypeId = courseEditModel.CourseTypeId,
                CourseGroupId = courseEditModel.CourseGroupId,
                Description = string.IsNullOrWhiteSpace(courseEditModel.Description) ? null : courseEditModel.Description
            };

            unitOfWork.CourseRepository.Add(course);
            unitOfWork.Commit();
            
            return RedirectToAction("Courses");
        }

        [HttpGet]
        [Route("admin/courses/update/{id}")]
        public IActionResult Update(Guid id)
        {
            var course = unitOfWork.CourseRepository.Find(id);
            if (course == null)
            {
                return NotFound();
            }

            CourseEditModel courseListViewModel = new()
            {
                Id=course.Id,
                Name = course.Name,
                IsNew = course.IsNew,
                CourseTypeId = course.CourseTypeId,
                CourseGroupId = course.CourseGroupId,
                VisualOrder = course.VisualOrder,
                Description = course.Description,
                CourseTypes = unitOfWork.CourseTypeRepository.All(),
                CourseGroups = unitOfWork.CourseGroupRepository.All()
            };

            return View("UpdateCourse", courseListViewModel);
        }

        [HttpPost]
        [Route("admin/courses/update/{id}")]
        public IActionResult Update(CourseEditModel courseEditModel)
        {
            if (!ModelState.IsValid)
            {
                return View("UpdateCourse", courseEditModel);
            }

            var course = unitOfWork.CourseRepository.Find(courseEditModel.Id);
            if (course == null)
            {
                return NotFound();
            }

            course.Name = courseEditModel.Name;
            course.IsNew = courseEditModel.IsNew;
            course.CourseTypeId = courseEditModel.CourseTypeId;
            course.CourseGroupId = courseEditModel.CourseGroupId;
            course.VisualOrder =courseEditModel.VisualOrder;
            course.Description = string.IsNullOrWhiteSpace(courseEditModel.Description) ? null : courseEditModel.Description;

            unitOfWork.Commit();
           
            return RedirectToAction("Courses");
        }

        [HttpGet]
        [Route("admin/courses/delete/{id}")]
        public  IActionResult Delete(Guid id)
        {
            var course = unitOfWork.CourseRepository.Find(id);
            if (course == null)
            {
                return NotFound();
            }

            unitOfWork.CourseRepository.Remove(course);
            unitOfWork.Commit();

            return RedirectToAction("Courses");
        }

    }
}
