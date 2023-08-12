﻿using Microsoft.AspNetCore.Authorization;
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
using X.PagedList;

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

        public HomeController(ILogger<HomeController> logger, IFeedbackService feedbackService,
            SignInManager<UserManager> signInManager, UserManager<UserManager> userManager,
            IProductService productService, TechWizContext context, IEmailSender emailSender, IHomeService homeService)
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
                .Join(_context.Discounts, product => product.DiscountId, discount => discount.Id, (product, discount) => new ProductResult
                {
                    Product = product,
                    Discount = discount
                })
                .ToListAsync();

            var newestProductsAccessories = await _context.Products
                .Where(p => p.CreatedDate >= DateTime.Now.AddDays(-10) && p.TypeProduct.StartsWith("Accessories"))
                .OrderByDescending(p => p.CreatedDate)
                .Take(8)
                .Join(_context.Discounts, product => product.DiscountId, discount => discount.Id, (product, discount) => new ProductResult
                {
                    Product = product,
                    Discount = discount
                })
                .ToListAsync();
            // Lấy danh sách sản phẩm best seller
            var bestSellerProducts = await _context.ProductBills
                .GroupBy(pb => pb.ProductId)
                .Select(g => new
                {
                    ProductId = g.Key,
                    TotalQuantitySold = g.Sum(pb => pb.Quantity)
                })
                .OrderByDescending(result => result.TotalQuantitySold)
                .Take(8)
                .Join(_context.Products, result => result.ProductId, product => product.Id, (result, product) => new
                {
                    Product = product,
                    TotalQuantitySold = result.TotalQuantitySold
                })
                .Join(_context.Discounts, productResult => productResult.Product.DiscountId, discount => discount.Id, (productResult, discount) => new
                {
                    Product = productResult.Product,
                    TotalQuantitySold = productResult.TotalQuantitySold,
                    Discount = discount
                })
                .ToListAsync();
            

            // Truyền cả hai danh sách vào View
            ViewData["NewestProducts"] = newestProducts;
            ViewData["BestSellerProducts"] = bestSellerProducts.Select(result => new ProductResult
            {
                Product = result.Product,
                TotalQuantitySold = result.TotalQuantitySold,
                Discount = result.Discount
            }).ToList();
            ViewData["newestAccessories"] = newestProductsAccessories;


            var CategoryList = await _context.Categories.ToListAsync();
            ViewData["CategoryList"] = CategoryList;
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> Account()
        {
            if (_signInManager.IsSignedIn(User))
            {
                var currentUser = await _userManager.GetUserAsync(User);
                var accountModel = new UserManager()
                {
                    UserName = currentUser.UserName,
                    FullName = currentUser.FullName,
                    Address = currentUser.Address,
                    Email = currentUser.Email,
                    PhoneNumber = currentUser.PhoneNumber,
                    DateOfBirth = currentUser.DateOfBirth,
                };
                // Định dạng ngày tháng theo chuẩn MM/dd/yyyy

                return View("Account", accountModel);
            }


            return View();
        }




        public async Task<IActionResult> Search(string searchString)
        {

            //int pagesize = 3;
            var list = await _context.Products.Include(dc => dc.Discount).Where(p => p.Name.Contains(searchString))
                .ToListAsync();



            return View(list);
        }

        public async Task<IActionResult> ProductByCategory1(int id, int page)
        {
            int pagesize = 3;
            ViewBag.cateID = id;
            int pageIndex = 1;
            var result = _context.Categories.Include(p => p.CategoryProducts).ThenInclude(pc => pc.Product)
                .ThenInclude(dc => dc.Discount).FirstOrDefault(t => t.Id == id);
            var CategoryProductsList = result.CategoryProducts;

            var productList = new List<Product>();
            var productListFilter = new List<Product>();
            var productListFilterSort = new List<Product>();
            foreach (var item in CategoryProductsList)
            {
                var product = item.Product;
                productList.Add(product);
            }

            return View(productList.ToPagedList(pageIndex, pagesize));
        }

        public async Task<IActionResult> ProductByCategory(int id, int page, string orderSort, int? minPrice,
            int? maxPrice)
        {
            int pagesize = 3;
            ViewBag.cateID = id;
            ViewBag.MinPrice = minPrice;
            ViewBag.MaxPrice = maxPrice;
            ViewBag.OrderSort = orderSort;

            int pageIndex = 1;


            var result = _context.Categories.Include(p => p.CategoryProducts).ThenInclude(pc => pc.Product)
                .ThenInclude(dc => dc.Discount).FirstOrDefault(t => t.Id == id);
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

            if (minPrice == null || maxPrice == null)
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
                    foreach (var item in listCart)
                    {
                        item.Bill = null;
                        item.Product.ProductBills = null;
                    }

                }
            }
            return Json(listCart);

        }

       
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Discount)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
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
