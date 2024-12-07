using Microsoft.AspNetCore.Mvc;

namespace IdentityMailProject.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
