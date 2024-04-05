using Domain.Abstract;
using Domain.Subjects;
using MediatR;

namespace Application.Subjects.UpdateSubject
{
    public class UpdateSubjectCommand: IRequest<Result<Subject>>
    {
        public required Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}
