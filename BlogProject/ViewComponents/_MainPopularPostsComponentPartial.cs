using BusinessLyaer.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject.ViewComponents
{
    public class _MainPopularPostsComponentPartial : ViewComponent
    {
        private readonly IArticleService _articleService;

        public _MainPopularPostsComponentPartial(IArticleService articleService)
        {
            _articleService = articleService;
        }

        public IViewComponentResult Invoke()
        {
            var list = _articleService.TArticleListWithCategoryAndAppUser()
              .OrderByDescending(x => x.ArticleViewCount)
              .Take(3)
              .ToList();
            return View(list);
        }
    }
}
