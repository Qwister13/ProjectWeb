using Microsoft.AspNetCore.Mvc;
using ProjectWeb.BL.Auth;
using ProjectWeb.Models;
using System.Diagnostics;

namespace ProjectWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly ICurrentUser currentUser;

        public HomeController(ILogger<HomeController> logger, ICurrentUser currentUser)
        {
            this.logger = logger;
            this.currentUser = currentUser;
        }

        public IActionResult Index()
        {            
            return View(currentUser.IsLoggedIn());
        }

        public IActionResult Privacy()
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