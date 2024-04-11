using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace AvicLimited.Web.Models
{
    public class ProjectAddVM
    {
        [StringLength(100, ErrorMessage = "Project name cannot be more than 100 characters")]
        [Display(Name = "Project Name")]
        public string ProjectName { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        [Display(Name = "Subcategory")]
        public int? SubCategoryId { get; set; }

        [Required]
        [Display(Name = "Project Description")]
        public string ProjectDescription { get; set; } = string.Empty;

        [Display(Name = "Choose the gallery images of your project")] // need for validating UI files 
        public IFormFileCollection? GalleryFiles { get; set; }

        public List<GalleryImageVM>? Gallery { get; set; } = new List<GalleryImageVM>();

    }
}
