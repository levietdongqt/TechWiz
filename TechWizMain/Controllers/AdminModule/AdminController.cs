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
    public IActionResult Index()
    {
      return View();
    }

    [Route("Users")]
    public async Task<IActionResult> GetUsers()
    {
      var users = await _adminService.GetAllAsync();
      return View(users);
    }

  }

}