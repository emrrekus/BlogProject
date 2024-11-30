using Microsoft.AspNetCore.Mvc;

namespace BlogProject.ViewComponents
{
    public class _MainPopularPostsComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
