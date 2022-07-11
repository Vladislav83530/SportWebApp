using SportWebApp.Models;

namespace SportWebApp.Data.Interfaces
{
    public interface IUserProfileRepository
    {
        public Task<UserProfile?> GetCurrentUserAsync(string currentUserId);
        public Task EditUserProfileAsync(UserProfile user, string currentUserId);
    }
}
