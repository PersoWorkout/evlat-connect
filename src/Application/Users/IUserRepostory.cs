using Domain.Users;

namespace Application.Users
{
    public interface IUserRepostory
    {
        Task<IEnumerable<User>> GetUsers();
        Task<IEnumerable<User>> GetStudents();
        Task<IEnumerable<User>> GetProfessors();
        Task<User> Create(User user);
        Task<User> GetUserById(Guid Id);
        Task<User> GetStudentById(Guid Id);
        Task<User> GetProfessorById(Guid Id);
        Task<string> GetNewUsername(string firstname, string lastname);
        Task<User> Update(User user);
        Task<int> Delete(User user);
    }
}
