using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportWebApp.Data;
using SportWebApp.Models;
using SportWebApp.ViewModels;
using System.Security.Claims;

namespace SportWebApp.Controllers
{
    public class ProfileInfoController : Controller
    {
        private readonly ApplicationDbContext db;
        IWebHostEnvironment _appEnvironment;
        public ProfileInfoController(ApplicationDbContext context, IWebHostEnvironment appEnvironment)
        {
            db = context;
            _appEnvironment = appEnvironment;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ClaimsPrincipal currentUser = this.User;
            var currentUserID = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            UserProfile? curuser = await db.UserProfiles.FirstOrDefaultAsync(user => user.ApplicationUserId == currentUserID);
            UserAvatar? curuseravatar = await db.UserAvatars.FirstOrDefaultAsync(user => user.ApplicationUserId == currentUserID);
            ProfileInfoViewModel profileinfo = new ProfileInfoViewModel
            {
                UserProfile = curuser,
                UserAvatar = curuseravatar
            };
            if (curuser != null)
            {
                return View(profileinfo);
            }
            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> EditInfo()
        {
            ClaimsPrincipal currentUser = this.User;
            var currentUserID = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;

            UserProfile? curuser = await db.UserProfiles.FirstOrDefaultAsync(user => user.ApplicationUserId == currentUserID);
            UserAvatar? curuseravatar = await db.UserAvatars.FirstOrDefaultAsync(user => user.ApplicationUserId == currentUserID);
            ProfileInfoViewModel profileinfo = new ProfileInfoViewModel
            {
                UserProfile = curuser,
                UserAvatar = curuseravatar
            };
            if (curuser != null)
                return View(curuser);

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserProfile profile)
        {
            ClaimsPrincipal currentUser = this.User;
            var currentUserID = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;

            UserProfile? curuser = await db.UserProfiles.FirstOrDefaultAsync(user => user.ApplicationUserId == currentUserID);
            if (curuser != null)
            {
                curuser.Name = profile.Name;
                curuser.UserSurname = profile.UserSurname;
                curuser.Gender = profile.Gender;
                curuser.Country = profile.Country;
                curuser.Birthday = profile.Birthday;
                curuser.Age = profile.Age;
                curuser.Weight = profile.Weight;
                curuser.Height = profile.Height;
            }
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> EditImageGet()
        {
            ClaimsPrincipal currentUser = this.User;
            var currentUserID = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;

            UserProfile? curuser = await db.UserProfiles.FirstOrDefaultAsync(user => user.ApplicationUserId == currentUserID);
            UserAvatar? curuseravatar = await db.UserAvatars.FirstOrDefaultAsync(user => user.ApplicationUserId == currentUserID);
            ProfileInfoViewModel profileinfo = new ProfileInfoViewModel
            {
                UserProfile = curuser,
                UserAvatar = curuseravatar
            };
            if (curuser != null)
                return View(curuser);

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> EditAvatar(IFormFile uploadedFile)
        {
            ClaimsPrincipal currentUser = this.User;
            var currentUserID = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;

            UserAvatar? curuser = await db.UserAvatars.FirstOrDefaultAsync(user => user.ApplicationUserId == currentUserID);
            if (uploadedFile != null && curuser != null)
            {
                string path = "/Files/" + currentUserID + ".jpg";

                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }
                UserAvatar file = new UserAvatar { Name = currentUserID, Path = path, ApplicationUserId = currentUserID };
                db.UserAvatars.Remove(curuser);
                db.UserAvatars.Add(file);

                await db.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }

    }
}
