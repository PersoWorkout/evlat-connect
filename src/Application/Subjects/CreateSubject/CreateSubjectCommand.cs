using Domain.Abstract;
using Domain.Subjects;
using MediatR;

namespace Application.Subjects.CreateSubject
{
    public class CreateSubjectCommand: IRequest<Result<Subject>>
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
    }
}
