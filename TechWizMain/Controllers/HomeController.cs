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
using NuGet.Protocol;
using System.Collections.Generic;
using TechWizMain.Services.ReviewService;
using TechWizMain.Services.CategoriesService;

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
        private readonly IReviewService _reviewService;
        private readonly ICategoryService _categoryService;
        private readonly TechWizContext _context;
        private readonly IEmailSender _emailSender;
        private readonly IHomeService _homeService;
        private readonly string SubjectEmail;

        public HomeController(ILogger<HomeController> logger, IFeedbackService feedbackService,
            SignInManager<UserManager> signInManager, UserManager<UserManager> userManager,
            IProductService productService, TechWizContext context, IEmailSender emailSender, IHomeService homeService, IReviewService reviewService, ICategoryService categoryService)
        {
            _homeService = homeService;
            _logger = logger;
            _feedbackService = feedbackService;
            _signInManager = signInManager;
            _userManager = userManager;
            _productService = productService;
            _context = context;
            _emailSender = emailSender;
            _reviewService = reviewService;
            _categoryService = categoryService;
            SubjectEmail = "Thank You for Your Feedback,";
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            // Lấy danh sách sản phẩm mới nhất
            _context.Discounts.ToList();
            Console.WriteLine("");
            var newestProducts = _homeService.getNewestProducts();
            var newestProductsAccessories = _homeService.getNewestProductsAccessories();
            var bestSellerProducts = _homeService.getBestSellerProducts();
            var CategoryList = _context.Categories.ToListAsync();
            await Task.WhenAll(newestProducts, newestProductsAccessories, bestSellerProducts, CategoryList);

            ViewData["NewestProducts"] = newestProducts.Result;
            ViewData["BestSellerProducts"] = bestSellerProducts.Result;
            ViewData["newestAccessories"] = newestProductsAccessories.Result;
            ViewData["CategoryList"] = CategoryList.Result;
            return View();
        }


        [HttpGet]
        [Authorize(Roles = "customer,admin")]
        public async Task<IActionResult> Account()
        {
            if (_signInManager.IsSignedIn(User))
            {
                var currentUser = await _userManager.GetUserAsync(User);

                // Định dạng ngày tháng theo chuẩn MM/dd/yyyy
                var bill = await _context.Bills.Where(t => t.UserId.Equals(currentUser.Id) && !t.Status.Equals(ProcessBill.Temporary.ToString())).ToListAsync();
                if(bill != null)
                {
                    ViewBag.billList = bill;
                }
                else
                {
                    ViewBag.billList = new List<Bill>();
                }
                return View("Account", currentUser);
            }


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
                    Include(dc => dc.Discount).Where(p => p.Name.Contains(searchString) && p.status).ToListAsync();
            var productListFilterSort = new List<Product>();
            var productListSort = new List<Product>();

            if (minPrice == null && maxPrice == null && orderSort == null)
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
                List<Product> productListFilter = list.Where(p => p.Price >= minPrice && p.Price <= maxPrice).ToList();
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

        public async Task<IActionResult> ProductByCategory(int id, string? orderSort, int? minPrice, int? maxPrice, int? page)
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
                if (product.status == true)
                {
                    productList.Add(product);
                }

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
        [Authorize(Roles = "customer,admin")]
        public async Task<IActionResult> Cart()
        {
            IEnumerable<ProductBill> listCart = null;
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser != null)
            {
                var tempBill = await _context.Bills.Include(t => t.ProductBills).ThenInclude(t => t.Product).ThenInclude(t => t.Discount).FirstOrDefaultAsync(b => b.UserId.Equals(currentUser.Id) && b.Status.Equals(ProcessBill.Temporary.ToString()));
                if (tempBill != null)
                {
                    listCart = tempBill.ProductBills;
                    foreach (var item in listCart)
                    {
                        item.Bill = null;
                        item.Product.ProductBills = null;
                        item.Product.Discount.Products = null;
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
            var list = await _context.Reviews.Include(r => r.Product).Where(m => m.ProductId == id).ToListAsync();
            double number = 0;
            if (list != null)
            {
                foreach (var e in list)
                {
                    number += e.Rating.Value;
                }
            }
            if (product == null)
            {
                return NotFound();
            }
            ViewBag.Reviews = list;
            ViewBag.CountReviews = list.Count();
            if (list.Count() == 0)
            {
                ViewBag.Number = (float) 0;
            }
            else
            {
                var rating = number / list.Count();
                ViewBag.Number = (float)Math.Round(rating,1);
            }

            //Lay danh sach product moi nhat

            return View(product);
        }
        [HttpGet]
        [Authorize(Roles = "customer,admin")]
        public IActionResult Checkout()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "customer,admin")]
        public async Task<IActionResult> CheckoutHandler(string txtName, string txtEmail, string txtAddress, string txtPhone, string txtId, string submitTotal)
        {
            MailBuilder mail = new MailBuilder();
            decimal total = decimal.Parse(submitTotal);
            Bill bill = await _context.Bills.Where(t => t.UserId.Equals(txtId) && t.Status.Equals(ProcessBill.Temporary.ToString())).FirstOrDefaultAsync();
            if (bill != null)
            {
                bill.OrderDate = DateTime.Now;
                bill.Total = total;
                bill.Status = "Pending";
                bill.DeliveryPhone = txtPhone;
                bill.DeliveryAddress = txtAddress;
                _context.Bills.Update(bill);
                await _context.SaveChangesAsync();
            }
            if (txtEmail != null)
            {
                Task.Run(async () =>
                {
                    _emailSender.SendEmailAsync(txtEmail, "Thanks for your Order", mail.BuildMailOrders(txtName, DateTime.Now, total, txtAddress));
                });


            }
            return Redirect("/");

        }
        [Route("addToCart/{id}/{quantity}/{salePrice}")]
        public async Task<IActionResult> AddToCart(int? id, int quantity, Decimal salePrice)
        {
            try
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
                    productBill.Quantity += quantity;
                    productBill.SalePrice = salePrice;
                    _context.ProductBills.Update(productBill);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false });
            }

            return Json(new { success = true });
        }
        [Route("UpdateCart/{id}/{quantity}")]
        public async Task<IActionResult> UpdateCart(int? id, int quantity)
        {
            ProductBill productBill = null;
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser != null)
            {
                productBill = await _context.ProductBills.FirstOrDefaultAsync(t => t.Id == id);
                productBill.Quantity = quantity;
                _context.ProductBills.Update(productBill);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
        [Route("DeleteFromCart/{productBillID}")]
        public async Task<IActionResult> DeleteFromCart(int? productBillID)
        {
            try
            {
                var productBill = await _context.ProductBills.FirstOrDefaultAsync(t => t.Id == productBillID);
                _context.ProductBills.Remove(productBill);
                await _context.SaveChangesAsync();

            }
            catch
            {
                return Json(new { success = false });
            }

            return Json(new { success = true });
        }
        public async Task<IActionResult> ShopList(string? search, int? orderby)
        {
            List<Product> products = new List<Product>();
            int firstNumber = 0;
            int secondNumber = 0;
            if (search != null)
            {
                string[] part = search.Split(new char[] { ' ', '-', '$' }, StringSplitOptions.RemoveEmptyEntries);
                firstNumber = int.Parse(part[0]);
                secondNumber = int.Parse(part[1]);
                products = _context.Products.Include(t => t.Discount).Where(m => m.Price >= firstNumber && m.Price <= secondNumber && m.status == true).ToList().OrderByDescending(m => m.Price).ToList();
            }
            else
            {
                products = _context.Products.Include(t => t.Discount).Include(m => m.Reviews).ToListAsync().Result.Where(m => m.status == true).ToList().OrderByDescending(m => m.CreatedDate).ToList();

            }
            ViewBag.Categories = await _categoryService.GetAllCate();
            ViewData["Products"] = products;
            return View();
        }

        public IActionResult Privacy()
        {
            MailBuilder mailBuilder = new MailBuilder();
            decimal a = 10000;
            _emailSender.SendEmailAsync("huy.tran9510@gmail.com", "ABC", mailBuilder.BuildMailOrders("HuyTran", new DateTime(), a, "abc"));
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

        [HttpPost]
        [Route("InsertReview")]
        [Authorize(Roles = "customer,admin")]
        public IActionResult InsertReview(List<int>? vehicle1, string? content, int? ProductId, string UserId)
        {
            int rating = vehicle1.Count();
            Review review = new Review();
            review.Rating = rating;
            review.Content = content;
            review.ProductId = ProductId;
            review.UserId = UserId;
            review.ReviewDate = DateTime.Now;
            _context.Reviews.Add(review);
            _context.SaveChanges();
            return Redirect("/Home/Details/" + ProductId);
        }
        [HttpPost]
        [Route("CountCart")]
        [Authorize(Roles = "customer,admin")]
        public async Task<IActionResult> CountCart()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return Json(new { count = 0 });
            }
            int count = await _homeService.CountCart(currentUser);
            return Json(new { count = count });
        }
        [HttpPost]
        [Route("changeBill/{billId}")]
        [Authorize(Roles = "customer,admin")]
        public async Task<IActionResult> changeBill(int billId)
        {
           var bill = await _context.Bills.Where(t => t.Id  == billId).FirstOrDefaultAsync();
            bill.Status = ProcessBill.Cancel.ToString();
            _context.Update(bill);
            _context.SaveChanges();
            return Json(new { success = true });
        }
    }
}
