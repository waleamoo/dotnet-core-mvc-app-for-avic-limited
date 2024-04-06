using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace AvicLimited.Web.Models
{
    public class CategoriesVM
    {
        public CategoryCreateVM CategoryCreateVM { get; set; } = new();
        public SubcategoryCreateVM SubcategoryCreateVM { get; set; } = new();
    }

    public class CategoryCreateVM
    {
        [StringLength(100, ErrorMessage = "Category name cannot be more than 50 characters")]
        [DisplayName("Category Name")]
        public string CategoryName { get; set; } = string.Empty;

        [StringLength(255, ErrorMessage = "Category description cannot be more than 255 characters"), MinLength(10, ErrorMessage = "Description cannot be less than 10 characters")]
        [DisplayName("Category Description")]
        public string CategoryDescription { get; set; } = string.Empty;
    }


    public class SubcategoryCreateVM
    {
        [Required]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        [StringLength(100, ErrorMessage = "Category name cannot be more than 50 characters")]
        [DisplayName("Subcategory Name")]
        public string SubcategoryName { get; set; } = string.Empty;

        [StringLength(255, ErrorMessage = "Category description cannot be more than 255 characters"), MinLength(10, ErrorMessage = "Description cannot be less than 10 characters")]
        [DisplayName("Subcategory Description")]
        public string SubcategoryDescription { get; set; } = string.Empty;
    }
}
