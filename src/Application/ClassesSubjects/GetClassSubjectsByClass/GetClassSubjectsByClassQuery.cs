using Domain.Abstract;
using Domain.ClassesSubjects;
using MediatR;

namespace Application.ClassesSubjects.GetClassSubjectsByClass
{
    public class GetClassSubjectsByClassQuery(
        Guid classId,
        DateTime? from = null,
        DateTime? to = null): IRequest<Result<IEnumerable<ClassSubject>>>
    {
        public Guid ClassId { get; set; } = classId;
        public DateTime? From { get; set; } = from;
        public DateTime? To { get; set; } = to;
    }
}
