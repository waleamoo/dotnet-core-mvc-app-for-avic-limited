using Microsoft.EntityFrameworkCore;

namespace AvicLimited.Data.Models
{
    [Index(nameof(CategoryName))]
    public class Category : BaseEntity
    {
        public string CategoryName { get; set; } = string.Empty;
        public string CategoryDescription { get; set; } = string.Empty;
        public List<SubCategory> SubCategories { get; set; }
    }
}
