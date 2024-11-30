using Microsoft.AspNetCore.Mvc;

namespace BlogProject.ViewComponents
{
    public class _MainHeadComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
