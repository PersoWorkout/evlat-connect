namespace Domain.ClassesSubjects.DTOs
{
    public class ClassSubjectResponse
    {
        public required string ClassId { get; set; }
        public string? ClassName { get; set; }
        public required string SubjectId { get; set; }
        public string? SubjectName { get; set; }
        public required string StartedAt { get; set; }
        public required string FinnishedAt { get; set; }
        public string? Message { get; set; }
    }
}
