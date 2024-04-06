namespace Domain.ClassesSubjects.DTOs
{
    public class CreateClassSubjectRequest
    {
        public required DateTime StartedAt { get; set; }
        public required DateTime FinishedAt { get; set; }
        public string? Message { get; set; }
    }
}
