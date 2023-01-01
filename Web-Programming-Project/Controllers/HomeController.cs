using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Web_Programming_Project.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Web_Programming_Project.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        Context obj = new Context();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        
        public IActionResult Index()
        {
            var values = obj.Movies.ToList();
            return View(values);
        }

        [Authorize]
        [HttpGet]
        public IActionResult NewMovie() 
        {
            return View();       
        }

        [Authorize]
        [HttpPost]
        public IActionResult NewMovie(Movie movie)
        {
            obj.Movies.Add(movie);
            obj.SaveChanges();
            return RedirectToAction("Index");   
            
        }

        [Authorize]
        public IActionResult MovieDelete(int id)
        {
            var movie = obj.Movies.Find(id);
            obj.Movies.Remove(movie);
            obj.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize]
        public IActionResult MovieGet(int id)
        {
            var movie = obj.Movies.Find(id);
            return View("MovieGet", movie);
        }

        [Authorize]
        public IActionResult MovieUpdate(Movie movie)
        {
            
            var updatedMovie = obj.Movies.Find(movie.Id);
            updatedMovie.Name = movie.Name;
            updatedMovie.Director = movie.Director;
            updatedMovie.Composer = movie.Composer;
            updatedMovie.Language = movie.Language;
            updatedMovie.Rating = movie.Rating;
            updatedMovie.Writer = movie.Writer;
            updatedMovie.Star_Actor = movie.Star_Actor;
            updatedMovie.Genre = movie.Genre;
            updatedMovie.Synposis = movie.Synposis;


            obj.SaveChanges();
            return RedirectToAction("Index");
        }

       public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("SignIn", "User");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}