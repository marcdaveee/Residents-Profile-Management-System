using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RPMS.ViewModels;

namespace RPMS.Controllers
{    
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Register (string? returnUrl = null)
        {
            
            //Check if there is an existing account in the db

            RegisterViewModel registerViewModel = new RegisterViewModel();
            registerViewModel.ReturnURl = returnUrl ?? Url.Content("~/");

            return View(registerViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register (RegisterViewModel registerViewModel, string? returnUrl = null)
        {

            returnUrl = returnUrl ?? Url.Content("~/");

            if (ModelState.IsValid)                
            {
                //Check if email already exists
                var existingUserEmail = await _userManager.FindByEmailAsync(registerViewModel.Email);

                if (existingUserEmail != null)
                {
                    ModelState.AddModelError("Email", "Email already exists.");
                    return View(registerViewModel);
                }
                else
                {
                    var user = new IdentityUser
                    {
                        UserName = registerViewModel.Email,
                        Email = registerViewModel.Email
                    };
                    var result = await _userManager.CreateAsync(user, registerViewModel.Password);
                    if (result.Succeeded)
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);

                        if(returnUrl != null && Url.IsLocalUrl(returnUrl)){
                            return LocalRedirect(returnUrl);
                        }
                        else
                        {
                            return RedirectToAction("Index", "Resident");
                        }
                    }

                    foreach(var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    
                }
                
            }

            return View(registerViewModel);
        }

        [AllowAnonymous]
        public ActionResult Login (string? returnUrl = null)
        {
            LoginViewModel loginViewModel = new LoginViewModel();
       
            return View(loginViewModel);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel, string? returnUrl = null)
        {
            

            if (ModelState.IsValid)
            {
                //var user = await _userManager.FindByEmailAsync(loginViewModel.Email);                
              
                //if (user == null)
                //{
                //    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                //    return View(loginViewModel);
                //}

                var user = await _userManager.FindByEmailAsync(loginViewModel.Email);

                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(loginViewModel);
                }

                var result = await _signInManager.PasswordSignInAsync(loginViewModel.Email, loginViewModel.Password, loginViewModel.RememberMe, false);

                if (result.Succeeded)
                {
                    if(returnUrl != null && Url.IsLocalUrl(returnUrl))
                    {
                        return LocalRedirect(returnUrl);
                    }
                    return RedirectToAction("Index", "Resident");
                }
                else
                {
                    if(result.IsLockedOut)
                    {
                        return View("Lockout");
                    }

                    ModelState.AddModelError(string.Empty, "Incorrect login credentials.");
                    return View(loginViewModel);
                }
            }
            return View(loginViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout(string? returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            await _signInManager.SignOutAsync();

            return RedirectToAction("login", "Account");
        }
    }
}
