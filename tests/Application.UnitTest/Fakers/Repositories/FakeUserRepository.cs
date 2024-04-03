using Application.Users;
using Domain.Users;

namespace Application.UnitTest.Fakers.Repositories
{
    public class FakeUserRepository : IUserRepostory
    {
        public List<User> Users { get; set; } = [];

        public async Task<User> Create(User user)
        {
            Users.Add(user);
            return user;
        }

        public Task<int> Delete(User user)
        {
            throw new NotImplementedException();
        }

        public async Task<string> GetNewUsername(string firstname, string lastname)
        {
            var username = firstname.Trim() + "." + lastname.Trim();
            var counter = Users
                .Where(x => x.Username.StartsWith(username))
                .Count();

            return username + (counter > 0 ? counter : "");
        }

        public async Task<User> GetProfessorById(Guid id)
        {
            return Users.FirstOrDefault(x =>
                x.Role == UserRole.Professeur &&
                x.Id == id);
        }

        public Task<IEnumerable<User>> GetProfessors()
        {
            throw new NotImplementedException();
        }

        public Task<User> GetStudentById(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetStudents()
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUserById(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetUsers()
        {
            throw new NotImplementedException();
        }

        public Task<User> Update(User user)
        {
            throw new NotImplementedException();
        }

        public void ClearUsers()
        {
            Users = [];
        }
    }
}
