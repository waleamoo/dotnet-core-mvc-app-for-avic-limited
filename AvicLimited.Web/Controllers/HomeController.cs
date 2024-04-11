using AvicLimited.Web.Models;
using AvicLimited.Web.Repositories.Implementation;
using AvicLimited.Web.Repositories.Interface;
using AvicLimited.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AvicLimited.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly ISubcategoryRepository _subcategoryRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IUserService _userService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public HomeController(ILogger<HomeController> logger, ICategoryRepository categoryRepository, 
            IProjectRepository projectRepository, ISubcategoryRepository subcategoryRepository,
            IAccountRepository accountRepository, IUserService userService,
            IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _categoryRepository = categoryRepository;
            _projectRepository = projectRepository;
            _subcategoryRepository = subcategoryRepository;
            _accountRepository = accountRepository;
            _userService = userService;
            _webHostEnvironment = webHostEnvironment;
        }

        
        public ViewResult Index()
        {
            return View();
        }

        [HttpGet("privacy")]
        public ViewResult Privacy()
        {
            return View();
        }

        [HttpGet("services")]
        public async Task<IActionResult> Services()
        {
            // TODO: Implement pagination here 
            // TODO: Implement drift plugin 
            // TODO: Implement WhatsApp plugin
            var model = await _categoryRepository.GetCategoriesAndSubcategories();
            return View(model);
        }

        [HttpGet("contact")]
        public ViewResult Contact()
        {
            ContactAddVM model = new ContactAddVM();
            return View(model);
        }

        [HttpPost("contact")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Contact(ContactAddVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _projectRepository.ContactAdd(model);
                    
                    TempData["success"] = "Message sent";
                    return Redirect(Request.Headers["Referer"].ToString());
                }
            }
            catch (Exception)
            {

            }
            TempData["error"] = "Message error";
            ModelState.AddModelError(string.Empty, "Problem occured sending message");
            return Redirect(Request.Headers["Referer"].ToString());

        }

        [HttpPost("quote")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> QuoteAdd(QuoteAddVM model)
        {
            if (HttpContext.Request.HasFormContentType)
            {
                model.QuoteName = HttpContext.Request.Form["QuoteName"];
                model.QuoteEmail = HttpContext.Request.Form["QuoteEmail"];
                model.QuotePhone = HttpContext.Request.Form["QuotePhone"];
                model.QuoteBudget = int.Parse(HttpContext.Request.Form["QuoteBudget"]);
                model.CategoryId = int.Parse(HttpContext.Request.Form["CategoryId"]);
                model.QuoteMessage = HttpContext.Request.Form["QuotePhone"];
            }

            try
            {
                if (ModelState.IsValid)
                {
                    await _projectRepository.QuoteAdd(model);
                    
                    TempData["success"] = "Quote sent";
                    return Redirect(Request.Headers["Referer"].ToString());
                }
            }
            catch (Exception)
            {

            }
            TempData["error"] = "Quote error";
            return Redirect(Request.Headers["Referer"].ToString());
        }

        [HttpGet("projects/{p}/{categoryId}/{subCategoryId}")]
        public async Task<IActionResult> Projects(int p = 1, int categoryId = 0, int subCategoryId = 0)
        {
            var projects = await _projectRepository.GetProjectsByCategory(categoryId, subCategoryId);
            int pageSize = 6; 
            var models = projects.Skip((p - 1) * pageSize).Take(pageSize);
            ViewBag.PageNumber = p;
            ViewBag.PageRange = pageSize;
            ViewBag.TotalPages = (int)Math.Ceiling((decimal)projects.Count() / pageSize);
            ViewBag.CategoryId = categoryId;
            ViewBag.SubCategoryId = subCategoryId;
            return View(models.ToList());
        }

        [HttpGet("project/{id}")]
        public async Task<IActionResult> Project(int id)
        {
            var project = await _projectRepository.GetProject(id);
            return View(project);
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

        // for dropdown 
        public async Task<JsonResult> GetSubCategoryByCategoryId(int id)
        {
            var subcategories = await _subcategoryRepository.GetSubCategoriesByCategoryId(id);
            return new JsonResult(subcategories);
        }

        /**************************************Authentication section******************************************/
        [HttpGet("register")]
        [AllowAnonymous]
        public async Task<ViewResult> Register()
        {
            SignUpVM model = new SignUpVM();
            return View(model);
        }

        [HttpPost("register")]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(SignUpVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = _accountRepository.CreateUserAsync(model);
                    if (!result.Result.Succeeded)
                    {
                        foreach (var error in result.Result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                        return View(model);
                    }
                    ModelState.Clear();
                    TempData["success"] = "Registration successful";
                    return Redirect(Request.Headers["Referer"].ToString());
                }
            }
            catch (Exception)
            {
                throw;
            }
            return View(model);
        }

        [HttpGet("login")]
        [AllowAnonymous]
        public async Task<ViewResult> Login()
        {
            SignInVM model = new SignInVM();
            return View(model);
        }

        [HttpPost("login")]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(SignInVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = _accountRepository.PasswordSignInAsync(model);
                    if (result.Result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    if (result.Result.IsNotAllowed)
                    {
                        ModelState.AddModelError(string.Empty, "Login error");
                    }
                    return Redirect(Request.Headers["Referer"].ToString());
                }
            }
            catch (Exception)
            {
                throw;
            }
            return View(model);
        }

        [Authorize]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            await _accountRepository.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        [HttpGet("change-password")]
        public async Task<ViewResult> ChangePassword()
        {
            var model = new ChangePasswordVM();
            return View(model);
        }

        [Authorize]
        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword(ChangePasswordVM model)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountRepository.ChangePasswordAsync(model);
                if (result.Succeeded)
                {
                    TempData["success"] = "Password changeed successfully";
                    return View();
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }
    }
}
