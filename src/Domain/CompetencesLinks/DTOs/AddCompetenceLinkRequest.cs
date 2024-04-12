namespace Domain.CompetencesLinks.DTOs
{
    public class AddCompetenceLinkRequest
    {
        public required string Name { get; set; }
        public required string Path { get; set; }
        public required int Type { get; set; }
        public required string CompetenceId { get; set; }
    }
}
