using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RMSysProj.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class TrainerGroupsController : Controller
    {
        [HttpGet]
        [Route("admin/trainergroups")]
        public IActionResult TrainerGroups()
        {
            return View("TrainerGroups");
        }
    }
}
