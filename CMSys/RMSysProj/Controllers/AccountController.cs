using CMSys.Core.Entities.Membership;
using CMSys.Core.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using RMSysProj.ViewModels;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RMSysProj.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        public AccountController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Route("login")]
        public IActionResult Login()
        {
            return View("Login");
        }

        [HttpPost]
        [Route("login")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                User user = unitOfWork.UserRepository.FindByEmail(model.Email);
                if (user != null && user.VerifyPassword(model.Password))
                {
                    await Authenticate(model.Email);

                    return RedirectToAction("Courses","Course");
                }
                ModelState.AddModelError("", "Incorrect login and(or) password");
            }
            return View("Login",model);
        }

        [HttpGet]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

        private async Task Authenticate(string userName)
        {
            var claims = unitOfWork.UserRepository.FindByEmail(userName).GetClaims();
            ClaimsIdentity id = new ClaimsIdentity(claims, "Cookie");
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
    }
}
