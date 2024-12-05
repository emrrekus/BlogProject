using BlogProject.Areas.Writer.Models;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject.Areas.Writer.Controllers
{

    [Area("Writer")]
    [Route("Writer/[controller]/[action]/{id?}")]
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public ProfileController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            return View(user);
        }


        [HttpPost]
        public async Task<IActionResult> ChangePassword(string currentPassword, string newPassword, string confirmPassword)
        {
            if (string.IsNullOrEmpty(currentPassword) || string.IsNullOrEmpty(newPassword) || string.IsNullOrEmpty(confirmPassword))
            {
                return Json(new { success = false, message = "Tüm alanları doldurmanız gerekiyor." });
            }
            var user = await _userManager.GetUserAsync(User);

            var checkPassword = await _userManager.CheckPasswordAsync(user, currentPassword);

            if (!checkPassword)
            {
                return Json(new { success = false, message = "Mevcut şifrenizi yanlış girdiniz." });
            }

            if (newPassword != confirmPassword)
            {
                return Json(new { success = false, message = "Yeni şifre ve onay şifresi eşleşmiyor." });
            }


            if (user == null)
            {
                return Json(new { success = false, message = "Kullanıcı bulunamadı." });
            }

            var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
            if (result.Succeeded)
            {
                return Json(new { success = true, message = "Şifreniz başarıyla güncellendi." });
            }

            var errorMessage = string.Join(" ", result.Errors.Select(e => e.Description));
            return Json(new { success = false, message = errorMessage });
        }
        [HttpPost]
        public async Task<IActionResult> UpdateProfile(UserEditViewModel model)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return Json(new { success = false, message = "Kullanıcı bulunamadı." });
                }

                user.Name = model.Name;
                user.Surname = model.Surname;
                user.Description = model.Description;
                user.UserName = model.UserName;
                user.Email = model.Email;

                if (model.ImageUrl != null && model.ImageUrl.Length > 0)
                {
                    string uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/userimages");
                    if (!Directory.Exists(uploadFolder))
                    {
                        Directory.CreateDirectory(uploadFolder);
                    }

                    string fileName = $"{Guid.NewGuid()}{Path.GetExtension(model.ImageUrl.FileName)}";
                    string filePath = Path.Combine(uploadFolder, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.ImageUrl.CopyToAsync(stream);
                    }

                    user.ImageUrl = $"/userimages/{fileName}";
                }

                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return Json(new { success = true, message = "Profil bilgileri başarıyla güncellendi." });
                }

                return Json(new { success = false, message = "Güncelleme sırasında bir hata oluştu." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Bir hata oluştu: {ex.Message}" });
            }
        }

        public PartialViewResult UpdateProfilePopup()
        {
            return PartialView();
        }

        public PartialViewResult PasswordPopup()
        {
            return PartialView();
        }



    }
}
