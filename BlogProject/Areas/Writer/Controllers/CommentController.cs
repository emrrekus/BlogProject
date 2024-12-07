using BusinessLyaer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject.Areas.Writer.Controllers
{
    [Area("Writer")]
    [Route("Writer/[controller]/[action]/{id?}")]

    public class CommentController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService, UserManager<AppUser> userManager)
        {
            _commentService = commentService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var comments = await _commentService.TCommentListByUserIdAsync(user.Id);
            return View(comments);
        }


        public IActionResult DeleteComment(int id)
        {
            _commentService.TDelete(id);
            return RedirectToAction("Index");
        }


        public IActionResult EditComment(int id)
        {
            var comment = _commentService.TGetById(id);
            return View(comment);
        }

        [HttpPost]
        public IActionResult EditComment(Comment comment)
        {
            if (!ModelState.IsValid)
            {
                _commentService.TUpdate(comment);
                return RedirectToAction("Index");

            }
            return View();
        }
    }
}
