using BusinessLyaer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject.Areas.Writer.Controllers
{
    [Area("Writer")]
    [Route("Writer/[controller]/[action]/{id?}")]
  
    public class DashboardController : Controller
    {
        private readonly IArticleService _articleService;
        private readonly ICategoryService _categoryService;
        private readonly ICommentService _commentService;
        private readonly INewsLetterService _newsletterService;

        public DashboardController(IArticleService articleService, ICategoryService categoryService, ICommentService commentService, INewsLetterService newsletterService)
        {
            _articleService = articleService;
            _categoryService = categoryService;
            _commentService = commentService;
            _newsletterService = newsletterService;
        }

        public IActionResult Index()
        {
            var result = _articleService.TArticleListWithCategoryAndAppUser().GroupBy(a => a.Category)
                .Select(group => new
                {
                    CategoryName = group.Key.CategoryName,
                    ArticleCount = group.Count()
                })
                .OrderByDescending(x => x.ArticleCount).FirstOrDefault();


            ViewBag.mostPopulerArticle = _articleService.TGetAll().OrderByDescending(x => x.ArticleViewCount).Select(x => x.Title).FirstOrDefault();
            ViewBag.mostPopulerCategory = result.CategoryName;
            ViewBag.commentCount = _commentService.TGetAll().Count();

            var result1 = _articleService.TArticleListWithCategoryAndAppUser().GroupBy(x => x.AppUser)
                  .Select(group => new
                  {
                      Username = group.Key.Name + (" ") + group.Key.Surname,
                      ArticleCount = group.Count()
                  })
                .OrderByDescending(x => x.ArticleCount).FirstOrDefault();

            ViewBag.userMostBlogs = result1.Username;

            ViewBag.articleCount = _articleService.TGetAll().Count();

            ViewBag.newsLetterCount = _newsletterService.TGetAll().Count();


            var result2 = _articleService.TArticleListWithCategoryAndAppUser().SelectMany(x => x.Tags).GroupBy(x => x.TagCloud)
               .Select(group => new
               {
                   TagName = group.Key.Title,
                   TagCount = group.Count()
               }).OrderByDescending(x => x.TagCount).FirstOrDefault();




            ViewBag.mostTags = result2.TagName;
            ViewBag.coutCategories = _categoryService.TGetAll().Count();

            var articles = _articleService.TGetAll();

       
            var categoryGroups = articles
                .Where(a => a.Category != null) 
                .GroupBy(a => a.Category.CategoryName)  
                .Select(g => new { CategoryName = g.Key, Count = g.Count() })
                .OrderByDescending(x => x.Count)
                .ToList();

         
            var categoryNames = categoryGroups.Select(x => x.CategoryName).ToList();
            var categoryCounts = categoryGroups.Select(x => x.Count).ToList();

           
            ViewBag.CategoryNames = categoryNames;
            ViewBag.CategoryCounts = categoryCounts;



            return View();
        }
    }
}
