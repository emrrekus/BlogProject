using BlogProject.Models;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject.ViewComponents
{
    public class UserCommentFormComponent : ViewComponent
    {
        private readonly UserManager<AppUser> _userManager;

        public UserCommentFormComponent(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            var model = new CommentViewModel
            {
                Username = user?.UserName,
                Email = user?.Email
            };

            return View(model);
        }

    }
}
