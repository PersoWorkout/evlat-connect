using Domain.Abstract;
using Domain.ClassesSubjects;
using MediatR;

namespace Application.ClassesSubjects.CreateClassSubject
{
    public class CreateClassSubjectCommand: IRequest<Result<ClassSubject>>
    {
        public Guid ClassId { get; set; }
        public Guid SubjectId { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime FinishedAt { get; set; }
        public string? Message { get; set; }
    }
}
