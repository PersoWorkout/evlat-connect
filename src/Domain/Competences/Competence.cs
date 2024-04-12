using Domain.Abstract;
using Domain.CompetencesLinks;
using Domain.Subjects;

namespace Domain.Competences
{
    public class Competence: BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid SubjectId { get; set; }
        public Subject Subject { get; set; }
        public List<CompetenceLink> Links { get; set; }

        public void Update(
            string? name = null,
            string? description = null,
            Guid? subjectId = null)
        {
            if (!string.IsNullOrEmpty(name))
                Name = name;

            if (!string.IsNullOrEmpty(description))
                Description = description;

            if(subjectId.HasValue)
                SubjectId = subjectId.Value;
        }
    }
}
