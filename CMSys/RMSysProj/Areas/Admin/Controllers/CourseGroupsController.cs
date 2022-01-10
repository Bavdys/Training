using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RMSysProj.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CourseGroupsController : Controller
    {
        [HttpGet]
        [Route("admin/coursegroups")]
        public IActionResult CourseGroups()
        {
            return View("CourseGroups");
        }
    }
}
