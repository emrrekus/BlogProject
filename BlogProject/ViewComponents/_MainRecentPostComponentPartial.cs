using BusinessLyaer.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject.ViewComponents
{
    public class _MainRecentPostComponentPartial : ViewComponent
    {
        private readonly IArticleService _articleService;

        public _MainRecentPostComponentPartial(IArticleService articleService)
        {
            _articleService = articleService;
        }

        public IViewComponentResult Invoke()
        {
            var list = _articleService.TArticleListWithCategoryAndAppUser()
                .OrderByDescending(x=> x.ArticleId)
                .Take(3)
                .ToList();

            return View(list);
        }
    }
}
