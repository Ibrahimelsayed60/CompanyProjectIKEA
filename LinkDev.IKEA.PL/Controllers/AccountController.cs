using LinkDev.IKEA.DAL.Entities;
using LinkDev.IKEA.PL.ViewModels.Accounts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LinkDev.IKEA.PL.Controllers
{
    public class AccountController : Controller
    {
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;

		public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
			_userManager = userManager;
			_signInManager = signInManager;
		}

        #region Register
        // Register

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid) // Server-side Validation
            {
                var User = new ApplicationUser()
                {
                    UserName = model.Email.Split("@")[0],
                    Email = model.Email,
                    FName = model.FName,
                    LName = model.LName,
                    IsAgree = model.IsAgree,
                };
                var Result = await _userManager.CreateAsync(User, model.Password);

                if (Result.Succeeded)
                {
                    return RedirectToAction(nameof(Login));
                }
                else
                {
                    foreach (var error in Result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);

                    }

                }
            }

            return View(model);
        }
        #endregion

        #region Login
        // Login

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var User = await _userManager.FindByEmailAsync(model.Email);

                if (User is not null)
                {
                    var Flag = await _userManager.CheckPasswordAsync(User, model.Password);

                    if (Flag)
                    {
                        var Result = await _signInManager.PasswordSignInAsync(User, model.Password, model.RememberMe, false);

                        if (Result.Succeeded)
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Incorrect password");
                    }

                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Email is not Exists");
                }
            }
            return View(model);
        } 
        #endregion



        // Sign out

        public new async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }

        // Forget Password
        // Reset Password
    }
}
