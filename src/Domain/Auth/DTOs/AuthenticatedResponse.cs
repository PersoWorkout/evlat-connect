namespace Domain.Auth.DTOs
{
    public class AuthenticatedResponse
    {
        public required string Token { get; set; }
        public required DateTime ExpiresAt { get; set; }
    }
}
