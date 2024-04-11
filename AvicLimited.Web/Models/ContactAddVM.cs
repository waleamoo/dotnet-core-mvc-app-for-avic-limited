using System.ComponentModel.DataAnnotations;

namespace AvicLimited.Web.Models
{
    public class ContactAddVM
    {
        [Required, MinLength(3), MaxLength(50)]
        public string ContactName { get; set; } = string.Empty;
        [EmailAddress]
        [Required]
        public string ContactEmail { get; set; } = string.Empty;
        [Required, MinLength(3), MaxLength(50)]
        public string ContactSubject { get; set; } = string.Empty;
        [Required, MinLength(3), MaxLength(255)]
        public string ContactMessage { get; set; } = string.Empty;
    }
}
