using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportWebApp.Data;
using SportWebApp.Models;
using SportWebApp.ViewModels;
using System.Security.Claims;

namespace SportWebApp.Controllers
{
    /// <summary>
    /// Create, Edit and Delete Exercise
    /// </summary>
    [Authorize]
    public class ExerciseController : Controller
    {
        private readonly ApplicationDbContext db;
        public ExerciseController(ApplicationDbContext context)
        {
            db = context;
        }

        /// <summary>
        /// All exercise
        /// </summary>
        /// <returns>View with all exercise (filtrated exercise)</returns>
        [HttpGet]
        public IActionResult Index(string name, string muscleGroup, string equipment)
        {
            string? currentUserId = GetCurUserId();
            List<Exercise> exercises = db.Exercises.Where(x => x.ApplicationUserId == currentUserId).ToList();

            if (!String.IsNullOrEmpty(name) && !String.IsNullOrEmpty(muscleGroup) && !String.IsNullOrEmpty(equipment))
            {
                exercises = exercises.Where(p => p.Name.Contains(name) && p.MuscleGroup.Contains(muscleGroup) && p.Equipment.Contains(equipment)).ToList();
            }
            if (!String.IsNullOrEmpty(name) && !String.IsNullOrEmpty(muscleGroup) && String.IsNullOrEmpty(equipment))
            {
                exercises = exercises.Where(p => p.Name.Contains(name) && p.MuscleGroup.Contains(muscleGroup)).ToList();
            }
            if (!String.IsNullOrEmpty(name) && String.IsNullOrEmpty(muscleGroup) && !String.IsNullOrEmpty(equipment))
            {
                exercises = exercises.Where(p => p.Name.Contains(name) && p.Equipment.Contains(equipment)).ToList();
            }
            if (String.IsNullOrEmpty(name) && !String.IsNullOrEmpty(muscleGroup) && !String.IsNullOrEmpty(equipment))
            {
                exercises = exercises.Where(p => p.MuscleGroup.Contains(muscleGroup) && p.Equipment.Contains(equipment)).ToList();
            }
            if (String.IsNullOrEmpty(name) && String.IsNullOrEmpty(muscleGroup) && !String.IsNullOrEmpty(equipment))
            {
                exercises = exercises.Where(p => p.Equipment.Contains(equipment)).ToList();
            }
            if (String.IsNullOrEmpty(name) && !String.IsNullOrEmpty(muscleGroup) && String.IsNullOrEmpty(equipment))
            {
                exercises = exercises.Where(p => p.MuscleGroup.Contains(muscleGroup)).ToList();
            }
            if (!String.IsNullOrEmpty(name) && String.IsNullOrEmpty(muscleGroup) && String.IsNullOrEmpty(equipment))
            {
                exercises = exercises.Where(p => p.Name.Contains(name)).ToList();
            }

            ExerciseViewModel exerciseViewModel = new ExerciseViewModel
            {
                Exercise = new Exercise(),
                Exercises = exercises,
                FilterViewModel = new FilterViewModel(name, muscleGroup, equipment)
            };
            return View(exerciseViewModel);
        }

