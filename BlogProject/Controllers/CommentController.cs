using BlogProject.Models;
using BusinessLyaer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject.Controllers
{
    [AllowAnonymous]
    public class CommentController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ICommentService _commentService;
        private readonly IArticleService _articleService;

        public CommentController(UserManager<AppUser> userManager, ICommentService commentService, IArticleService articleService)
        {
            _userManager = userManager;
            _commentService = commentService;
            _articleService = articleService;
        }

        public async Task<PartialViewResult> AddComment(CommentViewModel commentViewModel)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            var model = new CommentViewModel
            {
              
                UserId = commentViewModel.UserId,
                Username = commentViewModel.Username,
                Email = commentViewModel.Email,
                ArticleId = commentViewModel.ArticleId
            };



            return PartialView(model);
        }

        [HttpPost]
        public JsonResult PartialAddComment(Comment comment)
        {
            
            try
            {
                comment.Article = _articleService.TArticleListWithCategoryAndAppUserByArticleId(comment.ArticleId);
                comment.AppUser = _userManager.GetUserAsync(HttpContext.User).Result;
                comment.CreatedDate = DateTime.Now;
                comment.Status = true;

                _commentService.TInsert(comment);
                return Json(new { success = true, message = "Yorum başarıyla eklendi!" });
                           }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Yorum eklenirken bir hata oluştu: " + ex.Message });
            }
        }


    }
}
