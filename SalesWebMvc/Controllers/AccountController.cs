using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using SalesWebMvc.Models.ViewModels;
using SalesWebMvc.Services;

namespace SalesWebMvc.Controllers
{
    public class AccountController : Controller
    {
        private readonly AccountService _accountService;

        public AccountController(AccountService accountService)
        {
            _accountService = accountService;
        }
        public IActionResult Index()
        {
            return RedirectToAction(nameof(Login));
        }

        [HttpGet]
        public IActionResult Register()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([FromForm] RegisterViewModel account)
        {

            if(!ModelState.IsValid)
            {
                return View(account);
            }

            if(account.Username == null || account.Password == null || account.Email == null || account.PhoneNumber == null)
            {
                return View(account);
            }

            var result = await _accountService.RegisterAsync(account.Username, account.Password, account.Email, account.PhoneNumber);

            if(!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
               
                return View(account);
            }

            return RedirectToAction("Index", "Home");
        }

        
        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([FromForm]LoginViewModel account)
        {
            if (!ModelState.IsValid)
            {
               return View(account);
            }

            if(account.Username == null || account.Password == null)
            {
                return View(account);
            }

            var result = await _accountService.LoginAsync(account.Username, account.Password, account.StayConnected);

            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Não foi possivel entrar na conta, verifique os campos e tente novamente.");
                return View(account);
            }

            if(account.ReturnUrl != null)
            {
                return LocalRedirect(account.ReturnUrl);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Logout(string returnUrl)
        {
            await _accountService.LogoutAsync();

            if(returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}