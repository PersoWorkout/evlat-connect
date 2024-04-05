using Domain.Subjects;
using Domain.Subjects.DTOs;
using FluentValidation;

namespace Application.Subjects.CreateSubject
{
    public class CreateSubjectValidator: AbstractValidator<CreateSubjectRequest>
    {
        public CreateSubjectValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithErrorCode(SubjectErrors.NameEmpty.Code)
                .WithMessage(SubjectErrors.NameEmpty.Description);
        }
    }
}
