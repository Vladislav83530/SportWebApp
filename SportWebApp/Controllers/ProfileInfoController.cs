using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportWebApp.Data;
using SportWebApp.Models;
using System.Security.Claims;

namespace SportWebApp.Controllers
{
    public class ProfileInfoController : Controller
    {
        private readonly ApplicationDbContext db;
        public ProfileInfoController(ApplicationDbContext context)
        {
            db = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ClaimsPrincipal currentUser = this.User;
            var currentUserID = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            UserProfile? curuser = await db.UserProfiles.FirstOrDefaultAsync(user => user.ApplicationUserId == currentUserID); 

            if (curuser != null)
            {
                return View(curuser);
            }
            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> EditInfo()
        {
            //if (id != null)
            //{
            ClaimsPrincipal currentUser = this.User;
            var currentUserID = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;

            UserProfile? curuser = await db.UserProfiles.FirstOrDefaultAsync(user => user.ApplicationUserId == currentUserID);


            if (curuser != null)
            {



                return View(curuser);
            }
           // }
            return NotFound();

        }

    }
}
