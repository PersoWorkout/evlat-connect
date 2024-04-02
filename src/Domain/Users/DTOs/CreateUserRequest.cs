namespace Domain.Users.DTOs
{
    public class CreateUserRequest
    {
        public required string Firstname { get; set; }
        public required string Lastname { get; set; }
        public required int Role { get; set; }
        public required string Password { get; set; }
        public required string PhoneNumber { get; set; }
    }
}