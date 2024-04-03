namespace Domain.Classes.DTOs
{
    public class UpdateClassRequest
    {
        public string? Name { get; set; }
        public string? Promotion { get; set; }
        public bool? IsActive { get; set; }
        public int? Type { get; set; }
        public string? ProfessorId { get; set; }
    }
}
