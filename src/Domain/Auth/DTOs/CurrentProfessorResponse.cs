using Domain.Classes.DTOs;
using Domain.Users.DTOs;

namespace Domain.Auth.DTOs
{
    public class CurrentProfessorResponse
    {
        public required UserResponse User { get; set; }
        public List<ClassResponse>? Classes { get; set; }
    }
}
