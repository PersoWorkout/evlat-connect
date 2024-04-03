using Domain.Classes;
using Domain.Classes.DTOs;
using Domain.Classes.Errors;
using FluentValidation;

namespace Application.Classes.UpdateClasse
{
    public class UpdateClassValidator: AbstractValidator<UpdateClassRequest>
    {
        public UpdateClassValidator()
        {
            RuleFor(x => x.Type)
                .Must(type =>
                    Enum.IsDefined(typeof(ClassType), type!))
                .When(x => x.Type.HasValue)
                .WithErrorCode(ClassErrors.TypeNotDefined.Code)
                .WithMessage(ClassErrors.TypeNotDefined.Description);

            RuleFor(x => x.ProfessorId)
                .Must(professorId =>
                    Guid.TryParse(professorId, out var id))
                .When(x => !string.IsNullOrEmpty(x.ProfessorId))
                .WithErrorCode(ClassErrors.ProfessorIdInvalid.Code)
                .WithMessage(ClassErrors.ProfessorIdInvalid.Description);
        }
    }
}
