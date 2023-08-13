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
using System.Collections.Generic;

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
            ViewBag.Cart = cart;
            
            // Truyền cả hai danh sách vào View
            ViewData["NewestProducts"] = newestProducts;
            ViewData["BestSellerProducts"] = bestSellerProducts;
            ViewData["newestAccessories"] = newestProductsAccessories;


            var CategoryList = await _context.Categories.ToListAsync();
            ViewData["CategoryList"] = CategoryList;
            return View();
        }        

            public async Task<IActionResult> Search(string searchString, int? page, string? orderSort, int? minPrice, int? maxPrice)
        {

            int pagesize = 3;
            int pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            ViewBag.searchString = searchString;
            ViewBag.MinPrice = minPrice;
            ViewBag.MaxPrice = maxPrice;
            ViewBag.OrderSort = orderSort;
            var list = await _context.Products.
                    Include(dc => dc.Discount).Where(p => p.Name.Contains(searchString)).ToListAsync();
            var productListFilterSort = new List<Product>();
            var productListSort = new List<Product>();

            if(minPrice == null&&maxPrice == null&&orderSort==null)
            {
                return View(list.ToPagedList(pageIndex, pagesize));
            }



            if (minPrice == null || maxPrice == null)
            {
                switch (orderSort)
                {
                    case "price-asc":
                        productListSort = list.OrderBy(o => o.Price).ToList();
                        break;
                    case "price-desc":
                        productListSort = list.OrderByDescending(o => o.Price).ToList();
                        break;
                }
                return View(productListSort.ToPagedList(pageIndex, pagesize));
            }
            else
            {
                minPrice = 0;
                List<Product>  productListFilter = list.Where(p => p.Price >= minPrice && p.Price <= maxPrice).ToList();
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

        public async Task<IActionResult> ProductByCategory(int id, string? orderSort, int? minPrice,   int? maxPrice, int? page)
        {
            int pagesize = 3;
            int pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            ViewBag.cateID = id;
            ViewBag.MinPrice = minPrice;
            ViewBag.MaxPrice = maxPrice;
            ViewBag.OrderSort = orderSort;                 


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
        public async Task<IActionResult> Cart()
        {
            var cart = await _context.Products.Where(p => p.CreatedDate >= DateTime.Now.AddDays(-10) && p.TypeProduct.StartsWith("Accessories"))
                .OrderByDescending(p => p.CreatedDate)
                .Take(8)
                .ToListAsync();
            ViewBag.Cart = cart;
            return View();

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
            productBill = await _context.ProductBills.FirstOrDefaultAsync(t => t.BillId == bill.Id && t.ProductId==id);
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
					await _emailSender.SendEmailAsync(feedback.Email,SubjectEmail,mailBuilder.BuilderMailContact(feedback.Name));
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