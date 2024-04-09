namespace Domain.Competences.DTOs
{
    public class UpdateCompetenceRequest
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public List<Link>? Links { get; set; }
        public string? SubjectId { get; set; }
    }
}
