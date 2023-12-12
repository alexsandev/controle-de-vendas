using Microsoft.AspNetCore.Identity;

namespace SalesWebMvc.Services
{
    public class AccountService
    {
        private readonly UserManager<IdentityUser> _userManager;

        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IdentityResult> RegisterAsync(string username, string password, string email, string phone)
        {
            var user = new IdentityUser { 
                UserName = username, 
                NormalizedUserName = username.ToUpper().Trim(), 
                Email = email,
                NormalizedEmail = email.ToUpper().Trim(),
                PhoneNumber = phone,
            };

            return await _userManager.CreateAsync(user, password);
        }

        public async Task<SignInResult> LoginAsync(string username, string password, bool isPersistent)
        {
            return await _signInManager.PasswordSignInAsync(username, password, isPersistent, false);
        }


        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }

    }
}