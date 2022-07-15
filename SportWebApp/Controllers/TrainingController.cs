using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportWebApp.Data;
using SportWebApp.Data.Interfaces;
using SportWebApp.Models;
using SportWebApp.ViewModels;
using System.Security.Claims;

namespace SportWebApp.Controllers
{
	/// <summary>
	/// Create, Edit, Delete training
	/// </summary>
	[Authorize]
	public class TrainingController : Controller
	{
		private readonly ITrainingRepository _training;
		private readonly IExerciseRepository _exercise;

		public TrainingController(ITrainingRepository training, IExerciseRepository exercise)
		{
			_training = training;
			_exercise = exercise;
		}

		/// <summary>
		/// Show all trainings (filtraition)
		/// </summary>
		/// <returns>View with filtreted trainings</returns>
		[HttpGet]
		public  async Task<IActionResult> Index(string name, string muscleGroup, string equipment)
		{
			string? currentUserId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			if (currentUserId != null)
			{
				List<Training> trainings = (List<Training>)await _training.GetTrainingsAsync(currentUserId);

				if (!String.IsNullOrEmpty(name) && !String.IsNullOrEmpty(muscleGroup) && !String.IsNullOrEmpty(equipment))
				{
					trainings = trainings.Where(p => p.Name.Contains(name) && p.MuscleGroup.Contains(muscleGroup) && p.Place.Contains(equipment)).ToList();
				}
				if (!String.IsNullOrEmpty(name) && !String.IsNullOrEmpty(muscleGroup) && String.IsNullOrEmpty(equipment))
				{
					trainings = trainings.Where(p => p.Name.Contains(name) && p.MuscleGroup.Contains(muscleGroup)).ToList();
				}
				if (!String.IsNullOrEmpty(name) && String.IsNullOrEmpty(muscleGroup) && !String.IsNullOrEmpty(equipment))
				{
					trainings = trainings.Where(p => p.Name.Contains(name) && p.Place.Contains(equipment)).ToList();
				}
				if (String.IsNullOrEmpty(name) && !String.IsNullOrEmpty(muscleGroup) && !String.IsNullOrEmpty(equipment))
				{
					trainings = trainings.Where(p => p.MuscleGroup.Contains(muscleGroup) && p.Place.Contains(equipment)).ToList();
				}
				if (String.IsNullOrEmpty(name) && String.IsNullOrEmpty(muscleGroup) && !String.IsNullOrEmpty(equipment))
				{
					trainings = trainings.Where(p => p.Place.Contains(equipment)).ToList();
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
					Training = new Training(),
					Trainings = trainings,
					FilterViewModel = new FilterViewModel(name, muscleGroup, equipment)
				};

				return View(trainingViewModel);
			}
			return BadRequest();
		}

		/// <summary>
		/// Create training 
		/// </summary>
		/// <param name="_training"></param>
		/// <returns>View with created training</returns>
		[HttpPost]
		public async Task<IActionResult> CreateTraining(Training train)
		{
			string? currentUserId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			if (currentUserId!=null)
				await _training.CreateTrainingAsync(train, currentUserId);		
			TempData["AlertMessage"] = "Your training was created successfully!";
			return RedirectToAction("Index");
		}

		/// <summary>
		/// Edit training
		/// </summary>
		/// <param name="id"></param>
		/// <returns>View with form for editing training</returns>
		[HttpGet]
		public async Task<IActionResult> EditTraining(int id)
		{
			string? currentUserId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			if (currentUserId != null)
			{
				Training? training = await _training.GetTrainingAsync(id);
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
		public async Task<IActionResult> EditTrainingPost(Training train)
		{
			await _training.EditTrainingAsync(train, train.Id);
			TempData["AlertMessage"] = "Your training was edited successfully!";
			return RedirectToAction("Index");
		}

		/// <summary>
		/// Delete training
		/// </summary>
		/// <param name="id"></param>
		/// <returns>View with deleted training</returns>
		[HttpGet]
		public async Task<IActionResult> DeleteTraining(int id)
		{
				await _training.DeleteTrainingAsync(id);
				TempData["AlertMessage"] = "Your training was deleted successfully!";
				return RedirectToAction("Index");
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
		public async Task<IActionResult> AddExercise(int id, string name, string muscleGroup, string equipment)
		{
			string? currentUserId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			if (currentUserId != null) { 
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

				Training? training = await _training.GetTrainingAsync(id);
				if (training != null)
				{
					TrainingViewModel trainingViewModel = new TrainingViewModel
					{
						Exercises=exercises,
						Training = training,
						FilterViewModel = new FilterViewModel(name, muscleGroup, equipment)
					};
					return View(trainingViewModel);
				}
				return NotFound();
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
		public async Task<IActionResult> AddExercisePost(Exercise exercise, Training _training)
		{
			await _exercise.AddExerciseAsync(exercise, _training);
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
				await _exercise.DeleteExerciseForTrainingAsync(id, idexer);
				TempData["AlertMessage"] = "Your exercise was deleted successfully!";
				return RedirectToAction("Index");
			}
			return NotFound();
		}

        [HttpGet]
		public async Task<IActionResult> StartTraining(int id)
        {
			string? currentUserId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			if (currentUserId != null)
			{
				Training? training = await _training.GetTrainingAsync(id);
                if (training != null)
				{
					TrainingHistoryViewModel vm = new TrainingHistoryViewModel()
					{
						Training = training,
						TrainingHistory = new TrainingHistory()
					};
					return View(vm);
				}
			}
			return NotFound();
		}

	}
}
