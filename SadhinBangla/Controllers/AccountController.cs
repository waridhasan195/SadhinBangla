using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SadhinBangla.Models.ViewModels;

namespace SadhinBangla.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;

        public AccountController(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
        }

       

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
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
            //Error Return View Back
            return View();

        }

    }
}
