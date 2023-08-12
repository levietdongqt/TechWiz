using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using TechWizMain.Models;
using TechWizMain.Services.DiscountService;

namespace TechWizMain.Controllers.AdminModule
{
    [Authorize(Roles ="admin")]
    [Route("Admins")]
    public class DiscountsController : Controller
    {
        private readonly TechWizContext _context;
        private readonly IDiscountService _discountService;
        public DiscountsController(TechWizContext context , IDiscountService discountService)
        {
            _context = context;
            _discountService = discountService; 
        }
        
        // GET: Discounts
        [Route("Discounts")]
        public async Task<IActionResult> Index()
        {
              return _context.Discounts != null ? 
                          View(await _context.Discounts.ToListAsync()) :
                          Problem("Entity set 'TechWizContext.Discounts'  is null.");
        }

        // GET: Discounts/Details/5
        [Route("Discounts/Details")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Discounts == null)
            {
                return NotFound();
            }

            var discount = await _context.Discounts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (discount == null)
            {
                return NotFound();
            }

            return View(discount);
        }

        // GET: Discounts/Create
        [Route("Discounts/Create")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Discounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Route("Discounts/Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Percent,DateBegin,DateEnd")] Discount discount)
        {
            if (ModelState.IsValid)
            {
                var result = _discountService.AddDiscount(discount);
                if (result)
                {
                    return Redirect("/Admins/Discounts");
                }
            }
            return View(discount);
        }
        
        // GET: Discounts/Edit/5
        [Route("Discounts/Edit")]

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Discounts == null)
            {
                return NotFound();
            }

            var discount = await _context.Discounts.FindAsync(id);
            if (discount == null)
            {
                return NotFound();
            }
            return View(discount);
        }

        // POST: Discounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Route("Discounts/Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Percent,DateBegin,DateEnd")] Discount discount)
        {
            if (id != discount.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                   _discountService.UpdateDiscount(discount);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DiscountExists(discount.Id))
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
            return View(discount);
        }
        
        // GET: Discounts/Delete/5
        [Route("Discounts/Delete")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Discounts == null)
            {
                return NotFound();
            }

            var discount = await _context.Discounts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (discount == null)
            {
                return NotFound();
            }

            return View(discount);
        }

        // POST: Discounts/Delete/5
        [Route("Discounts/Delete")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Discounts == null)
            {
                return Problem("Entity set 'TechWizContext.Discounts'  is null.");
            }
            else
            {
                _discountService.DeleteDiscount(id);
            }
            return RedirectToAction(nameof(Index));
        }

        public bool DiscountExists(int id)
        {
            return (_context.Discounts?.Any(e => e.Id == id)).GetValueOrDefault();
        }


    }
}
