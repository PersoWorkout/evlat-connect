namespace Domain.Abstract
{
    public abstract class BaseEntity(Guid id)
    {
        public Guid Id { get; init; } = id;
        public DateTime CreatedAt { get; init; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
    }
}
