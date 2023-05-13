using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RunGroupWebApp.Data;
using RunGroupWebApp.Models;
using RunGroupWebApp.ViewModels;

namespace RunGroupWebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ApplicationDbContext _context;
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }
        public IActionResult Login()
        {
            var reponse = new LoginViewModel();
            return View(reponse);
        }
        [HttpPost]
        public async Task<IActionResult> login(LoginViewModel loginVM)
        {
            if(!ModelState.IsValid) { return View(loginVM); }

            var user = await _userManager.FindByEmailAsync(loginVM.EmailAddress);
            if(user != null)
            {
                //User is found, check password
                var PasswordCheck = await _userManager.CheckPasswordAsync(user, loginVM.Password);
                if(PasswordCheck)
                {
                    var result =await _signInManager.PasswordSignInAsync(user, loginVM.Password,false,false);
                    if(result.Succeeded)
                    {
                        return RedirectToAction("Index", "Dashboard");
                    }
                }
                //Wrong password
                TempData["Error"] = "Wrong credential, please try again";
                return View(loginVM);
            }
            TempData["Error"] = "Wrong credential, please try again";
            return View(loginVM);
        }

        public IActionResult Register()
        {
            var reponse = new RegisterViewModel();
            return View(reponse);
        }

        [HttpPost]
        public async Task<IActionResult> register(RegisterViewModel registerVM)
        {
            if(!ModelState.IsValid) return View(registerVM);

            var user = await _userManager.FindByEmailAsync(registerVM.EmailAddress);
            if(user != null)
            {
                TempData["Error"] = "Email address already in use";
                return View(registerVM);
            }

            var newUser = new AppUser()
            {
                Email = registerVM.EmailAddress,
                UserName = registerVM.EmailAddress
            };
            var newUserResponse = await _userManager.CreateAsync(newUser,registerVM.Password);
            if(newUserResponse.Succeeded)
            {
                await _userManager.AddToRoleAsync(newUser, UserRoles.User);
            }
            else
            {
                TempData["Error"] = "Create new user failed.";
            }
            return View("Register");
        }

        [HttpPost]
        public async Task<IActionResult> logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index","Home");
        }
    }
}
