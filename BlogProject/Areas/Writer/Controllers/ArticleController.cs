using BusinessLyaer.Abstract;
using BusinessLyaer.ValidationRules.ArticleAddRules;
using EntityLayer.Concrete;
using FluentValidation.Results;
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
        private readonly ITagCloudService _tagCloudService;

        public ArticleController(IArticleService articleService, UserManager<AppUser> userManager, ICategoryService categoryService, ITagCloudService tagCloudService)
        {
            _articleService = articleService;
            _userManager = userManager;
            _categoryService = categoryService;
            _tagCloudService = tagCloudService;
        }

        public async Task<IActionResult> ArticleList(int page = 1)
        {
            var user = await _userManager.GetUserAsync(User);
            var article = await _articleService.TArticleListWithCategoryAndAppUserbyUserIdAsync(user.Id);
            return View(article.ToPagedList(page, 8));
        }




        public async Task<IActionResult> EditArticle(int id)
        {
            var article = await _articleService.TArticleWithCategoryAndAppUserByArticleIdAsync(id);
            ViewBag.v1 = GetCategorySelectList();
            ViewBag.selectedTags = article.Tags;
            ViewBag.Tags = _tagCloudService.TGetAll();

            return View(article);
        }


        [HttpPost]
        public async Task<IActionResult> EditArticle(Article article, int[] tagId, IFormFile? CoverImage, IFormFile? MainImage)
        {
            ViewBag.v1 = GetCategorySelectList();

            try
            {

                await _articleService.TUpdateArticleWithTagsAsync(article, tagId, CoverImage, MainImage);
                return RedirectToAction("ArticleList");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Güncelleme sırasında bir hata oluştu: {ex.Message}");
                return View(article);
            }

        }


        public IActionResult CreateArticle()
        {
            ViewBag.v1 = GetCategorySelectList();
            ViewBag.Tags = _tagCloudService.TGetAll();
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> CreateArticle(Article article, int[] tagId, IFormFile CoverImage, IFormFile MainImage)
        {
            ViewBag.v1 = GetCategorySelectList();
            ViewBag.Tags = _tagCloudService.TGetAll();
            try
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);

                await _articleService.TCreateArticle(article, user.Id, tagId, CoverImage, MainImage);

                return RedirectToAction("ArticleList"); // Başarılıysa listeye dön
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Oluşturma sırasında bir hata oluştu: {ex.Message}");
                return View(article); // Hata durumunda aynı View'e model ile dön
            }


        }




        private List<SelectListItem> GetCategorySelectList()
        {
            var categoryList = _categoryService.TGetAll();
            return categoryList.Select(x => new SelectListItem
            {
                Text = x.CategoryName,
                Value = x.CategoryId.ToString()
            }).ToList();
        }



    }
}
