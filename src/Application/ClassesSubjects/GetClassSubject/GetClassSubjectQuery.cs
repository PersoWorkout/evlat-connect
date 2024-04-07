using Domain.Abstract;
using Domain.ClassesSubjects;
using MediatR;

namespace Application.ClassesSubjects.GetClassSubject
{
    public class GetClassSubjectQuery(
        Guid classId,
        DateTime startedDate): IRequest<Result<ClassSubject>>
    {
        public Guid ClassId { get; set; } = classId;
        public DateTime StartedDate { get; set; } = startedDate;
    }
}
