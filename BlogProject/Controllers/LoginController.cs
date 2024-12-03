using BlogProject.Models;
using BusinessLyaer.ValidationRules.LoginValidationRules;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using ViewSharedLayer.ViewModels;

namespace BlogProject.Controllers
{
    public class LoginController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;

        public LoginController(SignInManager<AppUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel model)
        {
            ModelState.Clear();
           LoginValidationRules loginValidationRules = new LoginValidationRules();
            ValidationResult validResult = loginValidationRules.Validate(model);

            if(validResult.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, false, true);

                if (result.Succeeded)
                {
                    return RedirectToAction("ArticleList", "Default");
                }
                else
                {
                    return View();
                }
            }
            else
            {
                foreach (var item in validResult.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
                return View();
            }

            
        }

        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("ArticleList", "Default");
        }

    }
}
