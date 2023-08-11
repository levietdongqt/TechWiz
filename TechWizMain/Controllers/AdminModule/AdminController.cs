using Microsoft.AspNetCore.Mvc;
using TechWizMain.Areas.Identity.Data;  
using TechWizMain.Repository.UserRepository;
using TechWizMain.Services.AdminService;

namespace TechWizMain.Controllers
{

  [Route("Admin")]
  public class AdminController : Controller
  {

    private readonly IAdminService _adminService;

    public AdminController(IAdminService adminService)
    {

      _adminService = adminService;

    }

    [Route("")]
    public async Task<IActionResult> Index()
    {
      var users = await _adminService.GetAllAsync(true);
      return View(users);
    }

    [Route("Users")]
    public async Task<IActionResult> GetUsers()
    {
      var usersActive = await _adminService.GetAllAsync(true);
	  var usersBanned = await _adminService.GetAllAsync(false);
	  ViewBag.Active = usersActive;
      ViewBag.Banned = usersBanned;
      return View(ViewBag.active);
    }

    [HttpPost]
    [Route("Banned")]
    public async Task<IActionResult> Banned(string Id)
    {
        if(Id != null) 
        {
           await _adminService.BannedUsers(Id);
        }
        return RedirectToAction("GetUsers");
    }

  }

}