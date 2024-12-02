using BusinessLyaer.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject.ViewComponents
{
    public class _MainPopularCategoriesComponentPartial : ViewComponent
    {
        private readonly IArticleService _articleService;

        public _MainPopularCategoriesComponentPartial(IArticleService articleService)
        {
            _articleService = articleService;
        }

        public IViewComponentResult Invoke()
        {
            var articles = _articleService.TArticleListWithCategoryAndAppUser();
            var categoryCounts = articles
                .GroupBy(a => a.Category)
                .Select(group => new
                {
                    CategoryName = group.Key.CategoryName,
                    ArticleCount = group.Count()
                })
                .OrderByDescending(x => x.ArticleCount)
                .Take(7)
                .ToList();

            return View(categoryCounts);
        }
    }
}
