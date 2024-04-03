using Domain.Auth.DTOs;
using Domain.Users.Errors;
using Domain.Users.ValueObjects;
using FluentValidation;

namespace Application.Auth.Login
{
    public class LoginValidator: AbstractValidator<LoginRequest>
    {
        public LoginValidator() 
        {
            RuleFor(x => x.Username)
                .NotEmpty()
                .WithErrorCode(PasswordErrors.InvalidConfirmation.Code)
                .WithMessage(PasswordErrors.InvalidConfirmation.Description);

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithErrorCode(PasswordErrors.Empty.Code)
                .WithMessage(PasswordErrors.Empty.Description)
                .Must(password => 
                    PasswordValueObject.Create(password).IsSucess)
                .WithErrorCode(PasswordErrors.Invalid.Code)
                .WithMessage(PasswordErrors.Invalid.Description);

            RuleFor(x => x.PasswordValidation)
                .Equal(x => x.Password)
                .WithErrorCode(PasswordErrors.InvalidConfirmation.Code)
                .WithMessage(PasswordErrors.InvalidConfirmation.Description);
        }
    }
}
