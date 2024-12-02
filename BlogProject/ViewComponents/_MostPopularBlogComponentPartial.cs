using BusinessLyaer.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject.ViewComponents
{
    public class _MostPopularBlogComponentPartial : ViewComponent
    {
        private readonly IArticleService _articleService;

        public _MostPopularBlogComponentPartial(IArticleService articleService)
        {
            _articleService = articleService;
        }

        public IViewComponentResult Invoke()
        {
            var blog= _articleService.TArticleListWithCategoryAndAppUser()
                .OrderByDescending(x=> x.ArticleViewCount)
                .FirstOrDefault();

            return View(blog);
        }
    }
}
