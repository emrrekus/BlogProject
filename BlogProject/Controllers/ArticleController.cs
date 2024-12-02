using BusinessLyaer.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject.Controllers
{
    public class ArticleController : Controller
    {
        private readonly IArticleService _articleService;

        public ArticleController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult ArticleDetail(int id)
        {
            _articleService.TArticleViewCountIncrease(id);

            var value = _articleService.TArticleListWithCategoryAndAppUserByArticleId(id);

            return View(value);
        }
    }
}
