using BusinessLyaer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject.Controllers
{
    public class NewsLetterController : Controller
    {

        private readonly INewsLetterService _newsLetterService;

        public NewsLetterController(INewsLetterService newsLetterService)
        {
            _newsLetterService = newsLetterService;
        }

        public PartialViewResult AddNewsLetter()
        {
            return PartialView();
        }





        [HttpPost]
        public JsonResult Subscribe(NewsLetter newsLetter) 
        {
            if (newsLetter == null || string.IsNullOrEmpty(newsLetter.Mail))
            {
                return Json(new { success = false, message = "Geçersiz bir e-posta adresi girdiniz." });
            }

            var exMail = _newsLetterService.TExistingNewsLetter(newsLetter);

            if (!exMail)
            {
                return Json(new { success = false, message = "Bu e-posta zaten abone." });
            }

            _newsLetterService.TInsert(newsLetter);

            return Json(new { success = true, message = "Abonelik başarıyla tamamlandı!" });
        }

    }
}
