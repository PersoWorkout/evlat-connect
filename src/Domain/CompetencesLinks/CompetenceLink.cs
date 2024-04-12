using Domain.Abstract;
using Domain.Competences;

namespace Domain.CompetencesLinks
{
    public class CompetenceLink(
        string name, 
        string path, 
        LinkType type, 
        Guid competenceId): BaseEntity()
    {
        public string Name { get; set; } = name;
        public string Path { get; set; } = path;
        public LinkType Type { get; set; } = type;
        public Guid CompetenceId { get; set; } = competenceId;

        public Competence Competence { get; set; }

        public void Update(
            string? name = null,
            string? path = null,
            LinkType? type = null)
        {
            if(!string.IsNullOrEmpty(name))
                Name = name;

            if (!string.IsNullOrEmpty(path))
                Path = path;

            if (type.HasValue)
                Type = type.Value;

            UpdatedAt = DateTime.Now;
        }
    }
}
