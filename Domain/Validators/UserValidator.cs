using System.Net.Mail;
using Domain.Models;
using FluentValidation;

namespace Domain.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty()
                .WithMessage("You must specify a Username");
            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("You must specify a Password");
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("You must specify an Email Address");
            RuleFor(x => x.Email)
                .Must(BeAValidEmail)
                .WithMessage("You provided an invalid Email Address");
        }

        private bool BeAValidEmail(string email)
        {
            try
            {
                var parsedEmail = new MailAddress(email);
                return parsedEmail.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}