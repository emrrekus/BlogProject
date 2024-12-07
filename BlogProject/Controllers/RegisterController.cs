using BlogProject.Models;
using BusinessLyaer.ValidationRules.LoginValidationRules;
using BusinessLyaer.ValidationRules.RegisterValidationRules;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ViewSharedLayer.ViewModels;

namespace BlogProject.Controllers
{
    [AllowAnonymous]
    public class RegisterController : Controller
    {

        private readonly UserManager<AppUser> _userManager;

        public RegisterController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(RegisterViewModel model)
        {
            ModelState.Clear();
            RegisterValidationRules validationRules = new RegisterValidationRules(_userManager);
            ValidationResult validResult = await validationRules.ValidateAsync(model);

            if (!validResult.IsValid)
            {
                foreach (var error in validResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return View(model);
            }


            AppUser appUser = new AppUser
            {
                Description = string.Empty,
                Name = model.Name,
                Surname = model.Surname,
                Email = model.Email,
                UserName = model.UserName,
                ImageUrl = "test"
            };

            var result = await _userManager.CreateAsync(appUser, model.Password);

            return result.Succeeded ? RedirectToAction("Index", "Login") : View(model);

        }
    }
}




