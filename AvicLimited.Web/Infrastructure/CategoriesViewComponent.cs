using AvicLimited.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AvicLimited.Web.Infrastructure
{
    public class CategoriesViewComponent : ViewComponent
    {
        private readonly ApplicationContext _context;

        public CategoriesViewComponent(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await GetCategoriesAsync();
            return View(categories);
        }

        private async Task<List<Category>> GetCategoriesAsync()
        {
            return await _context.Categories.Include(x => x.SubCategories).ToListAsync();
        }
    }
}
