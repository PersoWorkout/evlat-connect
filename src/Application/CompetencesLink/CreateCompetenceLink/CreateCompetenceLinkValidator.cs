using Domain.CompetencesLinks;
using Domain.CompetencesLinks.DTOs;
using Domain.CompetencesLinks.Errors;
using FluentValidation;

namespace Application.CompetencesLink.CreateCompetenceLink
{
    public class CreateCompetenceLinkValidator:
        AbstractValidator<AddCompetenceLinkRequest>
    {
        public CreateCompetenceLinkValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithErrorCode(CompetenceLinkErrors.NameEmpty.Code)
                .WithMessage(CompetenceLinkErrors.NameEmpty.Description);

            RuleFor(x => x.Path)
                .NotEmpty()
                .WithErrorCode(CompetenceLinkErrors.PathEmpty.Code)
                .WithMessage(CompetenceLinkErrors.PathEmpty.Description);

            RuleFor(x => x.Type)
                .NotEmpty()
                .WithErrorCode(CompetenceLinkErrors.TypeNull.Code)
                .WithMessage(CompetenceLinkErrors.TypeNull.Description)
                .Must(type => 
                    Enum.IsDefined(typeof(LinkType), type))
                .WithErrorCode(CompetenceLinkErrors.TypeInvalid.Code)
                .WithMessage(CompetenceLinkErrors.TypeInvalid.Description);

            RuleFor(x => x.CompetenceId)
                .NotEmpty()
                .WithErrorCode(CompetenceLinkErrors.CompetenceNull.Code)
                .WithMessage(CompetenceLinkErrors.CompetenceNull.Description)
                .Must(competenceId =>
                    Guid.TryParse(competenceId, out var id))
                .WithErrorCode(CompetenceLinkErrors.CompetenceInvalid.Code)
                .WithMessage(CompetenceLinkErrors.CompetenceInvalid.Description);
        }
    }
}
