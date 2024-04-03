namespace Domain.Auth
{
    public class Session
    {
        public string Token { get; set; }
        public Guid UserId {  get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime ExpiredAt { get; set; } = DateTime.Now.AddHours(6);
    }
}
