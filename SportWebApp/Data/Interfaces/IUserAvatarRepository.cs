using SportWebApp.Models;

namespace SportWebApp.Data.Interfaces
{
    public interface IUserAvatarRepository
    {
        public Task<UserAvatar?> GetCurrentUserAvatarAsync(string currentUserId);
        public Task EditUserProfileAsync(IFormFile uploadedFile, UserAvatar currentUser, string currentUserId, IWebHostEnvironment appEnvironment);
    }
}
