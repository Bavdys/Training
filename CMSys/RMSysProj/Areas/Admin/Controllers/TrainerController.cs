using CMSys.Core.Entities.Catalog;
using CMSys.Core.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RMSysProj.Areas.Admin.ViewModels;
using RMSysProj.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RMSysProj.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class TrainerController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        public TrainerController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Route("admin/trainers")]
        public IActionResult Trainers()
        {
            IEnumerable<TrainerViewModel> trainerViewModels = unitOfWork.TrainerRepository.All().Select(item =>
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

            return View("Trainers", trainerViewModels);
        }

        [HttpGet]
        [Route("admin/trainers/create")]
        public IActionResult CreateTrainer()
        {
            var allUserTrainers = unitOfWork.TrainerRepository.All().Select(item=>item.User);
            IEnumerable<UserViewModel> userViewModels = unitOfWork.UserRepository.Filter(item => !allUserTrainers.Contains(item)).Select(item =>
            {
                return new UserViewModel() 
                { 
                    Id = item.Id, 
                    FullName = item.FullName 
                };
            });
            TrainerEditViewModel trainerEditViewModel = new()
            {
                TrainerGroups = unitOfWork.TrainerGroupRepository.All(),
                UserViewModels = userViewModels
            };  

            return View("AddTrainers",trainerEditViewModel);
        }

        [HttpPost]
        [Route("admin/trainers/create")]
        public IActionResult CreateTrainer(TrainerEditViewModel trainerEditViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("AddTrainers", trainerEditViewModel);
            }

            var trainer = new Trainer
            {
                Id = trainerEditViewModel.Id,
                VisualOrder = trainerEditViewModel.VisualOrder,
                Description = string.IsNullOrWhiteSpace(trainerEditViewModel.Description) ? null : trainerEditViewModel.Description,
                TrainerGroupId = trainerEditViewModel.GroupId
            };

            unitOfWork.TrainerRepository.Add(trainer);
            unitOfWork.Commit();
            
            return RedirectToAction("Trainers");
        }

        [HttpGet]
        [Route("admin/trainers/update/{id}")]
        public IActionResult UpdateTrainer(Guid id)
        {
            var trainer = unitOfWork.TrainerRepository.Find(id);
            if (trainer == null)
            {
                return NotFound();
            }

            TrainerEditViewModel trainerEditViewModel = new()
            {
                Id = trainer.Id,
                GroupId = trainer.TrainerGroupId,
                VisualOrder = trainer.VisualOrder,
                Description = trainer.Description,
                TrainerGroups = unitOfWork.TrainerGroupRepository.All()
            };
            ViewData["FullName"] = trainer.User.FullName;

            return View("UpdateTrainers", trainerEditViewModel);
        }

        [HttpPost]
        [Route("admin/trainers/update/{id}")]
        public IActionResult UpdateTrainer(TrainerEditViewModel trainerEditModel)
        {
            if (!ModelState.IsValid)
            {
                return View("UpdateCourses", trainerEditModel);
            }

            var trainer = unitOfWork.TrainerRepository.Find(trainerEditModel.Id);
            if (trainer == null)
            {
                return NotFound();
            }

            trainer.TrainerGroupId = trainerEditModel.GroupId;
            trainer.VisualOrder = trainerEditModel.VisualOrder;
            trainer.Description = string.IsNullOrWhiteSpace(trainerEditModel.Description) ? null : trainerEditModel.Description;

            unitOfWork.Commit();
            
            return RedirectToAction("Trainers");
        }

        [HttpGet]
        [Route("admin/trainers/delete/{id}")]
        public IActionResult DeleteTrainer(Guid id)
        {
            var trainer = unitOfWork.TrainerRepository.Find(id);
            if (trainer == null)
            {
                return NotFound();
            }

            unitOfWork.TrainerRepository.Remove(trainer);
            unitOfWork.Commit();

            return RedirectToAction("Trainers");
        }
    }
}
