namespace Domain.Competences.DTOs
{
    public class CreateCompetenceRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string SubjectId { get; set; }
    }
}
