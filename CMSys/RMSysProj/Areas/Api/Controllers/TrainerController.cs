using CMSys.Core.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace RMSysProj.Area.Api.Controllers
{
    [Area("Api")]
    [Authorize]
    [ApiController]
    public class TrainerController: ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        public TrainerController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet("api/trainers/{id}")]
        public IActionResult TrainerById(Guid id)
        {
            var trainer = unitOfWork.TrainerRepository.Find(id);
            if (trainer == null)
            {
                return NotFound();
            }

            var trainerViewModel = new
            {
                trainer.User.FullName,
                Photo = Convert.ToBase64String(trainer.User.Photo),
                trainer.Description
            };
            return new ObjectResult(trainerViewModel);
        }
    }
}
