using Domain.Users.DTOs;
using Domain.Users.Errors;
using Domain.Users.ValueObjects;
using FluentValidation;

namespace Application.Users.UpdateUser
{
    public class UpdateUserValidator: AbstractValidator<UpdateUserRequest>
    {
        public UpdateUserValidator() 
        {
            RuleFor(x => x.Password)
                .Must(password => PasswordValueObject.Create(password!).IsSucess)
                .When(x => !string.IsNullOrEmpty(x.Password))
                .WithErrorCode(PasswordErrors.Invalid.Code)
                .WithMessage(PasswordErrors.Invalid.Description);

            RuleFor(x => x.PasswordConfirmation)
                .Equal(x => x.Password)
                .When(x => !string.IsNullOrEmpty(x.Password))
                .WithErrorCode(PasswordErrors.InvalidConfirmation.Code)
                .WithMessage(PasswordErrors.InvalidConfirmation.Description);

            RuleFor(x => x.ClassId)
                .Must(classId => Guid.TryParse(classId, out var id))
                .When(x => !string.IsNullOrEmpty(x.ClassId));
        }
    }
}
