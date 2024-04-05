namespace Domain.Users.DTOs
{
    public class UpdateUserRequest
    {
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? PasswordConfirmation { get; set; }
        public string? PhoneNumber { get; set; }
        public string? ClassId { get; set; }
    }
}
