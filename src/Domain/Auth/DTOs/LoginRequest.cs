namespace Domain.Auth.DTOs
{
    public class LoginRequest
    {
        public required string Username; 
        public required string Password;
        public required string PasswordValidation;
    }
}
