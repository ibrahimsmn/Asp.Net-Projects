using FluentValidation;
using GameNews.Models;
using System.Linq;
using System;
using System.Xml.Serialization;

namespace GameNews.Validations
{
    public class SignUpViewModelValidation:AbstractValidator<SignUpViewModel>
    {

        public SignUpViewModelValidation()
        {
            RuleFor(x => x.Name)
                  .NotEmpty().WithMessage("Boş Bırakmayınız.")
                  .NotNull().WithMessage("Boş Bırakmayınız.")
                  .Must(IsValidName).WithMessage("Lütfen geçerli bir kullanıcı adı giriniz.")
                  .Length(5, 30).WithMessage("En az 5 en fazla 30 karakter giriniz.");
            RuleFor(x => x.Mail)
                .EmailAddress().WithMessage("Lütfen geçerli bir mail adresi giriniz.")
                .Length(10, 200)
                 .NotEmpty().WithMessage("Boş Bırakmayınız.")
                  .NotNull().WithMessage("Boş Bırakmayınız.");
            RuleFor(x => x.Password)
                  .NotEmpty().WithMessage("Boş Bırakmayınız.")
                  .NotNull().WithMessage("Boş Bırakmayınız.")
                  .Length(5, 30).WithMessage("En az 5 en fazla 30 karakter giriniz.");

            RuleFor(x => x.PasswordAgain)
                .Equal(x => x.Password).WithMessage("Şifreler Uyuşmuyor");
                

           




        }
        private bool IsValidName(string name)

        {
            if (name.All(Char.IsNumber))
                {

                return false;
            }

            return true;
        }
    }
}
