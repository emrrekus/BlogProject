using Microsoft.AspNetCore.Mvc;

namespace BlogProject.ViewComponents
{
    public class _MainHeaderComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }

    }
}
