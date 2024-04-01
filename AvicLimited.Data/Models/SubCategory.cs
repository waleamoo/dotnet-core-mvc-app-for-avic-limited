using System.ComponentModel.DataAnnotations.Schema;

namespace AvicLimited.Data.Models
{
    public class SubCategory : BaseEntity
    {
        public string SubCategoryName { get; set; } = string.Empty;
        public string SubCategoryDescription { get; set;} = string.Empty;

        public int CategoryId { get; set;}
        [ForeignKey("ArtisanId")]
        public Category Category { get; set;}
    }
}