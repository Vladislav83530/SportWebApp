using Microsoft.EntityFrameworkCore;
using SportWebApp.Data.Interfaces;
using SportWebApp.Models;

namespace SportWebApp.Data.Repositories
{
    public class TrainingHistoryRepository : ITrainingHistoryRepository
    {
        ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// Add training to history
        /// </summary>
        /// <param name="_training"></param>
        /// <param name="currentUserId"></param>
        /// <returns></returns>
        public async Task CreateTrainingAsync(TrainingHistory _training, string currentUserId)
        {
            TrainingHistory training = new TrainingHistory()
            {
                Name = _training.Name,
                Date = _training.Date,
                Duration = _training.Duration,
                Place = _training.Place,
                Calories = _training.Calories,
                Notes = _training.Notes,
                Feeling = _training.Feeling,
                MuscleGroup = _training.MuscleGroup,
                ImageUrl = _training.ImageUrl,
                Exercises = _training.Exercises,
                ApplicationUserId = currentUserId
            };
            await db.TrainingHistory.AddRangeAsync(training);
            await db.SaveChangesAsync();
        }

        /// <summary>
        /// Delete all history
        /// </summary>
        /// <returns></returns>
        public async Task DeleteTrainingsAsync()
        {
            db.TrainingHistory.RemoveRange(db.TrainingHistory);      
            await db.SaveChangesAsync();
        }

        /// <summary>
        /// Get history
        /// </summary>
        /// <param name="currentUserId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<TrainingHistory>> GetTrainingsAsync(string currentUserId)
        {
            return await db.TrainingHistory.Where(x => x.ApplicationUserId == currentUserId).ToListAsync();
        }
    }
}
