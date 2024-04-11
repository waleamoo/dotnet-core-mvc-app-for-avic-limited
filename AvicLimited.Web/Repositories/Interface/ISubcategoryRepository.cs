using AvicLimited.Data.Models;
using AvicLimited.Web.Models;

namespace AvicLimited.Web.Repositories.Interface
{
    public interface ISubcategoryRepository : IGenericRepository<SubCategory>
    {
        Task<bool> AddSubcategory(SubcategoryCreateVM subcategoryCreateVM);
        Task<List<SubCategory>> GetSubCategoriesByCategoryId(int categoryId);
    }
}
