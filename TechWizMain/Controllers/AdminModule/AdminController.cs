using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechWizMain.Areas.Identity.Data;  
using TechWizMain.Repository.UserRepository;
using TechWizMain.Services.AdminService;

namespace TechWizMain.Controllers
{
	//[Authorize(Roles ="admin")]
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
      return View();
    }

    [Route("Users")]
    public async Task<IActionResult> GetUsers(string? UserName)
    {
        
	    if (UserName == null)
       {
		    ViewBag.Active = await _adminService.GetAllAsync(true);
       }
       else
       {
			ViewBag.Active = await _adminService.GetByUserName(UserName);
	    }
     
	  var usersBanned = await _adminService.GetAllAsync(false);
      ViewBag.Banned = usersBanned;
      return View();
    }
		[Route("FeedBacks")]
		public IActionResult FeedBacks()
        {
            return View();
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