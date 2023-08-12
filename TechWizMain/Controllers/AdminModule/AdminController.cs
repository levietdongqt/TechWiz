using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechWizMain.Areas.Identity.Data;
using TechWizMain.Data;
using TechWizMain.Models;
using TechWizMain.Repository.UserRepository;
using TechWizMain.Services.AdminService;
using TechWizMain.Services.FeedbackService;

namespace TechWizMain.Controllers
{
    [Authorize(Roles = "admin")]
    [Route("Admin")]
  public class AdminController : Controller
  {

    private readonly IAdminService _adminService;
    private readonly IFeedbackService _feedbackService;
    private readonly TechWizContext _context;
    private readonly UserManagerContext _userManagerContext;
    private const int ITEM_PER_PAGE = 5;


    [BindProperty(SupportsGet = true,Name = "p")]
    public int currentPage { get; set; }
	public int countPages { get; set; }



    
    public AdminController(IAdminService adminService, IFeedbackService feedbackService, TechWizContext context, UserManagerContext userManagerContext)
    {

      _adminService = adminService;
      _feedbackService = feedbackService;
       _context = context;
        _userManagerContext = userManagerContext;

    }

    [Route("")]
    public async Task<IActionResult> Index()
    {
      return View();
    }

    [Route("Users")]
    public async Task<IActionResult> GetUsers(int? p, string? UserName)
    {
			if (p == null)
			{
				p = 1;

			}
			currentPage = p.Value;
			int totalPages = await _userManagerContext.Users.CountAsync();
			countPages = (int)Math.Ceiling((double)totalPages / ITEM_PER_PAGE);

			if (currentPage < 1)
			{
				currentPage = 1;
			}
			if (currentPage > totalPages)
			{
				currentPage = totalPages;
			}
			ViewBag.currentPage = currentPage;
			ViewBag.countPages = countPages;

			if (UserName == null)
            {
		    var list = await _adminService.GetAllAsync(true);
			ViewBag.Active = list.Skip((currentPage - 1) * 5).Take(ITEM_PER_PAGE).ToList();
			}
            else
            {
			var list = await _adminService.GetByUserName(UserName);
			ViewBag.Active = list.Skip((currentPage - 1) * 5).Take(ITEM_PER_PAGE).ToList();
			}
     
	  var usersBanned = await _adminService.GetAllAsync(false);
      ViewBag.Banned = usersBanned;
      return View();
    }




		[Route("FeedBacks")]
		public async Task<IActionResult> FeedBacks(int? p)
        {
            if(p == null)
            {
                p = 1;
                
            }
			currentPage = p.Value;
			int totalPages = await _context.Feedbacks.CountAsync();
            countPages = (int) Math.Ceiling((double)totalPages / ITEM_PER_PAGE);

            if(currentPage < 1)
            {
                currentPage = 1;
            }
            if(currentPage > totalPages)
            {
                currentPage = totalPages;
            }
            ViewBag.currentPage = currentPage;
            ViewBag.countPages = countPages;

            var list = await _feedbackService.GetAllAsync();
            var list2 = list.Skip((currentPage-1) * 5).Take(ITEM_PER_PAGE).ToList();
            ViewBag.List = list2;
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

        [HttpGet]
		[Route("Details")]
		public async Task<IActionResult> Details(int? Id)
        {
            if (Id == null || _context.Feedbacks == null)
            {
                return NotFound();
            }

            var feedback = await _context.Feedbacks
                .FirstOrDefaultAsync(m => m.Id == Id);
            if (feedback == null)
            {
                return NotFound();
            }

            return View(feedback);
        }
        [HttpGet]
        [Route("ProcessBill")]
        public async Task<IActionResult> ProcessBillHandler(int? p)
        {
			if (p == null)
			{
				p = 1;
			}
			currentPage = p.Value;
			var listSpending = await _adminService.GetAllBillAsync(ProcessBill.Pending.ToString());
			var listCancel = await _adminService.GetAllBillAsync(ProcessBill.Cancel.ToString());
			var listSuccess = await _adminService.GetAllBillAsync(ProcessBill.Success.ToString());

            int totalPages = listSpending.Count();
			countPages = (int)Math.Ceiling((double)totalPages / ITEM_PER_PAGE);

			if (currentPage < 1)
			{
				currentPage = 1;
			}
			if (currentPage > totalPages)
			{
				currentPage = totalPages;
			}
			ViewBag.currentPage = currentPage;
			ViewBag.countPages = countPages;

            
            ViewBag.ListSpending = listSpending;
            ViewBag.ListCancel = listCancel;
            ViewBag.ListSuccess = listSuccess;
			ViewBag.Cancel = ProcessBill.Cancel.ToString();
			ViewBag.Success = ProcessBill.Success.ToString();
			return View();

		}
        [HttpGet]
        [Route("ProccessStatus")]
        public async Task<IActionResult> ProccessStatus(int Id, string process)
        {
            await _adminService.ChangedStatusBill(Id, process);

			return RedirectToAction("ProcessBill");
		}

  }

}