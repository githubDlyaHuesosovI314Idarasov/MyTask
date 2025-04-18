using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using MyTaskApp.Models;

namespace WebFlightCompany.Controllers
{
    
    public sealed class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private static String _aboutText = "The link shortening algorithm generates a unique ID-based short code using base62 and associates it with the original link.";

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;

        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult About()
        {
            var model = new AboutPageModel
            {
                Description = _aboutText,
                IsAdmin = User.IsInRole("Admin")
            };

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult About(AboutPageModel model)
        {
            if (User.IsInRole("Admin") && !string.IsNullOrWhiteSpace(model.Description))
            {
                _aboutText = model.Description;
            }

            return RedirectToAction(nameof(About));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}
