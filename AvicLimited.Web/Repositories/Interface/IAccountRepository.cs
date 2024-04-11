using AvicLimited.Web.Models;
using Microsoft.AspNetCore.Identity;

namespace AvicLimited.Web.Repositories.Interface
{
    public interface IAccountRepository
    {
        Task<IdentityResult> CreateUserAsync(SignUpVM model);
        Task<SignInResult> PasswordSignInAsync(SignInVM model);
        Task SignOutAsync();
        Task<IdentityResult> ChangePasswordAsync(ChangePasswordVM model);

    }
}
