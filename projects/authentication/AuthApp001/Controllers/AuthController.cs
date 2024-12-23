using System.Security.Claims;
using AuthApp001.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace AuthApp001.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginFormModel model)
        {
            var account = UserManager.Login(model.UserName, model.Password);
            if (account != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, account.UserName)
                };
                claims.AddRange(account.Claims);
                var claimsIdentity = new ClaimsIdentity(claims, Settings.AuthCookieName);
                var principal = new ClaimsPrincipal(claimsIdentity);

                var props = new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTime.UtcNow.AddMinutes(20)
                };

                await HttpContext.SignInAsync(Settings.AuthCookieName,
                    principal, props);
                return RedirectToAction("Index", "Home");
            }
            else
            {
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(Settings.AuthCookieName);
            return RedirectToAction("Index", "Home");
        }
    }
}