        /// <summary>
        /// Create exercise
        /// </summary>
        /// <param name="_exercise"></param>
        /// <returns>View with created exercise</returns>
        [HttpPost]
        public async Task<IActionResult> CreateExercise(Exercise _exercise)
        {
            string? currentUserId = GetCurUserId();
            if (currentUserId != null)
            {
                Exercise exercise = new Exercise()
                {
                    Name = _exercise.Name,
                    Description = _exercise.Description,
                    Equipment = _exercise.Equipment,
                    ImageUrl = _exercise.ImageUrl,
                    Repetition = _exercise.Repetition,
                    MuscleGroup = _exercise.MuscleGroup,
                    Weight = _exercise.Weight,
                    ApproachCount = _exercise.ApproachCount,
                    ApplicationUserId = currentUserId
                };
                await db.Exercises.AddRangeAsync(exercise);
                await db.SaveChangesAsync();
            }
            TempData["AlertMessage"] = "Your exercise was created successfully!";
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Edit Exercise Get
        /// </summary>
        /// <param name="id"></param>
        /// <returns>View for editting exercise</returns>
        [HttpGet]
        public async Task<IActionResult> EditExercise(int? id)
        {
            string? currentUserId = GetCurUserId();
            if (currentUserId != null)
            {
                Exercise? exercise = await db.Exercises.FirstOrDefaultAsync(x => x.Id == id);
                if (exercise!=null)
                {
                    return View(exercise);
                }
            }
            return NotFound();
        }

        /// <summary>
        /// Edit Exercise Get (for training page)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> EditExerciseForTraining(int? id)
        {
            string? currentUserId = GetCurUserId();
            if (currentUserId != null)
            {
                Exercise? exercise = await db.Exercises.FirstOrDefaultAsync(x => x.Id == id);
                if (exercise != null)
                {
                    return View(exercise);
                }
            }
            return NotFound();
        }

        /// <summary>
        /// Edit Exercise Post
        /// </summary>
        /// <param name="_exercise"></param>
        /// <returns>View with edited exercise</returns>
        [HttpPost]
        public async Task<IActionResult> EditExercisePost(Exercise _exercise)
        {
                Exercise? exercise = await db.Exercises.FirstOrDefaultAsync(x => x.Id == _exercise.Id);
                if (exercise != null)
                {
                    exercise.Description = _exercise.Description;
                    exercise.Equipment = _exercise.Equipment;
                    exercise.Weight = _exercise.Weight;
                    exercise.Repetition = _exercise.Repetition;
                    exercise.MuscleGroup = _exercise.MuscleGroup;
                    exercise.Name = _exercise.Name;
                    exercise.ImageUrl = _exercise.ImageUrl;

                }
                await db.SaveChangesAsync();
                TempData["AlertMessage"] = "Your exercise was edited successfully!";
                return RedirectToAction("Index");
        }

        /// <summary>
        /// Edit Exercise Post (for training page)
        /// </summary>
        /// <param name="_exercise"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> EditExercisePostForTraining(Exercise _exercise)
        {
            Exercise? exercise = await db.Exercises.FirstOrDefaultAsync(x => x.Id == _exercise.Id);
            if (exercise != null)
            {
                exercise.Description = _exercise.Description;
                exercise.Equipment = _exercise.Equipment;
                exercise.Weight = _exercise.Weight;
                exercise.Repetition = _exercise.Repetition;
                exercise.MuscleGroup = _exercise.MuscleGroup;
                exercise.Name = _exercise.Name;
                exercise.ImageUrl = _exercise.ImageUrl;

            }
            await db.SaveChangesAsync();
            TempData["AlertMessage"] = "Your exercise was edited successfully. Now you can add this exercise to training!";
            return RedirectToAction("Index", "Training");
        }

        /// <summary>
        /// Delete Exercise
        /// </summary>
        /// <param name="id"></param>
        /// <returns>View with deleted exercise</returns>
        [HttpGet]
        public async Task<IActionResult> DeleteExercise(int? id)
        {
            if (id != null)
            {
                Exercise? exercise = await db.Exercises.FirstOrDefaultAsync(x => x.Id == id);
                if (exercise!=null)
                    db.Exercises.Remove(exercise);
                await db.SaveChangesAsync();
                TempData["AlertMessage"] = "Your exercise was deleted successfully!";
                return RedirectToAction("Index");
            }
            return NotFound();
        }

        /// <summary>
        /// Get current user id
        /// </summary>
        /// <returns>current user Id (string)</returns>
        public string? GetCurUserId()
        {
            ClaimsPrincipal currentUser = this.User;
            var currentUserID = currentUser.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return currentUserID;
        }
    }
}
