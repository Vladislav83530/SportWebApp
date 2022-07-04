using Microsoft.AspNetCore.Mvc;
using SportWebApp.ViewModels;
using System.Diagnostics;

namespace SportWebApp.Controllers
{
    /// <summary>
    /// Controller for Home Page
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// Home Page
        /// </summary>
        /// <returns>View for Home Page</returns>
        public IActionResult Index()
        {
            return View();
        }
    }
}