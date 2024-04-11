using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AvicLimited.Web.Models
{
    public class ChangePasswordVM
    {
        [Required, DataType(DataType.Password), Display(Name = "Current Password")]
        public string CurrentPassword { get; set; } = string.Empty;
        [Required, DataType(DataType.Password), Display(Name = "New Password")]
        public string NewPassword { get; set; } = string.Empty;
        [Required, DataType(DataType.Password), Display(Name = "Confirm New Password")]
        [Compare("NewPassword", ErrorMessage = "Password does not match")]
        public string ConfirmNewPassword { get; set; } = string.Empty;
    }
}
