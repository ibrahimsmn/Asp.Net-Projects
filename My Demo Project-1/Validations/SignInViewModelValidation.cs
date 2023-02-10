using FluentValidation;
using GameNews.Models;

namespace GameNews.Validations
{
    public class SignInViewModelValidation:AbstractValidator<SignInViewModel>
    {
        public SignInViewModelValidation()
        {
            RuleFor(x => x.UserNameOrEmail)
                 .NotEmpty().WithMessage("Boş Bırakmayınız.")
                  .NotNull().WithMessage("Boş Bırakmayınız.");
            RuleFor(x => x.Password)
                  .NotEmpty().WithMessage("Boş Bırakmayınız.")
                  .NotNull().WithMessage("Boş Bırakmayınız.");


        }
    }
}
