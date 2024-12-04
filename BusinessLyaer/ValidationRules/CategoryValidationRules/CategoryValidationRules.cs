using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLyaer.ValidationRules.CategoryValidationRules
{
    public class CategoryValidationRules : AbstractValidator<Category>
    {

        public CategoryValidationRules()
        {
            RuleFor(x => x.CategoryName)
            .NotEmpty().WithMessage("Lütfen kategori adını boş bırakmayınız!")
            .MinimumLength(3).WithMessage("En az 3 karakter veri girişi yapınız!")
            .MaximumLength(20).WithMessage("En fazla 20 karakter veri girişi yapınız!");

        }
    }
}
