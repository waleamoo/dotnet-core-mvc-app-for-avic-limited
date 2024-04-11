using AvicLimited.Data.Common;
using AvicLimited.Data.Models;
using AvicLimited.Web.Models;
using AvicLimited.Web.Repositories.Interface;
using AvicLimited.Web.Services;
using Microsoft.AspNetCore.Identity;

namespace AvicLimited.Web.Repositories.Implementation
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IUserService _userService;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountRepository(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, 
            IUserService userService, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userService = userService;
            _roleManager = roleManager;
        }

        public async Task<IdentityResult> CreateUserAsync(SignUpVM model)
        {
            var user = new AppUser()
            {
                UserName = model.ClientEmail,
                NormalizedEmail = model.ClientEmail.ToUpper(),
                NormalizedUserName = model.ClientEmail.ToUpper(),
                ClientFirstname = model.ClientFirstName,
                ClientLastname = model.ClientLastName,
                ClientCompany = model.ClientCompany ?? "",
                ClientAddress = model.ClientAddress,
                PhoneNumber = model.ClientPhone,
                Email = model.ClientEmail,
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                // the created user to a User role
                var userRole = _roleManager.FindByNameAsync(Roles.User).Result;
                if (userRole != null)
                {
                    IdentityResult roleResult = await _userManager.AddToRoleAsync(user, userRole.Name);
                }
                //await GenerateEmailCofirmationTokenAsync(user);
            }
            return result;
        }

        public async Task<SignInResult> PasswordSignInAsync(SignInVM model)
        {
            return await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);

        }

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<IdentityResult> ChangePasswordAsync(ChangePasswordVM model)
        {
            var userId = _userService.GetUserId();
            var user = await _userManager.FindByIdAsync(userId) ?? throw new Exception("User not found");
            return await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
        }

    }
}
