using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportWebApp.Data;
using SportWebApp.Models;
using SportWebApp.ViewModels;
using System.Security.Claims;
using System.Text.Json;

namespace SportWebApp.Controllers
{
	/// <summary>
	/// Create, Edit, Delete training
	/// </summary>
	[Authorize]
	public class TrainingController : Controller
	{
		private readonly ApplicationDbContext db;

		public TrainingController(ApplicationDbContext context)
		{
			db = context;
		}

		/// <summary>
		/// Show all trainings (filtraition)
		/// </summary>
		/// <returns>View with filtreted trainings</returns>
		[HttpGet]
		public IActionResult Index(string name, string muscleGroup, string equipment)
		{
			string? currentUserId = GetCurUserId();
			List<Training> trainings = db.Trainings.Where(x => x.ApplicationUserId == currentUserId).ToList();

			

			if (!String.IsNullOrEmpty(name) && !String.IsNullOrEmpty(muscleGroup) && !String.IsNullOrEmpty(equipment))
			{
				trainings = trainings.Where(p => p.Name.Contains(name) && p.MuscleGroup.Contains(muscleGroup) && p.Equipment.Contains(equipment)).ToList();
			}
			if (!String.IsNullOrEmpty(name) && !String.IsNullOrEmpty(muscleGroup) && String.IsNullOrEmpty(equipment))
			{
				trainings = trainings.Where(p => p.Name.Contains(name) && p.MuscleGroup.Contains(muscleGroup)).ToList();
			}
			if (!String.IsNullOrEmpty(name) && String.IsNullOrEmpty(muscleGroup) && !String.IsNullOrEmpty(equipment))
			{
				trainings = trainings.Where(p => p.Name.Contains(name) && p.Equipment.Contains(equipment)).ToList();
			}
			if (String.IsNullOrEmpty(name) && !String.IsNullOrEmpty(muscleGroup) && !String.IsNullOrEmpty(equipment))
			{
				trainings = trainings.Where(p => p.MuscleGroup.Contains(muscleGroup) && p.Equipment.Contains(equipment)).ToList();
			}
			if (String.IsNullOrEmpty(name) && String.IsNullOrEmpty(muscleGroup) && !String.IsNullOrEmpty(equipment))
			{
				trainings = trainings.Where(p => p.Equipment.Contains(equipment)).ToList();
			}
			if (String.IsNullOrEmpty(name) && !String.IsNullOrEmpty(muscleGroup) && String.IsNullOrEmpty(equipment))
			{
				trainings = trainings.Where(p => p.MuscleGroup.Contains(muscleGroup)).ToList();
			}
			if (!String.IsNullOrEmpty(name) && String.IsNullOrEmpty(muscleGroup) && String.IsNullOrEmpty(equipment))
			{
				trainings = trainings.Where(p => p.Name.Contains(name)).ToList();
			}

			TrainingViewModel trainingViewModel = new TrainingViewModel
			{
				Trainings = trainings,
				FilterViewModel = new FilterViewModel(name, muscleGroup, equipment)
			};

			return View(trainingViewModel);
		}

		/// <summary>
		/// Create training 
		/// </summary>
		/// <param name="_training"></param>
		/// <returns>View with created training</returns>
		[HttpPost]
		public async Task<IActionResult> CreateTraining(Training _training)
		{
			string? currentUserId = GetCurUserId();
			if (currentUserId != null)
			{
				Training training = new Training()
				{
					Name = _training.Name,
					Date= _training.Date,
					Duration = _training.Duration,
					Place = _training.Place,
					Calories=_training.Calories,
					Notes=_training.Notes,
					Feeling = _training.Feeling,
					MuscleGroup = _training.MuscleGroup,
					ImageUrl = _training.ImageUrl,
					Exercises="[]",
					ApplicationUserId = currentUserId
				};
				await db.Trainings.AddRangeAsync(training);
				await db.SaveChangesAsync();
			}
			TempData["AlertMessage"] = "Your training was created successfully!";
			return RedirectToAction("Index");
		}

		/// <summary>
		/// Edit training
		/// </summary>
		/// <param name="id"></param>
		/// <returns>View with form for editing training</returns>
		[HttpGet]
		public async Task<IActionResult> EditTraining(int? id)
		{
			string? currentUserId = GetCurUserId();
			if (currentUserId != null)
			{
				Training? training = await db.Trainings.FirstOrDefaultAsync(x => x.Id == id);
				if (training != null)
				{
					return View(training);
				}
			}
			return NotFound();
		}

