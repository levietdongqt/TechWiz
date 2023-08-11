using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
        public ProductsController(TechWizContext context, IProductService productService)
        {
            _context = context;
            _productService = productService;

        }

        // GET: Products
        [Route("Products")]
        public async Task<IActionResult> Index()
        {
            var productActive = await _productService.GetProductListByStatus(true);
			var productDeleted = await _productService.GetProductListByStatus(false);
			ViewBag.Active = productActive;
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
        public async Task<IActionResult> Create( Product product, IFormFile? formFile   )
        {
            if (ModelState.IsValid)
            {
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
            }
            ViewData["DiscountId"] = new SelectList(_context.Discounts, "Id", "Id", product.DiscountId);
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

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["DiscountId"] = new SelectList(_context.Discounts, "Id", "Id", product.DiscountId);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Route("Products/Edit")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Price,BasePrice,ImageUrl,TypeProduct,DiscountId,InventoryQuantity,status")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["DiscountId"] = new SelectList(_context.Discounts, "Id", "Id", product.DiscountId);
            return View(product);
        }

        [Route("Products/Delete/{id}")]
        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
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

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Products == null)
            {
                return Problem("Entity set 'TechWizContext.Products'  is null.");
            }
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
          return (_context.Products?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
