using BusinessLyaer.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using X.PagedList.Extensions;

namespace BlogProject.Controllers
{
    public class DefaultController : Controller
    {
        private readonly IArticleService _articleService;
        

        public DefaultController(IArticleService articleService)
        {
            _articleService = articleService;
            
        }



        public IActionResult ArticleList(int page =1)
        {

            return View(_articleService.TArticleListWithCategoryAndAppUser().ToPagedList(page,6));
        }

    }
}
