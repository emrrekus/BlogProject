using Microsoft.AspNetCore.Mvc;

namespace BlogProject.Controllers
{
    public class DefaultController : Controller
    {
        
        public IActionResult Index()
        {
            return View();
        }
    }
}
