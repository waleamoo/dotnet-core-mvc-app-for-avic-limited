using Microsoft.AspNetCore.Identity;

namespace AvicLimited.Data.Models
{
    public class AppUser : IdentityUser
    {
        public string ClientFirstname { get; set; } = string.Empty;
        public string ClientLastname { get; set; } = string.Empty;
        public string ClientCompany { get; set; } = string.Empty;
        public string ClientAddress { get; set; } = string.Empty;
    }
}
