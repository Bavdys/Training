using CMSys.Core.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RMSysProj.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RMSysProj.Controllers
{
    [Authorize]
    public class TrainerController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        
        public TrainerController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Route("trainers")]
        public IActionResult Trainers()
        {
            List<TrainerPageViewModel> trainerListViewModels = new();
            var trainerGroups = unitOfWork.TrainerGroupRepository.All();
            if (trainerGroups == null)
            {
                return NotFound();
            }

            foreach (var item in trainerGroups)
            {
                var trainers = unitOfWork.TrainerRepository.Filter(itemTrainer => itemTrainer.TrainerGroupId == item.Id);
                if (trainers == null)
                {
                    return NotFound();
                }

                IEnumerable<TrainerViewModel> trainerViewModels = trainers.Select(itemTrainer =>
                {
                    return new TrainerViewModel()
                    {
                        Id = itemTrainer.Id,
                        VisualOrder = itemTrainer.VisualOrder,
                        Description = itemTrainer.Description,
                        TrainerGroup = itemTrainer.TrainerGroup,
                        FullName = itemTrainer.User.FullName,
                        PhotoUrl = Convert.ToBase64String(itemTrainer.User.Photo)
                    };
                });
                trainerListViewModels.Add(new TrainerPageViewModel() { TrainerGroup = item, Trainers = trainerViewModels });
            };

            return View("Trainers", trainerListViewModels);
        }
    }
}
