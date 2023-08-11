using ApplyGraduate.Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ApplyGraduate.MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> Login(string? ReturnUrl)
        {
            if (ReturnUrl != null)
                ViewData["returnUrl"] = ReturnUrl;
                
            return await Task.Run(() => View());
        }

        [HttpPost]
        public async Task<IActionResult> Login(User user, string? returnUrl)
        {
            var _user = await _userManager.FindByNameAsync(user.UserName);
            if(_user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(_user, user.PasswordHash, false, false);
                if (result.Succeeded)
                    return await Task.Run(() => Redirect(returnUrl ?? "/"));
                else
                    return await Task.Run(() => Content("Şifre Yanlış"));
            }
            return await Task.Run(() => Content("Kullanıcı Adı Yanlış"));
        }

        public async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return await Task.Run(() => Redirect("/"));
        }
    }
}