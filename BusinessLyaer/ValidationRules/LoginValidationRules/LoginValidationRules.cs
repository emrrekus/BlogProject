using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewSharedLayer.ViewModels;

namespace BusinessLyaer.ValidationRules.LoginValidationRules
{
    public class LoginValidationRules : AbstractValidator<LoginViewModel>
    {

        public LoginValidationRules()
        {
            RuleFor(x => x.Username).NotEmpty().WithMessage("Lütfen Kullanıcı adınızı giriniz.");

            RuleFor(x=>x.Password).NotEmpty().WithMessage("Lütfen Şifrenizi giriniz.");
        }

    }
}
