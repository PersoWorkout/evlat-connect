using Domain.Competences.DTOs;
using Domain.Competences.Errors;
using FluentValidation;

namespace Application.Competences.UpdateCompetence
{
    public class UpdateCompetenceValidator: AbstractValidator<UpdateCompetenceRequest>
    {
        public UpdateCompetenceValidator()
        {
            RuleFor(x => x.SubjectId)
                .Must(x => Guid.TryParse(x, out var id))
                .When(x => !string.IsNullOrEmpty(x.SubjectId))
                .WithErrorCode(CompetenceErrors.SubjectIdInvalid.Code)
                .WithMessage(CompetenceErrors.SubjectIdInvalid.Description);

        }
    }
}
