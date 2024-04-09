using Domain.Competences.DTOs;
using Domain.Competences.Errors;
using FluentValidation;

namespace Application.Competences.CreateCompetence
{
    public class CreateCompetenceValidator: AbstractValidator<CreateCompetenceRequest>
    {
        public CreateCompetenceValidator()
        { 
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithErrorCode(CompetenceErrors.NameEmpty.Code)
                .WithMessage(CompetenceErrors.NameEmpty.Description);

            RuleFor(x => x.SubjectId)
                .NotEmpty()
                .WithErrorCode(CompetenceErrors.SubjectIdNull.Code)
                .WithMessage(CompetenceErrors.SubjectIdNull.Description)
                .Must(subjectId => 
                    Guid.TryParse(subjectId, out Guid id))
                .WithErrorCode(CompetenceErrors.SubjectIdInvalid.Code)
                .WithMessage(CompetenceErrors.SubjectIdInvalid.Description);

        }
    }
}
