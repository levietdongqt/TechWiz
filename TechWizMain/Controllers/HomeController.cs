using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using TechWizMain.Areas.Identity.Data;
using TechWizMain.Models;
using TechWizMain.Services.FeedbackService;
using TechWizMain.Services.ProductsService;
using Microsoft.AspNetCore.Identity.UI.Services;
using System.Security.Cryptography;
using X.PagedList;
namespace TechWizMain.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IFeedbackService _feedbackService;
        private readonly SignInManager<UserManager> _signInManager;
        private readonly UserManager<UserManager> _userManager;
        private readonly IProductService _productService;
        private readonly TechWizContext _context;
		private readonly IEmailSender _emailSender;

        private readonly string SubjectEmail;
        private readonly string BodyEmail;
		public HomeController(ILogger<HomeController> logger, IFeedbackService feedbackService, SignInManager<UserManager> signInManager, UserManager<UserManager> userManager,IProductService productService,TechWizContext context, IEmailSender emailSender)
        {
            _logger = logger;
            _feedbackService = feedbackService;
            _signInManager = signInManager;
            _userManager = userManager;
            _productService = productService;
            _context = context;
            _emailSender = emailSender;
            SubjectEmail = "Thank You for Your Feedback,";
			BodyEmail = "<p>I hope this email finds you well. I wanted to take a moment to express my sincere appreciation for the feedback you provided. Your insights and thoughts are incredibly valuable to me, and I'm grateful for your time and effort in sharing your perspective.</p>\r\n    <p>Your feedback will help me improve and grow, and I truly value your honesty. Please know that your input matters to me, and I'm committed to using it constructively.</p>\r\n    <p>Once again, thank you for taking the time to provide your feedback. I'm looking forward to implementing the suggested improvements and working towards delivering a better experience.</p>\r\n    <p>Best regards";
		}
        [HttpGet]

        public async Task<IActionResult> Index()
        {
            // Lấy danh sách sản phẩm mới nhất
            var newestProducts = await _context.Products
                .Where(p => p.CreatedDate >= DateTime.Now.AddDays(-10) && p.TypeProduct.StartsWith("Plant"))
                .OrderByDescending(p => p.CreatedDate)
                .Take(8)
                .ToListAsync();
            var newestProductsAccessories = await _context.Products
                .Where(p => p.CreatedDate >= DateTime.Now.AddDays(-10) && p.TypeProduct.StartsWith("Accessories"))
                .OrderByDescending(p => p.CreatedDate)
                .Take(8)
                .ToListAsync();

            // Lấy danh sách sản phẩm best seller
            var bestSellerProducts = await _context.Products
                .OrderByDescending(p => p.Discount) // Sắp xếp theo số lượng bán hàng giảm dần
                .Take(8) // Lấy 8 sản phẩm best seller
                .ToListAsync();

            // Truyền cả hai danh sách vào View
            ViewData["NewestProducts"] = newestProducts;
            ViewData["BestSellerProducts"] = bestSellerProducts;
            ViewData["newestAccessories"] = newestProductsAccessories;
            return View();
        }


        public async Task<IActionResult> ProductByCategory (int id, int page, int? minPrice, int? maxPrice, string? orderSort)
        {       
            int pagesize = 3;
            ViewBag.cateID = id;
            ViewBag.MinPrice = minPrice;
            ViewBag.MaxPrice = maxPrice;
            ViewBag.OrderSort = orderSort;

            int pageIndex = 1;


            var result = _context.Categories.
                    Include(p => p.CategoryProducts).
                    ThenInclude(pc => pc.Product).
                    ThenInclude(dc => dc.Discount).
                    FirstOrDefault(t => t.Id == id) ;
            var CategoryProductsList = result.CategoryProducts;

            var productList = new List<Product>();
            var productListFilter = new List<Product>();
            var productListFilterSort = new List<Product>();
            foreach (var item in CategoryProductsList)
            {
                var product = item.Product;
                productList.Add(product);
            }

            productListFilter = productList.Where(p => p.Price >= minPrice && p.Price <= maxPrice).ToList();

            if(minPrice == null && maxPrice == null)
            {
                switch (orderSort)
                {
                    case "price-asc":
                        productListFilterSort = productList.OrderBy(o => o.Price).ToList();
                        break;
                    case "price-desc":
                        productListFilterSort = productList.OrderByDescending(o => o.Price).ToList();
                        break;
                }
                return View(productListFilterSort.ToPagedList(pageIndex, pagesize));
            }
            else
            {
                switch (orderSort)
                {
                    case "price-asc":
                        productListFilterSort = productListFilter.OrderBy(o => o.Price).ToList();
                        break;
                    case "price-desc":
                        productListFilterSort = productListFilter.OrderByDescending(o => o.Price).ToList();
                        break;
                }
                return View(productListFilterSort.ToPagedList(pageIndex, pagesize));
            }          
        }

        public async Task<IActionResult> Search(string searchString)
        {
            
            //int pagesize = 3;
            var list =await _context.Products.
                    Include(dc => dc.Discount).Where(p => p.Name.Contains(searchString)).ToListAsync();
                   
           

            return View(list);
        }

        public IActionResult Details()
        {
            return View();
        }
        public IActionResult Checkout()
        {
            return View();
        }
        public async Task<IActionResult> ShopList()
        {
            var Product = await _context.Products.ToListAsync();
            ViewData["Products"] = Product;
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
        public async Task<IActionResult> Contact(Feedback feedback)
        {
            if (ModelState.IsValid)
            {
                feedback.FeedbackDate = DateTime.Now;
                var result = _feedbackService.InsertFeedback(feedback);
                if (result)
                {
                    await _emailSender.SendEmailAsync(feedback.Email,SubjectEmail, "<p>Dear, " +feedback.Name+"</p>\r\n"+BodyEmail);
                    // Return a JSON response indicating success
                    return Json(new { success = true });
                }
            }
            return Json(new { success = false });
        }
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}