		/// <summary>
		/// Edit training
		/// </summary>
		/// <param name="_training"></param>
		/// <returns>View with edited training</returns>
		[HttpPost]
		public async Task<IActionResult> EditTrainingPost(Training _training)
		{
			Training? training = await db.Trainings.FirstOrDefaultAsync(x => x.Id == _training.Id);
			if (training != null)
			{
				training.Name = _training.Name;
				training.Date = _training.Date;
				training.Duration = _training.Duration;
				training.MuscleGroup = _training.MuscleGroup;
				training.Place = _training.Place;
				training.Calories = _training.Calories;
				training.Notes = _training.Notes;
				training.Feeling = _training.Feeling;
				training.ImageUrl = _training.ImageUrl;
			}			
			await db.SaveChangesAsync();
			TempData["AlertMessage"] = "Your training was edited successfully!";
			return RedirectToAction("Index");
		}

		/// <summary>
		/// Delete training
		/// </summary>
		/// <param name="id"></param>
		/// <returns>View with deleted training</returns>
		[HttpGet]
		public async Task<IActionResult> DeleteTraining(int? id)
		{
			if (id != null)
			{
				Training? training = await db.Trainings.FirstOrDefaultAsync(x => x.Id == id);
				if (training != null)
					db.Trainings.Remove(training);
				await db.SaveChangesAsync();
				TempData["AlertMessage"] = "Your training was deleted successfully!";
				return RedirectToAction("Index");
			}
			return NotFound();
		}
		
		/// <summary>
		/// Add Exercise Page with filtration
		/// </summary>
		/// <param name="id"></param>
		/// <param name="name"></param>
		/// <param name="muscleGroup"></param>
		/// <param name="equipment"></param>
		/// <returns>View for adding exercise to training</returns>
		[HttpGet]
		public async Task<IActionResult> AddExercise(int? id, string name, string muscleGroup, string equipment)
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
			if (currentUserId != null)
			{
				Training? training = await db.Trainings.FirstOrDefaultAsync(x => x.Id == id);
				TrainingViewModel trainingViewModel = new TrainingViewModel
				{
					Exercises=exercises,
					Training = training,
					FilterViewModel = new FilterViewModel(name, muscleGroup, equipment)
				};
				if (training != null)
				{
					return View(trainingViewModel);
				}
			}
			return NotFound();
		}

		/// <summary>
		/// Add Exercise Post method
		/// </summary>
		/// <param name="_exercise"></param>
		/// <param name="_training"></param>
		/// <returns>View page with added exercise to training</returns>
		[HttpPost]
		public async Task<IActionResult> AddExercisePost(Exercise _exercise, Training _training)
		{
			Training? training = await db.Trainings.FirstOrDefaultAsync(x => x.Id == _training.Id);
			if (training.Exercises != null)
			{
				List<Exercise> exercisesList = JsonSerializer.Deserialize<List<Exercise>>(training.Exercises);
				_exercise.Id = exercisesList.Count+1;
				exercisesList.Add(_exercise);
				
				if (training != null)
				{
					string exercises = JsonSerializer.Serialize(exercisesList);
					training.Exercises = exercises;
				}
			}
			await db.SaveChangesAsync();
			TempData["AlertMessage"] = "Your exercise was added successfully!";
			return RedirectToAction("Index");
		}

		/// <summary>
		/// Delete exercise
		/// </summary>
		/// <param name="id"></param>
		/// <param name="idexer"></param>
		/// <returns>View with deleted exercise in training</returns>
		[HttpGet]
		public async Task<IActionResult> DeleteExercise(int? id, int? idexer)
        {
			if (id != null && idexer != null)
			{
				Training? training = await db.Trainings.FirstOrDefaultAsync(x => x.Id == id);
				if (training.Exercises != null)
				{
					List<Exercise> exercisesList = JsonSerializer.Deserialize<List<Exercise>>(training.Exercises);
					Exercise exercise = exercisesList.FirstOrDefault(x=>x.Id == idexer);
					exercisesList.Remove(exercise);

					if (training != null)
					{
						string exercises = JsonSerializer.Serialize(exercisesList);
						training.Exercises = exercises;
					}
				}
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
