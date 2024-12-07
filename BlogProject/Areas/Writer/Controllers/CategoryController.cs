using BusinessLyaer.Abstract;
using BusinessLyaer.ValidationRules.CategoryValidationRules;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;
using X.PagedList.Extensions;
using X.PagedList.Mvc;

namespace BlogProject.Areas.Writer.Controllers
{
    [Area("Writer")]
   [Route("Writer/[controller]/[action]/{id?}")]
 
    public class CategoryController : Controller
    {

        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public IActionResult CategoryList(int page = 1)
        {
            return View(_categoryService.TGetAll().ToPagedList(page, 8));
        }




        public IActionResult DeleteCategory(int id)
        {
            _categoryService.TDelete(id);
            return RedirectToAction("CategoryList");
        }


       
        public IActionResult EditCategory(int id)
        {
            var category = _categoryService.TGetById(id);

            return View(category);
        }

        [HttpPost]
        public IActionResult EditCategory(Category category)
        {
            if (!ModelState.IsValid)
            {
                _categoryService.TUpdate(category);
                return RedirectToAction("CategoryList");
            }
            return View(category);
        }


        public IActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateCategory(Category category)
        {
            ModelState.Clear();
            CategoryValidationRules validRules = new();
            ValidationResult validationResult = validRules.Validate(category);

            if (validationResult.IsValid)
            {
                _categoryService.TInsert(category);
                return RedirectToAction("CategoryList");
            }
            else
            {
                foreach (var item in validationResult.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
                return View();
            }
        }


    }
}
