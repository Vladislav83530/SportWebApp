using Microsoft.EntityFrameworkCore;
using SportWebApp.Data.Interfaces;
using SportWebApp.Models;

namespace SportWebApp.Data.Repositories
{
    public class TrainingRepository : ITrainingRepository
    {
        ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// Create training
        /// </summary>
        /// <param name="_training"></param>
        /// <param name="currentUserId"></param>
        /// <returns></returns>
        public async Task CreateTrainingAsync(Training _training, string currentUserId)
        {
            Training training = new Training()
            {
                Name = _training.Name,
                Place = _training.Place,
                Notes = _training.Notes,
                MuscleGroup = _training.MuscleGroup,
                ImageUrl = _training.ImageUrl,
                Exercises = "[]",
                ApplicationUserId = currentUserId
            };
            await db.Trainings.AddRangeAsync(training);
            await db.SaveChangesAsync();
        }

        /// <summary>
        /// Delete training
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteTrainingAsync(int id)
        {
            Training? training = await db.Trainings.FirstOrDefaultAsync(x => x.Id == id);
            if (training != null)
                db.Trainings.Remove(training);
            await db.SaveChangesAsync();
        }

        /// <summary>
        /// Edit Training
        /// </summary>
        /// <param name="_training"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task EditTrainingAsync(Training _training, int id)
        {
            Training? training = await GetTrainingAsync(id);
            if (training != null)
            {
                training.Name = _training.Name;
                training.MuscleGroup = _training.MuscleGroup;
                training.Place = _training.Place;
                training.Notes = _training.Notes;
                training.ImageUrl = _training.ImageUrl;
            }
            await db.SaveChangesAsync();
        }

        /// <summary>
        /// Get training 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>training</returns>
        public async Task<Training?> GetTrainingAsync(int id)
        {
            return await db.Trainings.FirstOrDefaultAsync(x => x.Id == id);
        }

        /// <summary>
        /// Get trainings
        /// </summary>
        /// <param name="currentUserId"></param>
        /// <returns>trainings (List<Training>)</returns>
        public async Task<IEnumerable<Training>> GetTrainingsAsync(string currentUserId)
        {
            return await db.Trainings.Where(x => x.ApplicationUserId == currentUserId).ToListAsync();
        }
    }
}
