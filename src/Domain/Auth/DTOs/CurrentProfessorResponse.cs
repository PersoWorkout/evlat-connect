using Domain.Classes.DTOs;

namespace Domain.Auth.DTOs
{
    public class CurrentProfessorResponse
    {
        public required string Id { get; set; }
        public required string Firstname { get; set; }
        public required string Lastname { get; set; }
        public required string Username { get; set; }
        public required string Role { get; set; }
        public required string PhoneNumber { get; set; }
        public List<ClassResponse>? Classes { get; set; }
    }
}
