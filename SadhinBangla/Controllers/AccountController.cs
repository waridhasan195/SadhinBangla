using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SadhinBangla.Models.ViewModels;

namespace SadhinBangla.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {

            if (ModelState.IsValid)
            {
                var identityUser = new IdentityUser
                {
                    UserName = registerViewModel.UserName,
                    Email = registerViewModel.Email,
                };

                var identityUserResult = await userManager.CreateAsync(identityUser, registerViewModel.Password);

                if (identityUserResult.Succeeded)
                {
                    //assign this user the "User" role
                    var roleIdentityResult = await userManager.AddToRoleAsync(identityUser, "User");
                    if (roleIdentityResult.Succeeded)
                    {
                        //Success Notificatioon
                        return RedirectToAction("Register");
                    }
                }
            }
            
            //Error Return View Back
            return View();

        }

        [HttpGet]
        public IActionResult Login(string ReturnUrl)
        {
            
            var model = new LoginViewModel
            {
                ReturnUrl = ReturnUrl
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var signinResult = await signInManager.PasswordSignInAsync(loginViewModel.UserName, loginViewModel.Password, false, false);
            if (signinResult != null && signinResult.Succeeded)
            {
                if (!string.IsNullOrWhiteSpace(loginViewModel.ReturnUrl))
                {
                    return Redirect(loginViewModel.ReturnUrl);
                }

                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpGet] 
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
 