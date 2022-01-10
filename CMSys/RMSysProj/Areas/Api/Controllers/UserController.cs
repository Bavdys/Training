using CMSys.Core.Entities.Membership;
using CMSys.Core.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RMSysProj.Areas.Admin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RMSysProj.Areas.Api.Controllers
{
    [Area("Api")]
    [Authorize(Roles = "Admin")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        public UserController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet("api/admin/users/update/{id}")]
        public ActionResult<Role> Roles(Guid id)
        {
            var userRoles = unitOfWork.UserRepository.Find(id).Roles;
            if(userRoles == null)
            {
                return NotFound();
            }

            IEnumerable<RoleViewModel> userRolesViewModel = unitOfWork.UserRepository.Find(id).Roles.Select(item => {
                return new RoleViewModel()
                {
                    Id = item.Id,
                    Name = item.Name
                };
            });
            IEnumerable<RoleViewModel> allRolesViewModel = unitOfWork.RoleRepository.Filter(item => !userRoles.Contains(item)).Select(item=> {
                return new RoleViewModel()
                {
                    Id = item.Id,
                    Name = item.Name
                };
            });
            RolePageViewModel rolePageViewModel = new()
            {
                UserId = id,
                AllRoles = allRolesViewModel,
                UserRoles = userRolesViewModel
            };

            return Ok(rolePageViewModel);
        }

        [HttpPost("api/admin/users/update")]
        public IActionResult AddRole([FromBody] RoleEditViewModel roleEditViewModel)
        {
            var role = unitOfWork.RoleRepository.Find(roleEditViewModel.IdRole);
            if (role == null)
            {
                return NotFound();
            }
            var user = unitOfWork.UserRepository.Find(roleEditViewModel.IdUser);
            if (user == null)
            {
                return NotFound();
            }

            user.Roles.Add(role);

            unitOfWork.Commit();

            return Ok(roleEditViewModel);
        }

        [HttpDelete("api/admin/users/update/{id}")]
        public IActionResult DeleteRole(Guid id, Guid idRole)
        {
            var role = unitOfWork.RoleRepository.Find(idRole);
            if (role == null)
            {
                return NotFound();
            }
            var user = unitOfWork.UserRepository.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            user.Roles.Remove(role);

            unitOfWork.Commit();

            return Ok();
        }
    }
}
