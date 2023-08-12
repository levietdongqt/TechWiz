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
using TechWizMain.Services.HomeService;
using TechWizMain.Services;
namespace TechWizMain.Controllers
{

    public class HomeController : Controller
    {
        private static List<ProductBill> _billList;
        private readonly ILogger<HomeController> _logger;
        private readonly IFeedbackService _feedbackService;
        private readonly SignInManager<UserManager> _signInManager;
        private readonly UserManager<UserManager> _userManager;
        private readonly IProductService _productService;
        private readonly TechWizContext _context;
        private readonly IEmailSender _emailSender;
        private readonly IHomeService _homeService;
        private readonly string SubjectEmail;
        public HomeController(ILogger<HomeController> logger, IFeedbackService feedbackService, SignInManager<UserManager> signInManager, UserManager<UserManager> userManager, IProductService productService, TechWizContext context, IEmailSender emailSender, IHomeService homeService)
        {
            _homeService = homeService;
            _logger = logger;
            _feedbackService = feedbackService;
            _signInManager = signInManager;
            _userManager = userManager;
            _productService = productService;
            _context = context;
            _emailSender = emailSender;
            SubjectEmail = "Thank You for Your Feedback,";
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

            var cart = await _context.Products.Where(p => p.CreatedDate >= DateTime.Now.AddDays(-10) && p.TypeProduct.StartsWith("Accessories"))
                .OrderByDescending(p => p.CreatedDate)
                .Take(8)
                .ToListAsync();

            // Truyền cả hai danh sách vào View
            ViewData["NewestProducts"] = newestProducts;
            ViewData["BestSellerProducts"] = bestSellerProducts;
            ViewData["newestAccessories"] = newestProductsAccessories;
            return View();
        }

        [Route("showCart")]
        public async Task<IActionResult> Cart()
        {
            IEnumerable<ProductBill> listCart = null;
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser != null)
            {
                var tempBill = await _context.Bills.Include(t => t.ProductBills).ThenInclude(t => t.Product).FirstOrDefaultAsync(b => b.UserId.Equals(currentUser.Id) && b.Status.Equals(ProcessBill.Temporary.ToString()));
                if (tempBill != null)
                {
                    listCart = tempBill.ProductBills;
                    foreach(var item in listCart)
                    {
                        item.Bill = null;
                        item.Product.ProductBills = null;
                    }
                    
                }
            }
            return Json(listCart);
        }

        public IActionResult Details()
        {
            return View();
        }
        public IActionResult Checkout()
        {

            return View();
        }
        [Route("addToCart/{id}/{quantity}/{salePrice}")]
        public async Task<IActionResult> AddToCart(int? id, int quantity, Decimal salePrice)
        {
            ProductBill productBill = null;
            var currentUser = await _userManager.GetUserAsync(User);
            var bill = await _homeService.getBills(currentUser);
            productBill = await _context.ProductBills.FirstOrDefaultAsync(t => t.BillId == bill.Id && t.ProductId == id);
            if (productBill == null)
            {
                productBill = new ProductBill();
                productBill.BillId = bill.Id;
                productBill.ProductId = id;
                productBill.Quantity = quantity;
                productBill.SalePrice = salePrice;
                await _context.ProductBills.AddAsync(productBill);
                await _context.SaveChangesAsync();
            }
            else
            {
                productBill.Quantity = quantity;
                productBill.SalePrice = salePrice;
                _context.ProductBills.Update(productBill);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
        [Route("UpdateCart/{id}/{quantity}/{salePrice}")]
        public async Task<IActionResult> UpdateCart(int? id, int quantity, Decimal salePrice)
        {
            ProductBill productBill = null;
            var currentUser = await _userManager.GetUserAsync(User);
            var bill = await _context.Bills.FirstOrDefaultAsync(t => t.UserId.Equals(currentUser.Id) && t.Status.Equals(ProcessBill.Temporary.ToString()));
            productBill = await _context.ProductBills.FirstOrDefaultAsync(t => t.BillId == bill.Id);
            productBill.Quantity = quantity;
            productBill.SalePrice = salePrice;
            _context.ProductBills.Update(productBill);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> ShopList()
        {
            var Product = await _context.Products.ToListAsync();
            ViewData["Products"] = Product;
            return View();
        }

        public IActionResult Privacy()
        {
            MailBuilder mailBuilder = new MailBuilder();
            decimal a = 10000;
            _emailSender.SendEmailAsync("huy.tran9510@gmail.com", "ABC", mailBuilder.BuildMailOrders("HuyTran", new DateTime(), a, "abc"));
            return View();
        }
        public IActionResult AddToCart()
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
                    MailBuilder mailBuilder = new MailBuilder();
                    await _emailSender.SendEmailAsync(feedback.Email, SubjectEmail, mailBuilder.BuilderMailContact(feedback.Name));
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