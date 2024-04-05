namespace Domain.Subjects.DTOs
{
    public class CreateSubjectRequest
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
    }
}
