using Microsoft.EntityFrameworkCore;
using SportWebApp.Data.Interfaces;
using SportWebApp.Models;

namespace SportWebApp.Data.Repositories
{
    public class UserAvatarRepository : IUserAvatarRepository
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        
        /// <summary>
        /// Edit User Profile Async
        /// </summary>
        /// <param name="uploadedFile"></param>
        /// <param name="currentUser"></param>
        /// <param name="currentUserId"></param>
        /// <param name="appEnvironment"></param>
        /// <returns></returns>
        public async Task EditUserProfileAsync(IFormFile uploadedFile, UserAvatar currentUser, string currentUserId, IWebHostEnvironment appEnvironment)
        {
            if (uploadedFile != null && currentUser != null)
            {
                string path = "/Files/" + currentUserId + ".jpg";
                
                using (var fileStream = new FileStream(appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }
                UserAvatar file = new UserAvatar { Name = currentUserId, Path = path, ApplicationUserId = currentUserId };
                db.UserAvatars.Remove(currentUser);
                db.UserAvatars.Add(file);
                await db.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Get current user avatar 
        /// </summary>
        /// <param name="currentUserId"></param>
        /// <returns>User avatar (UserAvatar)</returns>
        public async Task<UserAvatar?> GetCurrentUserAvatarAsync(string currentUserId)
        {
            return await db.UserAvatars.FirstOrDefaultAsync(user => user.ApplicationUserId == currentUserId);
        }
    }
}
