namespace Domain.ClassesSubjects.DTOs
{
    public class UpdateClassSubjectRequest
    {
        public string? SubjectId { get; set; }
        public DateTime? FinishedAt { get; set; }
        public string? Message { get; set; }
    }
}
