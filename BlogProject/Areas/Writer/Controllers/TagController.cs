using BusinessLyaer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;
using X.PagedList.Extensions;
using X.PagedList.Mvc;

namespace BlogProject.Areas.Writer.Controllers
{
    [Area("Writer")]
    [Route("Writer/[controller]/[action]/{id?}")]
    [Authorize]
    public class TagController : Controller
    {
        private readonly ITagCloudService _tagCloudService;

        public TagController(ITagCloudService tagCloudService)
        {
            _tagCloudService = tagCloudService;
        }

        public IActionResult TagList(int page =1)
        {
            return View(_tagCloudService.TGetAll().ToPagedList(page,5));
        }

        public IActionResult DeleteTag(int id)
        {
            _tagCloudService.TDelete(id);
            return RedirectToAction("TagList");
        }


        public IActionResult EditTag(int id)
        {
            var tag = _tagCloudService.TGetById(id);
            return View(tag);
        }
        [HttpPost]
        public IActionResult EditTag(TagCloud tag)
        {
            if (!ModelState.IsValid)
            {
                _tagCloudService.TUpdate(tag);
                return RedirectToAction("TagList");
            }
            return View();
        }


        public IActionResult CreateTag()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateTag(TagCloud tag)
        {
            if (!ModelState.IsValid)
            {
                _tagCloudService.TInsert(tag);
                return RedirectToAction("TagList");
            }
            return View();

        }

    }
}
