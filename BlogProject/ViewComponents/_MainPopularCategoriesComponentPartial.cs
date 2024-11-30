using Microsoft.AspNetCore.Mvc;

namespace BlogProject.ViewComponents
{
    public class _MainPopularCategoriesComponentPartial : ViewComponent
    {

        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
