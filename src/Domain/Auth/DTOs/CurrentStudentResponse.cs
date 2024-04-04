using Domain.Classes.DTOs;
using Domain.Users.DTOs;

namespace Domain.Auth.DTOs
{
    public class CurrentStudentResponse
    {
        public required UserResponse User { get; set; }
        public ClassResponse? Class { get; set; }
    }
}
