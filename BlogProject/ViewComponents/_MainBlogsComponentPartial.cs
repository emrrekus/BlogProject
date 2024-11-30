using Microsoft.AspNetCore.Mvc;

namespace BlogProject.ViewComponents
{
    public class _MainBlogsComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
