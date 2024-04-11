using AvicLimited.Data.Models;
using AvicLimited.Data.SeedEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace AvicLimited.Web.Infrastructure
{
    public class AppUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<AppUser, IdentityRole>
    {
        public AppUserClaimsPrincipalFactory(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IOptions<IdentityOptions> options) : base(userManager, roleManager, options)
        {

        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(AppUser user)
        {
            var identity = await base.GenerateClaimsAsync(user);
            identity.AddClaim(new Claim("ClientFirstname", user.ClientFirstname ?? ""));
            identity.AddClaim(new Claim("ClientLastname", user.ClientLastname ?? ""));
            return identity;
        }
    }
}
