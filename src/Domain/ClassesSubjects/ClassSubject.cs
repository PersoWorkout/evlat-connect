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
            DateTime? startedAt = null, 
            DateTime? finishedAt = null,
            string? message = null)
        {
            if (startedAt.HasValue)
                StartedAt = startedAt.Value;

            if (finishedAt.HasValue)
                FinishedAt = finishedAt.Value;

            if (!string.IsNullOrEmpty(message))
                Message = message;

            UpdatedAt = DateTime.Now;
        }
    }
}
