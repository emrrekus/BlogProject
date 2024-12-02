using BlogProject.Models;
using BusinessLyaer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject.Controllers
{
    public class ArticleController : Controller
    {
        private readonly IArticleService _articleService;
        private readonly UserManager<AppUser> _userManager;
        public ArticleController(IArticleService articleService, UserManager<AppUser> userManager)
        {
            _articleService = articleService;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }


        public async Task<IActionResult> ArticleDetail(int id)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
           
            _articleService.TArticleViewCountIncrease(id);

            var value = _articleService.TArticleListWithCategoryAndAppUserByArticleId(id);

            var commentViewModel = new CommentViewModel
            {
             
                ArticleId = id,
                Username = user?.UserName,
                Email = user?.Email,
                UserId = user.Id
            };

            ViewData["CommentViewModel"] = commentViewModel;
            return View(value);
        }
    }
}
