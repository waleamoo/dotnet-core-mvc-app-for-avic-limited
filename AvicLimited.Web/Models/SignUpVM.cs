using System.ComponentModel.DataAnnotations;

namespace AvicLimited.Web.Models
{
    public class SignUpVM
    {
        [Required]
        [Display(Name = "First Name")]

        [RegularExpression(@"^[a-zA-Z-]+$", ErrorMessage = "Only letters allowed")]
        public string ClientFirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        [RegularExpression(@"^[a-zA-Z-]+$", ErrorMessage = "Only letters allowed")]
        public string ClientLastName { get; set; }
        [Display(Name = "Company")]

        [RegularExpression(@"^[a-zA-Z0-9 ]+$", ErrorMessage = "Only letters allowed")]
        public string? ClientCompany { get; set; }
        [Required]
        [Display(Name = "Address")]

        [RegularExpression(@"^[a-zA-Z0-9 ]+$", ErrorMessage = "Only letters allowed")]
        public string ClientAddress { get; set; }
        [Required]
        [Display(Name = "Phone")]

        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Only letters allowed")]
        public string ClientPhone { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Email Address")]

        public string ClientEmail { get; set; }
        [Required]
        [Compare(nameof(ConfirmPassword))]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [Display(Name = "Confirm Password")]

        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
