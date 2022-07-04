using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SportWebApp.Data;
using SportWebApp.Models;
using SportWebApp.ViewModels;

namespace SportWebApp.Controllers
{
    ///<summary>
    ///Controller for registration and authorization
    ///</summary>
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private ApplicationDbContext db;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            db = context;
        }

        /// <summary>
        /// Registaration get
        /// </summary>
        /// <returns>View for registration</returns>
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        /// <summary>
        /// Registration post
        /// </summary>
        /// <param name="model"></param>
        /// <returns>View Home or registration view if user have error</returns>
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new() { Email = model.Email, UserName = model.Email };

                UserProfile profile = new UserProfile { Name = model.Name, UserSurname = model.UserSurname, ApplicationUser = user };
                db.UserProfiles.AddRange(profile);

                Training training = new Training { ApplicationUser = user, Name = "Default", Equipment = "Default", MuscleGroup = "Default", Exercises ="[]"};
                db.Trainings.AddRange(training);

                Exercise exercise = new Exercise { ApplicationUser = user, Name = "Default", Equipment = "Default", MuscleGroup = "Default" };
                db.Exercises.AddRange(exercise);

                UserAvatar avatar = new UserAvatar { ApplicationUser = user, Name = "download", Path = "/Files/2709_R0lVIE5JQyA2MDctNDM.jpg" };
                db.UserAvatars.AddRange(avatar);

                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }

        /// <summary>
        /// Authrization
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns>View for authorization</returns>
        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        /// <summary>
        /// Authorization
        /// </summary>
        /// <param name="model"></param>
        /// <returns>View Home or authorization view if user have error</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result =
                    await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {

                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Неправильний логін і (або) пароль");
                }
            }
            return View(model);
        }

        /// <summary>
        /// Log out
        /// </summary>
        /// <returns>View Home</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
