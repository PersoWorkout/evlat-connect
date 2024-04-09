using Domain.Classes;
using Domain.Subjects;

namespace Domain.ClassesSubjects
{
    public class ClassSubject
    {
        public Guid ClassId { get; set; }
        public Guid SubjectId { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime FinishedAt { get; set; }
        public string? Message { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; }

        public Class Class { get; set; }
        public Subject Subject { get; set; }

        public void Update(
            Guid? subjectId = null, 
            DateTime? finishedAt = null,
            string? message = null)
        {
            if (subjectId.HasValue)
                SubjectId = subjectId.Value;

            if (finishedAt.HasValue)
                FinishedAt = finishedAt.Value;

            if (!string.IsNullOrEmpty(message))
                Message = message;

            UpdatedAt = DateTime.Now;
        }
    }
}
