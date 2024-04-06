namespace AvicLimited.Web.Models
{
    public class CategoryListVM
    {
        public string CategoryName { get; set; } = string.Empty;
        public string CategoryDescription { get; set; } = string.Empty;
        public List<SubcategoryListVM> Subcategories { get; set; } = new();
        
    }

    public class SubcategoryListVM
    {
        public string SubcategoryName { get; set; } = string.Empty;
        public string SubcategoryDescription { get; set; } = string.Empty;
    }
}
