using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLyaer.ValidationRules.ArticleAddRules
{
    public class ArticleAddRules : AbstractValidator<Article>
    {

        public ArticleAddRules()
        {

            RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Başlık boş bırakılamaz.")
            .Length(5, 100).WithMessage("Başlık en az 5, en fazla 100 karakter olmalıdır.");

            RuleFor(x => x.CategoryId)
                .NotEmpty().WithMessage("Kategori seçilmelidir.");

            RuleFor(x => x.CoverImageUrl)
                .NotEmpty().WithMessage("Kapak görseli URL'si boş olamaz.")
                .Must(url => Uri.TryCreate(url, UriKind.RelativeOrAbsolute, out _)).WithMessage("Geçerli bir URL girilmelidir.");

            RuleFor(x => x.MainImageUrl)
                .NotEmpty().WithMessage("Ana görsel URL'si boş olamaz.")
                .Must(url => Uri.TryCreate(url, UriKind.RelativeOrAbsolute, out _)).WithMessage("Geçerli bir URL girilmelidir.");

            RuleFor(x => x.Detail)
                .NotEmpty().WithMessage("Detay alanı boş bırakılamaz.")
                .MinimumLength(20).WithMessage("Detay en az 20 karakter olmalıdır.");
                      

                   

        }
    }
}
