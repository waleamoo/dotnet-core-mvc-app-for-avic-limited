using System.ComponentModel.DataAnnotations.Schema;

namespace AvicLimited.Data.Models
{
    public class ProjectImage : BaseEntity
    {
        public string ProjectImageDescription { get; set; } = string.Empty;
        public string ProjectImageUrl { get; set; } = string.Empty;

        public int ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public Project Project { get; set; } = new();
    }
}
