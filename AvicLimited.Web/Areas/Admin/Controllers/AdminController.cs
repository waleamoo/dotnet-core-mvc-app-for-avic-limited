using AvicLimited.Data.Models;
using AvicLimited.Web.Models;
using AvicLimited.Web.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;

namespace AvicLimited.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ISubcategoryRepository _subcategoryRepository;

        public AdminController(ICategoryRepository categoryRepository, ISubcategoryRepository subcategoryRepository)
        {
            _categoryRepository = categoryRepository;
            _subcategoryRepository = subcategoryRepository;
        }
        // GET: /admin/admin/index
        public IActionResult Index()
        {
            return View();
        }
        
        // GET: /admin/admin/login
        public ViewResult Login()
        {
            return View();
        }

        // TODO: add category and sub category with their description 
        public ViewResult Category()
        {
            CategoriesVM model = new CategoriesVM();
            return View(model);
        }

        public async Task<ViewResult> Categories()
        {
            var categories = await _categoryRepository.GetCategoriesAndSubcategories();
            return View(categories);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CategoryAdd(CategoryCreateVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (await _categoryRepository.AddCategory(model))
                    {
                        return RedirectToAction(nameof(Categories));
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Error occured adding category");
            }

            TempData["category-error"] = "Please fill all fields to add a category";
            return Redirect(Request.Headers["Referer"].ToString());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubcategoryAdd(SubcategoryCreateVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (await _subcategoryRepository.AddSubcategory(model))
                    {
                        return RedirectToAction(nameof(Categories));
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Error occured adding subcategory");
            }

            TempData["category-error"] = "Please fill all fields to add a subcategory";
            return Redirect(Request.Headers["Referer"].ToString());
        }

        public ViewResult SubCategory()
        {
            SubcategoryCreateVM model = new SubcategoryCreateVM
            {
                //Categories = new SelectList(await categoryRepository.GetAllAsync(), "Id", "Name")
            };
            return View(model);
        }

        public IActionResult Subcategory(SubcategoryCreateVM model)
        {
            if (ModelState.IsValid)
            {

            }
            return View(model);
        }

        // TODO: CRUD Services 

        // TODO: CRUD Quote Request

        // TODO: CRUD Projects 



    }
}
