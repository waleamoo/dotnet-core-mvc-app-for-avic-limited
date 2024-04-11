using AvicLimited.Data.Models;
using AvicLimited.Web.Models;
using AvicLimited.Web.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace AvicLimited.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class AdminController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ISubcategoryRepository _subcategoryRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AdminController(ICategoryRepository categoryRepository,
            ISubcategoryRepository subcategoryRepository, IProjectRepository projectRepository, IWebHostEnvironment webHostEnvironment)
        {
            _categoryRepository = categoryRepository;
            _subcategoryRepository = subcategoryRepository;
            _projectRepository = projectRepository;
            _webHostEnvironment = webHostEnvironment;
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

        public async Task<ViewResult> CategoryEdit(int id)
        {
            var category = await _categoryRepository.GetAsync(id);
            CategoryEditVM model = new CategoryEditVM
            {
                Id = category.Id,
                CategoryName = category.CategoryName,
                CategoryDescription = category.CategoryDescription
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CategoryEdit(CategoryEditVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // map model 
                    Category category = new Category
                    {
                        Id = model.Id,
                        CategoryName = model.CategoryName,
                        CategoryDescription = model.CategoryDescription
                    };
                    await _categoryRepository.UpdateAsync(category);
                    TempData["subcategory-success"] = "Category updated";
                    return Redirect(Request.Headers["Referer"].ToString());
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Error occured editing subcategory");
            }

            TempData["subcategory-error"] = "Please fill all fields to edit a subcategory";
            return Redirect(Request.Headers["Referer"].ToString());
        }

        public async Task<IActionResult> CategoryDelete(int id)
        {
            await _categoryRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Categories));
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

        public async Task<ViewResult> SubcategoryEdit(int id)
        {
            var subcategory = await _subcategoryRepository.GetAsync(id);
            SubcategoryEditVM model = new SubcategoryEditVM
            {
                Id = subcategory.Id,
                CategoryId = subcategory.CategoryId,
                SubcategoryName = subcategory.SubCategoryName,
                SubcategoryDescription = subcategory.SubCategoryDescription
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubcategoryEdit(SubcategoryEditVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // map model 
                    SubCategory subcategory = new SubCategory
                    {
                        Id = model.Id,
                        CategoryId = model.CategoryId,
                        SubCategoryName = model.SubcategoryName,
                        SubCategoryDescription = model.SubcategoryDescription
                    };
                    await _subcategoryRepository.UpdateAsync(subcategory);
                    TempData["subcategory-success"] = "Subcategory updated";
                    return Redirect(Request.Headers["Referer"].ToString());
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Error occured editing subcategory");
            }

            TempData["subcategory-error"] = "Please fill all fields to edit a subcategory";
            return Redirect(Request.Headers["Referer"].ToString());
        }

        public async Task<IActionResult> SubcategoryDelete(int id)
        {
            await _subcategoryRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Categories));
        }


        // TODO: CRUD Quote Request
        // TODO: CRUD Contact Request
        // TODO: Modify Services Page to Multiple Models 
        // TODO: Add emailing functionality - Template should be from DB

        public async Task<ViewResult> Projects()
        {
            var projects = await _projectRepository.GetProjects();
            return View(projects);
        }

        public async Task<IActionResult> ProjectAdd()
        {
            ProjectAddVM model = new ProjectAddVM();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProjectAdd(ProjectAddVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (model.GalleryFiles != null)
                    {
                        string folder = "project/gallery/";
                        model.Gallery = new List<GalleryImageVM>();
                        foreach (var file in model.GalleryFiles)
                        {
                            var gallery = new GalleryImageVM()
                            {
                                Name = file.FileName,
                                URL = await UploadProjectImage(folder, file)
                            };
                            model.Gallery.Add(gallery);
                        }
                    }

                    if (await _projectRepository.AddProject(model))
                    {
                        //return RedirectToAction(nameof(Projects));
                        TempData["project-success"] = "Project saved";
                        return Redirect(Request.Headers["Referer"].ToString());
                    }
                }
                ModelState.AddModelError(string.Empty, "Error occured adding project");
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Error occured adding project");
            }

            TempData["project-error"] = "Please fill all fields to add a project";
            return View(model);
        }


        public async Task<IActionResult> ProjectEdit(int id)
        {
            var project = await _projectRepository.GetProject(id);
            if (project is not null)
            {
                return View(project);
            }
            TempData["project-error"] = "Project not found";
            return Redirect(Request.Headers["Referer"].ToString());
        }

        public async Task<IActionResult> ProjectDelete(int id)
        {
            var project = await _projectRepository.GetProject(id);
            if (project is not null)
            {
                if (project.ProjectImages.Any())
                    await DeleteProjectImages(project.ProjectImages);

                await _projectRepository.DeleteProject(id);
            }


            TempData["project-success"] = "Project deleted";
            return Redirect(Request.Headers["Referer"].ToString());
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProjectEdit(ProjectListVM model)
        {
            var project = await _projectRepository.GetAsync(model.Id);
            if (project is not null)
            {
                project.ProjectName = model.ProjectName;
                project.ProjectDescription = model.ProjectDescription;
                await _projectRepository.UpdateProject(project);
                return RedirectToAction(nameof(Projects));
            }
            TempData["project-error"] = "Error occured during project update";
            return Redirect(Request.Headers["Referer"].ToString());
        }

        private async Task<string> UploadProjectImage(string folderPath, IFormFile file)
        {
            folderPath += Guid.NewGuid().ToString() + "_" + file.FileName;
            string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folderPath);
            await file.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
            return "/" + folderPath;
        }

        private async Task DeleteProjectImages(List<ProjectImage> projectImages)
        {
            // Delete project images 
            if (projectImages != null)
            {
                string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath);
                foreach (var file in projectImages)
                {
                    string fileURL = uploadDir + file.ProjectImageUrl;
                    if (System.IO.File.Exists(fileURL))
                    {
                        System.IO.File.Delete(fileURL);
                    }
                }
            }
        }




    }
}
