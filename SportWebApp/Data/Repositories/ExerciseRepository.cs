using Microsoft.EntityFrameworkCore;
using SportWebApp.Data.Interfaces;
using SportWebApp.Models;
using System.Text.Json;

namespace SportWebApp.Data.Repositories
{
    public class ExerciseRepository : IExerciseRepository
    {
        ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// Create exercise
        /// </summary>
        /// <param name="_exercise"></param>
        /// <param name="currentUserId"></param>
        /// <returns></returns>
        public async Task CreateExerciseAsync(Exercise _exercise, string currentUserId)
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

        /// <summary>
        /// Delete exercise
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteExerciseAsync(int? id)
        {
            Exercise? exercise = await db.Exercises.FirstOrDefaultAsync(x => x.Id == id);
            if (exercise != null)
                db.Exercises.Remove(exercise);
            await db.SaveChangesAsync();
        }

        /// <summary>
        /// Edit exercise
        /// </summary>
        /// <param name="exercise"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task EditExerciseAsync(Exercise exercise, int id)
        {
            Exercise? _exercise = await GetExerciseAsync(id);
            if (_exercise != null)
            {
                _exercise.Description = exercise.Description;
                _exercise.Equipment = exercise.Equipment;
                _exercise.Weight = exercise.Weight;
                _exercise.Repetition = exercise.Repetition;
                _exercise.MuscleGroup = exercise.MuscleGroup;
                _exercise.Name = exercise.Name;
                _exercise.ImageUrl = exercise.ImageUrl;
            }
            await db.SaveChangesAsync();
        }

        /// <summary>
        /// Get exercise
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Exercise</returns>
        public async Task<Exercise?> GetExerciseAsync(int id)
        {
            return await db.Exercises.FirstOrDefaultAsync(x => x.Id == id);
        }

        /// <summary>
        /// Get exercises
        /// </summary>
        /// <param name="currentUserId"></param>
        /// <returns>Exercises (List<Exercise>)></returns>
        public async Task<IEnumerable<Exercise>> GetExercisesAsync(string currentUserId)
        {
            return await db.Exercises.Where(x => x.ApplicationUserId == currentUserId).ToListAsync();
        }

        /// <summary>
        /// Add exercise to training
        /// </summary>
        /// <param name="_exercise"></param>
        /// <param name="_training"></param>
        /// <returns></returns>
        public async Task AddExerciseAsync(Exercise _exercise, Training _training)
        {
            Training? training = await db.Trainings.FirstOrDefaultAsync(x => x.Id == _training.Id);
            if (training != null && training.Exercises != null)
            {
                List<Exercise>? exercisesList = JsonSerializer.Deserialize<List<Exercise>>(training.Exercises);
                if (exercisesList != null)
                {
                    _exercise.Id = exercisesList.Count + 1;
                    exercisesList.Add(_exercise);
                    string exercises = JsonSerializer.Serialize(exercisesList);
                    training.Exercises = exercises;
                }
            }
            await db.SaveChangesAsync();
        }

        /// <summary>
        /// Delete exercise from training
        /// </summary>
        /// <param name="id"></param>
        /// <param name="idexer"></param>
        /// <returns></returns>
        public async Task DeleteExerciseForTrainingAsync(int? id, int? idexer)
        {
            Training? training = await db.Trainings.FirstOrDefaultAsync(x => x.Id == id);
            if (training!=null&&training.Exercises != null)
            {
                List<Exercise>? exercisesList = JsonSerializer.Deserialize<List<Exercise>>(training.Exercises);
                if (exercisesList != null)
                {
                    Exercise? exercise = exercisesList.FirstOrDefault(x => x.Id == idexer);
                    if(exercise!=null)
                        exercisesList.Remove(exercise);
                    string exercises = JsonSerializer.Serialize(exercisesList);
                    training.Exercises = exercises;
                }
            }
            await db.SaveChangesAsync();
        }
    }
}


