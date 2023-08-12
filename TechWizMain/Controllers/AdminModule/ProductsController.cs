using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TechWizMain.Models;
using TechWizMain.Services.ProductsService;

namespace TechWizMain.Controllers.AdminModule
{
    [Authorize(Roles ="admin")]
    [Route("Admin")]
    public class ProductsController : Controller
    {
        private readonly TechWizContext _context;
        private readonly IProductService _productService;
        [TempData]
        public string StatusMessage { get; set; }

        private const int ITEM_PER_PAGE = 5;


        [BindProperty(SupportsGet = true, Name = "p")]
        public int currentPage { get; set; }
        public int countPages { get; set; }
        public ProductsController(TechWizContext context, IProductService productService)
        {
            _context = context;
            _productService = productService;

        }

        // GET: Products
        [Route("Products")]
        public async Task<IActionResult> Index(int? p)
        {
            if (p == null)
            {
                p = 1;

            }
            currentPage = p.Value;
            int totalPages = await _context.Products.CountAsync();
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

            var productActive = await _productService.GetProductListByStatus(true);
            

            var productDeleted = await _productService.GetProductListByStatus(false);
            var listActive = productActive.Skip((currentPage - 1) * 5).Take(ITEM_PER_PAGE).ToList();
            ViewBag.Active = listActive;
			ViewBag.Deleted = productDeleted;
			return View(ViewBag.productActive);
		}

        // GET: Products/Details/5
        [Route("Products/Details")]
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

        [Route("Products/create")]
        public IActionResult Create()
        {
            ViewData["DiscountId"] = new SelectList(_context.Discounts, "Name", "Name");
            ViewData["TypeProduct"] = new SelectList(_productService.getTypeProduct(), "Value", "Text");
            return View();
        }
        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Route("Products/create")]
        public async Task<IActionResult> Create( Product product, IFormFile? formFile ,string DiscountName)
        {
            if (ModelState.IsValid)
            {
                var discount = _context.Discounts.Where(t => t.Name.Equals(DiscountName)).First();
                product.DiscountId = discount.Id;
                var result = _productService.AddProduct( product,formFile);
                if (result)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewData["errMess"] = "Product is invalid";
                    return View(product);
                }
            }
            else
            {
                foreach (var key in ModelState.Keys)
                {
                    var errors = ModelState[key].Errors;
                    foreach (var error in errors)
                    {
                        var errorMessage = error.ErrorMessage;
                        // Xử lý thông báo lỗi
                    }
                }
            }
            ViewData["DiscountId"] = new SelectList(_context.Discounts, "Name", "Name");
            ViewData["TypeProduct"] = new SelectList(_productService.getTypeProduct(), "Value", "Text");
            ViewData["errMess"] = "Product is invalid";
            return View(product);
        }

        // GET: Products/Edit/5
        [Route("Products/Edit")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products.Include(t => t.Discount).Where(p => p.Id == id).SingleOrDefaultAsync();
            if (product == null)
            {
                return NotFound();
            }
            ViewData["DiscountId"] = new SelectList(_context.Discounts, "Name", "Name");
            ViewData["TypeProduct"] = new SelectList(_productService.getTypeProduct(), "Value", "Text");
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Route("Products/Edit")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, Product product, IFormFile? formFile, string DiscountName)
        {
            if (id != product.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                var discount = _context.Discounts.Where(t => t.Name.Equals(DiscountName)).First();
                product.DiscountId = discount.Id;
                var result = _productService.UpdateProduct(product, formFile);
                return RedirectToAction(nameof(Index));
            }
            ViewData["TypeProduct"] = new SelectList(_productService.getTypeProduct(), "Value", "Text");
            ViewData["DiscountId"] = new SelectList(_context.Discounts, "Name", "Name");
            return View(product);
        }

        [Route("Products/Delete")]
        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }
            var product = await _context.Products.FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
        // POST: Products/Delete/5
        [Route("Discounts/Delete")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }
            _productService.changeStatus(id, false);
            return RedirectToAction("Index");
        }


        [HttpGet]
        [Route("Products/Active")]
        public async Task<IActionResult> Active(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }
            _productService.changeStatus(id, true);
            return RedirectToAction("Index");
        }

        private bool ProductExists(int id)
        {
          return (_context.Products?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
