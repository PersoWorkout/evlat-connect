using Domain.Users;
using Domain.Users.DTOs;
using Domain.Users.Errors;
using Domain.Users.ValueObjects;
using FluentValidation;

namespace Application.Users.CreateUser
{
    public class CreateUserValidator: AbstractValidator<CreateUserRequest>
    {
        public CreateUserValidator() 
        {
            RuleFor(x => x.Firstname)
                .NotEmpty()
                .WithErrorCode(UserErrors.FirstnameEmpty.Code)
                .WithMessage(UserErrors.FirstnameEmpty.Description);

            RuleFor(x => x.Lastname)
                .NotEmpty();

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithErrorCode(PasswordErrors.Empty.Code)
                .WithMessage(PasswordErrors.Empty.Description)
                .Must(password => 
                    PasswordValueObject
                        .Create(password).IsSucess)
                .WithErrorCode(PasswordErrors.Invalid.Code)
                .WithMessage(PasswordErrors.Invalid.Description);

            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .WithErrorCode(PhoneNumberErrors.Empty.Code)
                .WithMessage(PhoneNumberErrors.Empty.Description)
                .Must(phoneNumber =>
                    PhoneNumberValueObject
                        .Create(phoneNumber).IsSucess)
                .WithErrorCode(PhoneNumberErrors.Invalid.Code)
                .WithMessage(PhoneNumberErrors.Invalid.Description);

            RuleFor(x => x.Role)
                .Must(role =>
                    Enum.IsDefined(typeof(UserRole), role))
                .WithErrorCode(UserErrors.RoleNotDefined.Code)
                .WithMessage(UserErrors.RoleNotDefined.Description);
        }

    }
}
