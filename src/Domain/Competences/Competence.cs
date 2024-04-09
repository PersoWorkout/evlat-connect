using Domain.Abstract;
using Domain.Subjects;

namespace Domain.Competences
{
    public class Competence: BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Link> Links { get; set; }
        public Guid SubjectId { get; set; }
        public Subject Subject { get; set; }

        public void Update(
            string? name = null, 
            List<Link>? links = null,
            Guid? subjectId = null)
        {
            if (!string.IsNullOrEmpty(name))
                Name = name;

            if (links is not null)
                Links = links;

            if(subjectId.HasValue)
                SubjectId = subjectId.Value;
        }
    }
}
