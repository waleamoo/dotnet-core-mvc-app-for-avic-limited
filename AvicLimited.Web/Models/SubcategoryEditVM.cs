using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace AvicLimited.Web.Models
{
    public class SubcategoryEditVM
    {
        [Required]
        public int Id { get; set; }
        
        [Required]
        [DisplayName("Category")]
        public int CategoryId { get; set; }

        [StringLength(100, ErrorMessage = "Category name cannot be more than 50 characters")]
        [DisplayName("Subcategory Name")]
        public string SubcategoryName { get; set; } = string.Empty;

        [Required]
        [DisplayName("Subcategory Description")]
        public string SubcategoryDescription { get; set; } = string.Empty;
    }
}
