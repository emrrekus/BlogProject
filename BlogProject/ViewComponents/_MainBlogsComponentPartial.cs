using BusinessLyaer.Abstract;
using Microsoft.AspNetCore.Mvc;
using X.PagedList.Extensions;

namespace BlogProject.ViewComponents
{
    public class _MainBlogsComponentPartial : ViewComponent
    {
        private readonly IArticleService _articleService;

        public _MainBlogsComponentPartial(IArticleService articleService)
        {
            _articleService = articleService;
        }

        public IViewComponentResult Invoke(int page =1)
        {
            return View(_articleService.TArticleListWithCategoryAndAppUser().ToPagedList(page,6));
        }
    }
}
