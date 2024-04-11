using System.ComponentModel.DataAnnotations;

namespace AvicLimited.Web.Models
{
    public class QuoteAddVM
    {
        [Required, MinLength(3), MaxLength(50)]
        public string QuoteName { get; set; } = string.Empty;
        [EmailAddress]
        [Required]
        public string QuoteEmail { get; set; } = string.Empty;
        [StringLength(15)]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Only numbers allowed")]
        public string QuotePhone { get; set; } = string.Empty;
        [Required]
        public int CategoryId { get; set; }
        [Required, MinLength(3), MaxLength(50)]
        public int QuoteBudget { get; set; }
        [MinLength(3), MaxLength(255)]
        [RegularExpression(@"^[a-zA-Z ]+$", ErrorMessage = "Only letters allowed")]
        public string? QuoteMessage { get; set; } = string.Empty;
    }
}
