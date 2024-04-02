using Domain.Abstract;
using Domain.Users;

namespace Domain.Classes
{
    public class Class: BaseEntity
    {
        public string Promotion {  get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public ClassType Type { get; set; }
        public Guid ProfessorId { get; set; }
        public User Professor { get; set; }

        public void Update(
            string? promotion = null, 
            string? name = null, 
            bool? isActive = null,
            ClassType? type = null, 
            Guid? professorId = null) 
        {
            if (!string.IsNullOrEmpty(promotion)) 
                Promotion = promotion;

            if(!string.IsNullOrEmpty(name))
                Name = name;

            if(type.HasValue)
                Type = type.Value;
            
            if(isActive.HasValue)
                IsActive = isActive.Value;

            if(professorId.HasValue)
                ProfessorId = professorId.Value;

            UpdatedAt = DateTime.Now;
        }
    }
}
