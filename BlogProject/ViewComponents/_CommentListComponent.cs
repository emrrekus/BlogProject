using BusinessLyaer.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject.ViewComponents
{
    public class _CommentListComponent : ViewComponent
    {
     private readonly IArticleService _articleService;

        public _CommentListComponent(IArticleService articleService)
        {
            _articleService = articleService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var comments = await _articleService.TCommentListWithArticleId(id);
            return View(comments);
        }
    }
}
