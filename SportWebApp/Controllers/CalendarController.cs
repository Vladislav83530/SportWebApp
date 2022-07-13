using DotNetCoreCalendar.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SportWebApp.Data.Interfaces;
using SportWebApp.Models;
using System.Security.Claims;

namespace SportWebApp.Controllers
{
    public class CalendarController :Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IEventRepository _event;
        private readonly UserManager<ApplicationUser> _usermanager;

        public CalendarController(ILogger<HomeController> logger, IEventRepository even, UserManager<ApplicationUser> usermanager)
        {
            _logger = logger;
            _event = even;
            _usermanager = usermanager;
        }

        /// <summary>
        /// Page My Calendar
        /// </summary>
        /// <returns>View with calendar</returns>
        [Authorize]
        public async Task<IActionResult> MyCalendar()
        {
            var userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewData["Events"] = JSONListHelper.GetEventListJSONString(await _event.GetMyEventsAsync(userid));
            return View();
        }
    }
}
