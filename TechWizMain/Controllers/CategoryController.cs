using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Printing;
using TechWizMain.Models;
using TechWizMain.Services.CategoriesService;
using TechWizMain.Services.HomeService;
using X.PagedList;
namespace TechWizMain.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }


        // GET: Category/ProductsList
        public async Task<IActionResult> ProductList(int cate_id)
        {
           
            return View(await _categoryService.GetProductsByCateID(cate_id));
        }


    }
}
