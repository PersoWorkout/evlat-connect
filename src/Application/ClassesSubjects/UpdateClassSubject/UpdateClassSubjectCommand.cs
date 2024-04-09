using Domain.Abstract;
using Domain.ClassesSubjects;
using MediatR;

namespace Application.ClassesSubjects.UpdateClassSubject
{
    public class UpdateClassSubjectCommand
        (Guid classId,
        DateTime startedAt,
        Guid? subjectId = null,
        DateTime? finishedAt = null,
        string? message = null): IRequest<Result<ClassSubject>>
    {
        public Guid ClassId { get; set; } = classId;
        public Guid? SubjectId { get; set; } = subjectId;
        public DateTime StartedDate { get; set; } = startedAt;
        public DateTime? FinishedDate { get; set; } = finishedAt;
        public string? Message { get; set; } = message;
    }
}
