using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportWebApp.Data;
using SportWebApp.Models;
using SportWebApp.ViewModels;
using System.Security.Claims;

namespace SportWebApp.Controllers
{
    [Authorize]
    public class ExerciseController : Controller
    {
        private readonly ApplicationDbContext db;
        public ExerciseController(ApplicationDbContext context)
        {
            db = context;
        }

        public IActionResult Index()
        {
            string? currentUserId = GetCurUserId();
            List<Exercise> exercises = db.Exercises.Where(x => x.ApplicationUserId == currentUserId).ToList();
            ExerciseViewModel exerciseViewModel = new ExerciseViewModel
            {
                Exercises = exercises
            };
            return View(exerciseViewModel);
        }

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
            return RedirectToAction("Index");
        }

        //[HttpGet]
        //public async Task<IActionResult> EditExerciseGet(int? id)
        //{
        //   string? currentUserId = GetCurUserId();
        //    if (currentUserId!=null)
        //    {
        //        Exercise? exercise = await db.Exercises.FirstOrDefaultAsync(x => x.Id == id);
        //        ExerciseViewModel exerciseViewModel = new ExerciseViewModel
        //        {
        //            Exercise = exercise
        //        };
        //        return View(exerciseViewModel);
        //    }            
        //    return NotFound();
        //}

        public string? GetCurUserId()
        {
            ClaimsPrincipal currentUser = this.User;
            var currentUserID = currentUser.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return currentUserID;
        }
    }
}
