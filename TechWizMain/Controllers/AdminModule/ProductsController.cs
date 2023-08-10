using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TechWizMain.Models;
using TechWizMain.Services.ProductsService;

namespace TechWizMain.Controllers.AdminModule
{
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
        public async Task<IActionResult> Index()
        {
            var techWizContext = _context.Discount.Include(p => p.Discount);
            return View(await techWizContext.ToListAsync());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Discount == null)
            {
                return NotFound();
            }

            var product = await _context.Discount
                .Include(p => p.Discount)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            ViewData["DiscountId"] = new SelectList(_context.Discounts, "Id", "Id");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Create( Product product, IFormFile? formFile)
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
            ViewData["DiscountId"] = new SelectList(_context.Discounts, "Id", "Id", product.DiscountId);
            ViewData["errMess"] = "Product is invalid";
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Discount == null)
            {
                return NotFound();
            }

            var product = await _context.Discount.FindAsync(id);
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

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Discount == null)
            {
                return NotFound();
            }

            var product = await _context.Discount
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
            if (_context.Discount == null)
            {
                return Problem("Entity set 'TechWizContext.Products'  is null.");
            }
            var product = await _context.Discount.FindAsync(id);
            if (product != null)
            {
                _context.Discount.Remove(product);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
          return (_context.Discount?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
