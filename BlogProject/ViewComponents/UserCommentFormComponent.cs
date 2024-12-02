using BlogProject.Models;
using BusinessLyaer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BlogProject.ViewComponents
{
    public class UserCommentFormComponent : ViewComponent
    {
        private readonly Microsoft.AspNetCore.Identity.UserManager<AppUser> _userManager;

        public UserCommentFormComponent(Microsoft.AspNetCore.Identity.UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {

            var user = await _userManager.GetUserAsync(HttpContext.User);

            var model = new CommentViewModel
            {
                UserId = user.Id,
                Username = user?.UserName,
                Email = user?.Email,
                ArticleId = id
            };

            return View(model); 
        }
    }
}




