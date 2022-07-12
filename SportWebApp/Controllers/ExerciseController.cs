using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportWebApp.Data;
using SportWebApp.Data.Interfaces;
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
        private readonly IExerciseRepository _exercise;
        public ExerciseController(IExerciseRepository exercise)
        {
            _exercise = exercise;
        }

        /// <summary>
        /// All exercise
        /// </summary>
        /// <returns>View with all exercise (filtrated exercise)</returns>
        [HttpGet]
        public async Task<IActionResult> Index(string name, string muscleGroup, string equipment)
        {
            string? currentUserId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (currentUserId != null)
            {
                List<Exercise> exercises = (List<Exercise>)await _exercise.GetExercisesAsync(currentUserId);

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
            return BadRequest();
        }

        /// <summary>
        /// Create exercise
        /// </summary>
        /// <param name="_exercise"></param>
        /// <returns>View with created exercise</returns>
        [HttpPost]
        public async Task<IActionResult> CreateExercise(Exercise exer)
        {
            string? currentUserId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (currentUserId != null)
            {
                await _exercise.CreateExerciseAsync(exer, currentUserId);
                TempData["AlertMessage"] = "Your exercise was created successfully!";
                return RedirectToAction("Index");
            }
            return BadRequest();
        }

        /// <summary>
        /// Edit Exercise Get
        /// </summary>
        /// <param name="id"></param>
        /// <returns>View for editting exercise</returns>
        [HttpGet]
        public async Task<IActionResult> EditExercise(int id)
        {
            string? currentUserId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (currentUserId != null)
            {
                var exercise = await _exercise.GetExerciseAsync(id);
                if (exercise != null)
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
        public async Task<IActionResult> EditExerciseForTraining(int id)
        {
            string? currentUserId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (currentUserId != null)
            {
                var exercise = await _exercise.GetExerciseAsync(id);
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
        public async Task<IActionResult> EditExercisePost(Exercise exer)
        {
            await _exercise.EditExerciseAsync(exer, exer.Id);
            TempData["AlertMessage"] = "Your exercise was edited successfully!";
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Edit Exercise Post (for training page)
        /// </summary>
        /// <param name="_exercise"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> EditExercisePostForTraining(Exercise exer)
        {
            await _exercise.EditExerciseAsync(exer, exer.Id);
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
                await _exercise.DeleteExerciseAsync(id);
                TempData["AlertMessage"] = "Your exercise was deleted successfully!";
                return RedirectToAction("Index");
            }
            return NotFound();
        }
    }
}
