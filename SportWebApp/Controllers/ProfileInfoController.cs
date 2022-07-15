using Microsoft.AspNetCore.Mvc;
using SportWebApp.Data.Interfaces;
using SportWebApp.Models;
using SportWebApp.ViewModels;
using System.Security.Claims;

namespace SportWebApp.Controllers
{
    /// <summary>
    /// Edit, Delete, Add Information for User Profile, wathing history
    /// </summary>
    public class ProfileInfoController : Controller
    {
        IWebHostEnvironment _appEnvironment;
        private readonly IUserProfileRepository _userProfile;
        private readonly IUserAvatarRepository _userAvatar;
        public ProfileInfoController(IUserProfileRepository userProfile, IUserAvatarRepository userAvatar, IWebHostEnvironment appEnvironment)
        {
            _userProfile = userProfile;
            _userAvatar = userAvatar;
            _appEnvironment = appEnvironment;
        }

        /// <summary>
        /// User Profile Page
        /// </summary>
        /// <returns>View with information about user and history</returns>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            string? currentUserID = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value; ;
            if (currentUserID != null)
            {
                var curUser = await _userProfile.GetCurrentUserAsync(currentUserID);
                var curUserAvatar = await _userAvatar.GetCurrentUserAvatarAsync(currentUserID);

                ProfileInfoViewModel profileinfo = new ProfileInfoViewModel
                {
                    UserProfile = curUser,
                    UserAvatar = curUserAvatar
                };

                return View(profileinfo);
            }
            return NotFound();
        }

        /// <summary>
        /// Edit Information 
        /// </summary>
        /// <param name="profile"></param>
        /// <returns>View with edited information about user</returns>
        [HttpPost]
        public async Task<IActionResult> Edit(UserProfile profile)
        {
            string? currentUserID = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (currentUserID != null)
            {
                await _userProfile.EditUserProfileAsync(profile, currentUserID);
                TempData["AlertMessage"] = "Your profile info edited successfully!";

                return RedirectToAction("Index");
            }

            return BadRequest();
        }

        /// <summary>
        /// Edit users avatar
        /// </summary>
        /// <param name="uploadedFile"></param>
        /// <returns>View with edited users avatar</returns>
        [HttpPost]
        public async Task<IActionResult> EditAvatar(IFormFile uploadedFile)
        {
            string? currentUserID = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (currentUserID != null)
            {
                var curUserAvatar = await _userAvatar.GetCurrentUserAvatarAsync(currentUserID);
                if (curUserAvatar != null)
                    await _userAvatar.EditUserProfileAsync(uploadedFile, curUserAvatar, currentUserID, _appEnvironment);

                return RedirectToAction("Index");
            }

            return BadRequest();
        }
    }
}
