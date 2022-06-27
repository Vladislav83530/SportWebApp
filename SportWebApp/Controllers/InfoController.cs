using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportWebApp.Data;
using SportWebApp.Models;

namespace SportWebApp.Controllers
{
    public class InfoController : Controller
    {
        //private readonly ApplicationDbContext db;
        //public InfoController(ApplicationDbContext context)
        //{
        //    db = context;
        //}
        //[HttpPost]
        //public IActionResult AddInfo()
        //{
        //    return View();
        //}
        //[Authorize]
        //[HttpPost]
        //public async Task<IActionResult> Add(UserProfile info)
        //{
          
        //    ApplicationUser curuser = await db.ApplicationUsers.FirstOrDefaultAsync(user => user.UserName == User.Identity.Name);


        //    curuser.Add(info);
            
        //    await db.SaveChangesAsync();
        //    return RedirectToAction("Index", "Cities");
        //}

    }
}
