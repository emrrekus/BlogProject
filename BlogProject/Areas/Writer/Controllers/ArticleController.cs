using BusinessLyaer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using X.PagedList;
using X.PagedList.Extensions;
using X.PagedList.Mvc;

namespace BlogProject.Areas.Writer.Controllers
{
    [Area("Writer")]
    [Route("Writer/[controller]/[action]/{id?}")]
    [Authorize]
    public class ArticleController : Controller
    {
        private readonly IArticleService _articleService;
        private readonly UserManager<AppUser> _userManager;
        private readonly ICategoryService _categoryService;

        public ArticleController(IArticleService articleService, UserManager<AppUser> userManager, ICategoryService categoryService)
        {
            _articleService = articleService;
            _userManager = userManager;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> ArticleList(int page=1)
        {

            var user = await _userManager.GetUserAsync(User);
           
            var article = await _articleService.TArticleListWithCategoryAndAppUserbyUserIdAsync(user.Id);


            return View(article.ToPagedList(page,8));
        }




        public IActionResult EditArticle(int id)
        {

            var categoryList = _categoryService.TGetAll();
            List<SelectListItem> values1 = (from x in categoryList
                                            select new SelectListItem
                                            {
                                                Text = x.CategoryName,
                                                Value = x.CategoryId.ToString()
                                            }).ToList();
            ViewBag.v1 = values1;

            var article= _articleService.TGetById(id);



            return View(article);  
        }


        [HttpPost]
        public IActionResult EditArticle(Article article)
        {
            if (!ModelState.IsValid)
            {
                _articleService.TUpdate(article);
                return RedirectToAction("ArticleList");
            }
            return View(article);
        }


    }
}
