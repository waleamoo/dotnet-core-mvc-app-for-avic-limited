using Microsoft.AspNetCore.Mvc;

namespace AvicLimited.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminController : Controller
    {
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
    }
}
