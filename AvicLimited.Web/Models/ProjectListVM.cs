using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using AvicLimited.Data.Models;

namespace AvicLimited.Web.Models
{
    public class ProjectListVM
    {
        [Required]
        public int Id { get; set; } 

        [StringLength(100, ErrorMessage = "Project name cannot be more than 100 characters")]
        [DisplayName("Project Name")]
        public string ProjectName { get; set; } 

        public string CategoryName { get; set; } 

        public string SubcategoryName { get; set; } 

        [Required]
        [DisplayName("Project Description")]
        public string ProjectDescription { get; set; } 

        public List<ProjectImage> ProjectImages { get; set; }
    }
}
