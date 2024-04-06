using AvicLimited.Web.Models;
using AvicLimited.Web.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AvicLimited.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public HomeController(ILogger<HomeController> logger, ICategoryRepository categoryRepository, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _categoryRepository = categoryRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        public ViewResult Index()
        {
            return View();
        }

        public ViewResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> Services()
        {
            var model = await _categoryRepository.GetCategoriesAndSubcategories();
            return View(model);
        }

        public ViewResult Contact()
        {
            return View();
        }

        [HttpPost]
        public IActionResult UploadImage(IFormFile upload)
        {
            if (upload != null && upload.Length > 0)
            {
                var fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + upload.FileName;
                var path = Path.Combine(Directory.GetCurrentDirectory(), _webHostEnvironment.WebRootPath, "uploads", fileName);
                var stream = new FileStream(path, FileMode.Create);
                upload.CopyToAsync(stream);
                return new JsonResult(new { path = "/uploads/" + fileName });
            }

            return new JsonResult(new { path = "Error uploading file" });
            // return RedirectToAction("Create"); // method to add the resource to database => from a form
        }

        [HttpGet]
        public IActionResult UploadExplorer()
        {
            var dir = new DirectoryInfo(Path.Combine(Directory.GetCurrentDirectory(), _webHostEnvironment.WebRootPath, "uploads"));
            ViewBag.fileInfo = dir.GetFiles();
            return View("FileExplorer");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
