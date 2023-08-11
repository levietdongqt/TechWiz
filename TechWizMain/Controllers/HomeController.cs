using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Diagnostics;
using TechWizMain.Areas.Identity.Data;
using TechWizMain.Models;
using TechWizMain.Services.FeedbackService;

namespace TechWizMain.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IFeedbackService _feedbackService;
        private readonly SignInManager<UserManager> _signInManager;
        private readonly UserManager<UserManager> _userManager;
        public HomeController(ILogger<HomeController> logger, IFeedbackService feedbackService, SignInManager<UserManager> signInManager, UserManager<UserManager> userManager)
        {
            _logger = logger;
            _feedbackService = feedbackService;
            _signInManager = signInManager;
            _userManager = userManager;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ShopList()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Contact()
        {
            if (_signInManager.IsSignedIn(User))
            {
                var currentUser = _userManager.GetUserAsync(User).Result;
                Feedback feedback = new Feedback();
                feedback.UserID = currentUser.Id;
                feedback.Name = currentUser.UserName;
                feedback.Email = currentUser.Email;
                     return View(feedback);
            }
                return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public  IActionResult Contact(Feedback feedback)
        {
            if (ModelState.IsValid)
            {
                feedback.FeedbackDate = DateTime.Now;
                var result = _feedbackService.InsertFeedback(feedback);
                if (result)
                {
                    return Redirect("/");
                }
            }
            return View(feedback); 
        }
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}