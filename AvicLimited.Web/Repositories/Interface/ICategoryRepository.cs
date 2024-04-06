using AvicLimited.Data.Models;
using AvicLimited.Web.Models;

namespace AvicLimited.Web.Repositories.Interface
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        Task<bool> AddCategory(CategoryCreateVM categoryCreateVM);
        Task<List<CategoryGetVM>> GetCategories();
        Task<List<CategoryListVM>> GetCategoriesAndSubcategories();

    }
}
