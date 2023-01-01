using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Web_Programming_Project.Models;

namespace Web_Programming_Project.Controllers
{
    public class AdminController : Controller
    {
        Context obj = new Context();

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult SignIn()
        {
           ClaimsPrincipal claimUser = HttpContext.User;

            if (claimUser.Identity.IsAuthenticated)
                return RedirectToAction("AdminPanel", "User");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(Admin admin)
        {
            var admin_info = obj.Admins.FirstOrDefault(x => x.AdminUsername == admin.AdminUsername &&
            x.AdminPassword == admin.AdminPassword);

            var info = obj.Users.FirstOrDefault(x => x.UserName == admin.AdminUsername &&
            x.Password == admin.AdminPassword);
            if (admin_info != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, admin.AdminUsername)

                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                AuthenticationProperties properties = new AuthenticationProperties()
                {
                    AllowRefresh = true,
                    IsPersistent = true
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity), properties);

                return RedirectToAction("AdminPanel", "User");
            }
            
            ViewData["ValidateMessage"] = "USER NOT FOUND";
            return View();
        }
    }
}
