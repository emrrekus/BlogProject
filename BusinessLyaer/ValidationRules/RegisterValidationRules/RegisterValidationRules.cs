using EntityLayer.Concrete;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewSharedLayer.ViewModels;

namespace BusinessLyaer.ValidationRules.RegisterValidationRules
{
    public class RegisterValidationRules : AbstractValidator<RegisterViewModel>
    {
        public RegisterValidationRules(UserManager<AppUser> userManager)
        {
            RuleFor(x => x.UserName)
            .NotEmpty().WithMessage("Lütfen kullanıcı adı giriniz.")
            .MinimumLength(3).WithMessage("Kullanıcı adı en az 3 karakterli olmalıdır.")
            .MaximumLength(20).WithMessage("Kullanıcı adı en fazla 20 karakter olmalıdır.")
            .MustAsync(async (userName, cancellation) =>
            {
                if (string.IsNullOrEmpty(userName)) 
                    return true;
                var existingUsernameUser = await userManager.FindByNameAsync(userName);
                return existingUsernameUser == null; 
            }).WithMessage("Bu kullanıcı adı zaten sistemde kayıtlı.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Lütfen adınızı giriniz.")
                .MinimumLength(3).WithMessage("Adınız en az 3 karakterli olamalıdır.")
                .MaximumLength(20).WithMessage("Adınız en fazla 20 karakteri olmalıdır.");

            RuleFor(x => x.Surname)
                .NotEmpty().WithMessage("Lütfen soyadınızı giriniz.")
                .MinimumLength(3).WithMessage("Soyadınız en az 3 karakterli olamalıdır.")
                .MaximumLength(20).WithMessage("Soyadınız en fazla 20 karakteri olmalıdır.");
            RuleFor(x => x.Email)
                    .NotEmpty().WithMessage("Lütfen mail adresinizi giriniz.")
                    .EmailAddress().WithMessage("Lütfen geçerli bir mail adresi giriniz.")
                    .MustAsync(async (email, cancellation) =>
                    {
                        if (string.IsNullOrEmpty(email))
                            return true;
                        var existingEmailUser = await userManager.FindByEmailAsync(email);
                        return existingEmailUser == null;
                    }).WithMessage("Bu e-posta adresi zaten sistemde kayıtlı.");

            RuleFor(x => x.Password)
       .NotEmpty().WithMessage("Lütfen şifrenizi giriniz.")
       .MinimumLength(6).WithMessage("Şifre en az 6 karakter uzunluğunda olmalıdır.")
       .Matches("[A-Z]").WithMessage("Şifre en az bir büyük harf içermelidir.")
       .Matches("[a-z]").WithMessage("Şifre en az bir küçük harf içermelidir.")
       .Matches("[0-9]").WithMessage("Şifre en az bir rakam içermelidir.")
       .Matches(@"[@$!%*?&.]").WithMessage("Şifre en az bir özel karakter içermelidir.");


        }
    }
}
