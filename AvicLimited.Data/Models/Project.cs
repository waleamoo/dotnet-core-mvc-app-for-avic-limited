namespace AvicLimited.Data.Models
{
    public class Project : BaseEntity
    {
        public string ProjectName { get; set; } = string.Empty;
        public string ProjectDescription { get; set; } = string.Empty;
    }
}
