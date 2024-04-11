using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace AvicLimited.Web.Models
{
    public class CategoryEditVM
    {
        [Required]
        public int Id { get; set; }

        [StringLength(100, ErrorMessage = "Category name cannot be more than 50 characters")]
        [DisplayName("Category Name")]
        public string CategoryName { get; set; } = string.Empty;

        [DisplayName("Category Description")]
        public string CategoryDescription { get; set; } = string.Empty;
    }
}
