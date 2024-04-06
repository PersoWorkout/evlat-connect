using Domain.ClassesSubjects.DTOs;
using Domain.ClassesSubjects.Errors;
using FluentValidation;

namespace Application.ClassesSubjects.CreateClassSubject
{
    public class CreateClassSubjectValidator: AbstractValidator<CreateClassSubjectRequest>
    {
        public CreateClassSubjectValidator()
        {
            RuleFor(x => x.StartedAt)
                .NotNull()
                .WithErrorCode(ClassSubjectErrors.StartingDateNull.Code)
                .WithMessage(ClassSubjectErrors.StartingDateNull.Description);

            RuleFor(x => x.FinishedAt)
                .NotNull()
                .WithErrorCode(ClassSubjectErrors.FinishDateNull.Code)
                .WithMessage(ClassSubjectErrors.FinishDateNull.Description)
                .GreaterThan(x => x.StartedAt)
                .WithErrorCode(ClassSubjectErrors.FinishDateInvalid.Code)
                .WithMessage(ClassSubjectErrors.FinishDateInvalid.Description);
        }
    }
}
