using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace AvicLimited.Data.Models
{
    [Index(nameof(ProjectName))]
    public class Project : BaseEntity
    {
        public string ProjectName { get; set; } = string.Empty;
        public string ProjectDescription { get; set; } = string.Empty;

        public int CategoryId { get; set; }

        public int? SubCategoryId { get; set; }
        [ForeignKey(nameof(CategoryId))]

        public virtual Category Category { get; set; }
        [ForeignKey(nameof(SubCategoryId))]

        public virtual SubCategory? SubCategory { get; set; }

        public List<ProjectImage>? ProjectImages { get; set; } = new();
    }
}
