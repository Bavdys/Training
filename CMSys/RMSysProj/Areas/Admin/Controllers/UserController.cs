using CMSys.Common.Paging;
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
    public class UserController : Controller
    {
        private const int PER_PAGE = 10;
        private readonly IUnitOfWork unitOfWork;
        public UserController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Route("admin/users")]
        [Route("admin/users/page/{page}/{search?}")]
        public IActionResult Users(int page=1, string search = null)
        {
            PageInfo pageInfo = new(page, PER_PAGE);
            var pagedListUser = unitOfWork.UserRepository.GetPagedList(pageInfo,search);
            if (pagedListUser == null)
            {
                return NotFound();
            }

            PageListViewModel pageListViewModel = new()
            {
                Total = pagedListUser.Total,
                Page = pagedListUser.Page,
                PerPage = pagedListUser.PerPage,
                TotalPages = pagedListUser.TotalPages,
                CanNext = pagedListUser.CanNext,
                CanPrevious = pagedListUser.CanPrevious,
                From = pagedListUser.From,
                To = pagedListUser.To
            };
            IEnumerable<UserViewModel> userViewModels = pagedListUser.Items.Select(item =>
            {
                return new UserViewModel()
                {
                    Id = item.Id,
                    FullName = item.FullName,
                    Department = item.Department,
                    Position = item.Position,
                    Photo = Convert.ToBase64String(item.Photo),
                    Office = item.Location,
                    Active = item.EndDate < DateTime.Now ? "No" : "Yes"
                };
            });
            UserPageViewModel userPageViewModel = new()
            {
                PageListViewModel = pageListViewModel,
                Users = userViewModels
            };

            return View("Users", userPageViewModel);
        }

        [HttpGet]
        [Route("admin/users/{id}")]
        public IActionResult UserById(Guid id)
        {
            var user = unitOfWork.UserRepository.Find(id);
            if (user == null)
            {
                return NotFound();
            }
          
            UserDetailsViewModel userDetailsViewModel = new()
            {
                Id = user.Id,
                FullName = user.FullName,
                Position = user.Position,
                Department = user.Department,
                Office = user.Location,
                Photo = Convert.ToBase64String(user.Photo),
                StartDate = user.StartDate,
                Roles = user.Roles.Select(item =>
                {
                    return new RoleViewModel()
                    {
                        Id = item.Id,
                        Name = item.Name
                    };
                })
            };

            return View("Details", userDetailsViewModel);
        }
        
        [HttpGet]
        [Route("admin/users/update/{id}")]
        public IActionResult UpdateUser(Guid id)
        {
            UserEditViewModel userEditViewModel = new();
            var user = unitOfWork.UserRepository.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            ViewData["FullName"] = user.FullName;

            return View("UpdateUser", userEditViewModel);
        }

        [HttpPost]
        [Route("admin/users/update")]
        public IActionResult ChangePassword(UserEditViewModel userEditViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("UpdateUser", userEditViewModel);
            }

            var user = unitOfWork.UserRepository.Find(userEditViewModel.Id);
            if (user == null)
            {
                return NotFound();
            }

            user.ChangePassword(userEditViewModel.Password);

            unitOfWork.Commit();

            return RedirectToAction("Users");
        }
    }
}
