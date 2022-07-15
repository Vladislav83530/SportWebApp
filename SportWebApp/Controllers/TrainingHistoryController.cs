using Microsoft.AspNetCore.Mvc;
using SportWebApp.Data.Interfaces;
using SportWebApp.Models;
using System.Security.Claims;

namespace SportWebApp.Controllers
{
    public class TrainingHistoryController : Controller
    {
        private readonly ITrainingHistoryRepository _trainhistory;

        public TrainingHistoryController(ITrainingHistoryRepository trainhistory)
        {
            _trainhistory = trainhistory;
        }

        /// <summary>
        /// Add training to history
        /// </summary>
        /// <param name="training"></param>
        /// <returns>View User Profile for watching history</returns>
        [HttpPost]
        public async Task<IActionResult> AddToHistory(TrainingHistory training)
        {
            string? currentUserId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (currentUserId != null)
                await _trainhistory.CreateTrainingAsync(training, currentUserId);
            TempData["AlertMessage"] = "Your training was added to history successfully!";
            return RedirectToAction("Index", "ProfileInfo");
        }

        /// <summary>
        /// Get timeline
        /// </summary>
        /// <returns>View with timeline</returns>
        [HttpGet]
        public async Task<IActionResult> Timeline()
        {
            string? currentUserId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (currentUserId != null)
            {
                List<TrainingHistory> trainings = (List<TrainingHistory>)await _trainhistory.GetTrainingsAsync(currentUserId);
                var orderedTrainings = trainings.OrderByDescending(n => n.Date);
                return View(orderedTrainings);
            }
            return BadRequest();
        }

        /// <summary>
        /// Delete history
        /// </summary>
        /// <returns>View without timeline</returns>
        public async Task<IActionResult> DeleteHistory()
        {
            string? currentUserId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (currentUserId != null)
            {
                await _trainhistory.DeleteTrainingsAsync();
                return RedirectToAction("Timeline", "TrainingHistory");
            }
            return BadRequest();
        }
    }
}
