using Domain.Abstract;

namespace Domain.Subjects
{
    public class Subject: BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public void Update(
            string? name = null, 
            string? description = null)
        {
            if (!string.IsNullOrEmpty(name))
                Name = name!;

            if(!string.IsNullOrEmpty(description))
                Description = description!;

            UpdatedAt = DateTime.Now;
        }
    }
}
