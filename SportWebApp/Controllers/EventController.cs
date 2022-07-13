using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SportWebApp.Controllers.ActionsFilters;
using SportWebApp.Data.Interfaces;
using SportWebApp.Models;
using SportWebApp.ViewModels;
using System.Security.Claims;

namespace SportWebApp.Controllers
{
    /// <summary>
    /// Event Controller
    /// </summary>
    [Authorize]
    public class EventController : Controller
    {
        private readonly IEventRepository _event;
        private readonly UserManager<ApplicationUser> _usermanager;

        public EventController(IEventRepository even, UserManager<ApplicationUser> usermanager)
        {
            _event = even;
            _usermanager = usermanager;
        }

        /// <summary>
        /// Get all events
        /// </summary>
        /// <returns>View with events</returns>
        public async Task<IActionResult> Index()
        {
            if (TempData["Alert"] != null)
            {
                ViewData["Alert"] = TempData["Alert"];
            }
            return View(await _event.GetMyEventsAsync(User.FindFirstValue(ClaimTypes.NameIdentifier)));
        }

        /// <summary>
        /// Create event (GET)
        /// </summary>
        /// <returns>View for creating event</returns>
        public IActionResult Create()
        {
            return View(new EventViewModel(User.FindFirstValue(ClaimTypes.NameIdentifier)));
        }
        
        /// <summary>
        /// Create event (POST)
        /// </summary>
        /// <param name="vm"></param>
        /// <param name="form"></param>
        /// <returns>View with created event</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(EventViewModel vm, IFormCollection form)
        {
            try
            {
                await _event.CreateEventAsync(form);
                TempData["Alert"] = "Success! You created a new event for: " + form["Event.Name"];
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewData["Alert"] = "An error occurred: " + ex.Message;
                return View(vm);
            }
        }

        /// <summary>
        /// Edit event (GET)
        /// </summary>
        /// <param name="id"></param>
        /// <returns>View for editind event</returns>
        [UserAccessOnly]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _event.GetEventAsync((int)id);
            if (@event == null)
            {
                return NotFound();
            }
            var vm = new EventViewModel(@event, User.FindFirstValue(ClaimTypes.NameIdentifier));
            return View(vm);
        }

        /// <summary>
        /// Edit event
        /// </summary>
        /// <param name="id"></param>
        /// <param name="form"></param>
        /// <returns>View eith edited event</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(int id, IFormCollection form)
        {
            try
            {
                await _event.UpdateEventAsync(form);
                TempData["Alert"] = "Success! You modified an event for: " + form["Event.Name"];
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewData["Alert"] = "An error occurred: " + ex.Message;
                var vm = new EventViewModel(await _event.GetEventAsync(id), User.FindFirstValue(ClaimTypes.NameIdentifier));
                return View(vm);
            }
        }

        /// <summary>
        /// Delete event (get)
        /// </summary>
        /// <param name="id"></param>
        /// <returns>View for deleting event</returns>
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var @event = await _event.GetEventAsync((int)id);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        /// <summary>
        /// Delete event
        /// </summary>
        /// <param name="id"></param>
        /// <returns>View with deleted event</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _event.DeleteEventAsync(id);
            TempData["Alert"] = "You deleted an event.";
            return RedirectToAction(nameof(Index));
        }
    }
}
