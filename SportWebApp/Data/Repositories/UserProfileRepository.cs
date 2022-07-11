using Microsoft.EntityFrameworkCore;
using SportWebApp.Data.Interfaces;
using SportWebApp.Models;

namespace SportWebApp.Data.Repositories
{
    public class UserProfileRepository : IUserProfileRepository
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// Edit User Profile
        /// </summary>
        /// <param name="user"></param>
        /// <param name="currentUserId"></param>
        /// <returns></returns>
        public async Task EditUserProfileAsync(UserProfile user, string? currentUserId )
        {
            UserProfile? currentUser = await GetCurrentUserAsync(currentUserId);
            currentUser.Name = user.Name;
            currentUser.UserSurname = user.UserSurname;
            currentUser.Gender = user.Gender;
            currentUser.Country = user.Country;
            currentUser.Birthday = user.Birthday;
            currentUser.Age = user.Age;
            currentUser.Weight = user.Weight;
            currentUser.Height = user.Height;
            await db.SaveChangesAsync();
        }

        /// <summary>
        /// get current user
        /// </summary>
        /// <param name="currentUserId"></param>
        /// <returns>current user</returns>
        public async Task<UserProfile?> GetCurrentUserAsync(string? currentUserId)
        {
            return await db.UserProfiles.FirstOrDefaultAsync(user => user.ApplicationUserId == currentUserId);
        }
    }
}
