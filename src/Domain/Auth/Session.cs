using Domain.Auth.ValueObjects;

namespace Domain.Auth
{
    public class Session(Guid userId)
    {
        public TokenValueObject Token { get; set; } = new TokenValueObject();
        public Guid UserId { get; set; } = userId;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime ExpiresAt { get; set; } = DateTime.Now.AddHours(6);
    }
}
