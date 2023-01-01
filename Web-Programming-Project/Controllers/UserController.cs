using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System.Security.Claims;
using Web_Programming_Project.Models;
using Web_Programming_Project.Resources.Languages;

namespace Web_Programming_Project.Controllers
{
    public class UserController : Controller
    {
        Context obj = new Context();

        

        [HttpGet]
        public IActionResult SignIn()
        {
            ClaimsPrincipal claimUser = HttpContext.User;

            if(claimUser.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(User user)
        {
           
            var info = obj.Users.FirstOrDefault(x => x.UserName == user.UserName &&
            x.Password == user.Password);
            
            if (info != null)
            {
               
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName)
                    
                };
               
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                AuthenticationProperties properties = new AuthenticationProperties()
                {
                    AllowRefresh= true,
                    IsPersistent =  true
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity), properties);

                if(user.UserName == "b201210097@sakarya.edu.tr" & user.Password == "sau")
                    return RedirectToAction("AdminPanel", "User");

                return RedirectToAction("Index", "Home");
                 

            }
            ViewData["ValidateMessage"] = "USER NOT FOUND";
            return View();
        }

        
        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignUp(User user)
        {
            Console.WriteLine("SignUp action method called");
            // Code to create a new user record in the database

            obj.Users.Add(user);
            obj.SaveChanges();

            Console.WriteLine("Redirecting to SignIn action method");
            // Redirect to the SignIn action method
            return RedirectToAction("SignIn");
        }

        [Authorize]
        public IActionResult AdminPanel()
        {
            var values = obj.Users.ToList();
            return View(values);
        }
        public IActionResult UserDelete(int id)
        {
            var user = obj.Users.Find(id);
            obj.Users.Remove(user);
            obj.SaveChanges();
            return RedirectToAction("AdminPanel");
        }
    }
}
