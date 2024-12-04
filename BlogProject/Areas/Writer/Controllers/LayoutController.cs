using Microsoft.AspNetCore.Mvc;

namespace BlogProject.Areas.Writer.Controllers
{
    public class LayoutController : Controller
    {
        public IActionResult PartialScript()
        {
            return View();
        }

        public IActionResult PartialFooter()
        {
            return View();
        }

        public IActionResult PartialHead()
        {
            return View();
        }

        public IActionResult PartialNavbar()
        {
            return View();
        }

        public IActionResult PartialHeader()
        {
            return View();
        }

    }
}
