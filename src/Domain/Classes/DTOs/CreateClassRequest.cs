namespace Domain.Classes.DTOs
{
    public class CreateClassRequest
    {
        public required string Promotion {  get; set; }
        public required string Name { get; set; }
        public bool IsActive { get; set; }
        public required int Type {  get; set; }
        public required string ProfessorId { get; set; }
    }
}
