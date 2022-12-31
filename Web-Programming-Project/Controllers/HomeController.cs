using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Web_Programming_Project.Models;

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

        //Homepage
        public IActionResult Index()
        {
            var values = obj.Movies.ToList();
            return View(values);
        }

        [HttpGet]
        public IActionResult NewMovie() 
        {
            return View();       
        }

        [HttpPost]
        public IActionResult NewMovie(Movie movie)
        {
            obj.SaveChanges
            movie.Movies
            movie.SaveChanges();
            return View();
        }

        public IActionResult SignIn()
        {
            return View();
        }

        public IActionResult SignUp() 
        {
            return View();  
        }

        public IActionResult AdminPanel() 
        {
            return View();        
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}