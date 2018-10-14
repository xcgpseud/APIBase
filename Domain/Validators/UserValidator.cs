using System.Net.Mail;
using Domain.Constants;
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
                .WithMessage(UserConstants.MissingUsername);
            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage(UserConstants.MissingPassword);
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage(UserConstants.MissingEmail);
            RuleFor(x => x.Email)
                .Must(BeAValidEmail)
                .WithMessage(UserConstants.InvalidEmail);
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