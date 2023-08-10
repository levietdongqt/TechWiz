using Microsoft.AspNetCore.Mvc;
using TechWizMain.Areas.Identity.Data;
using TechWizMain.Repository.UserRepository;
using TechWizMain.Services.AdminService;

namespace TechWizMain.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;
        public AdminController(IAdminService adminService) 
        {
            this._adminService = adminService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var list = await _adminService.GetAllAsync();
            
            return View(list);
        }

        [HttpPost]
        public IActionResult OnPost(string UserName)
        {
            return Content("bac" + UserName);
        }
    }
}
