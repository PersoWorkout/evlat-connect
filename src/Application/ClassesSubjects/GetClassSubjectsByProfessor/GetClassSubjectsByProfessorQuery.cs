using Domain.Abstract;
using Domain.ClassesSubjects;
using MediatR;

namespace Application.ClassesSubjects.GetClassSubjectsByProfessor
{
    public class GetClassSubjectsByProfessorQuery
        (Guid professorId,
        DateTime? from = null,
        DateTime? to = null): IRequest<Result<IEnumerable<ClassSubject>>>
    {
        public Guid ProfessorId { get; set; } = professorId;
        public DateTime? From { get; set; } = from;
        public DateTime? To { get; set; } = to;
    }
}
