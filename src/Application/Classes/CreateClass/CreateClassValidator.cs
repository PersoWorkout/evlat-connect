using Domain.Classes;
using Domain.Classes.DTOs;
using Domain.Classes.Errors;
using FluentValidation;

namespace Application.Classes.CreateClass
{
    public class CreateClassValidator: AbstractValidator<CreateClassRequest>
    {
        public CreateClassValidator() 
        {
            RuleFor(x => x.Name)
                    .NotEmpty()
                    .WithErrorCode(ClassErrors.NameEmpty.Code)
                    .WithMessage(ClassErrors.NameEmpty.Description);

            RuleFor(x => x.Promotion)
                    .NotEmpty()
                    .WithErrorCode(ClassErrors.PromotionEmpty.Code)
                    .WithMessage(ClassErrors.PromotionEmpty.Description);

            RuleFor(x => x.Type)
                .Must(type => Enum.IsDefined(typeof(ClassType), type))
                .WithErrorCode(ClassErrors.TypeNotDefined.Code)
                .WithMessage(ClassErrors.TypeNotDefined.Description);

            RuleFor(x => x.ProfessorId)
                .NotEmpty()
                .WithErrorCode(ClassErrors.ProfessorIdEmpty.Code)
                .WithMessage(ClassErrors.ProfessorIdEmpty.Description)
                .Must(professorId => 
                    Guid.TryParse(professorId, out Guid value))
                .WithErrorCode(ClassErrors.ProfessorIdInvalid.Code)
                .WithMessage(ClassErrors.ProfessorIdInvalid.Description);
        }
    }
}
