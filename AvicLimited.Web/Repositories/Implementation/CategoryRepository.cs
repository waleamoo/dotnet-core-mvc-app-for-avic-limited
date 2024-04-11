using AvicLimited.Data.Models;
using AvicLimited.Web.Models;
using AvicLimited.Web.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace AvicLimited.Web.Repositories.Implementation
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        private readonly ApplicationContext _context;

        public CategoryRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> AddCategory(CategoryCreateVM categoryCreateVM)
        {
            Category category = new Category 
            { 
                CategoryName = categoryCreateVM.CategoryName, 
                CategoryDescription = categoryCreateVM.CategoryDescription,
                DateCreated = DateTime.UtcNow,
                DateModified = DateTime.UtcNow
            };
            await AddAsync(category);
            return true;
        }

        public async Task<List<CategoryGetVM>> GetCategories()
        {
            List<Category> categories = await GetAllAsync();
            return categories.Select(c => new CategoryGetVM
            {
                CategoryId = c.Id,
                CategoryName = c.CategoryName,
                CategoryDescription = c.CategoryDescription
            }).ToList();
        }

        public async Task<List<CategoryListVM>> GetCategoriesAndSubcategories()
        {
            var categories =  await _context.Categories.Include(x => x.SubCategories).ToListAsync();
            return categories.Select(x => new CategoryListVM
            {
                Id = x.Id,
                CategoryName = x.CategoryName,
                CategoryDescription = x.CategoryDescription,
                Subcategories = x.SubCategories.Select(c => new SubcategoryListVM
                {
                    Id = c.Id,
                    SubcategoryName = c.SubCategoryName,
                    SubcategoryDescription = c.SubCategoryDescription
                }).ToList()
            }).ToList();
        }
    }
}
