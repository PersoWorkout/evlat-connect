using Application.Auth;
using Domain.Auth;
using Domain.Auth.ValueObjects;
using Domain.Users;
using Domain.Users.ValueObjects;

namespace Application.UnitTest.Fakers.Repositories
{
    public class FakeAuthRepository : IAuthRepository
    {
        public List<User> Users { get; set; } = [];
        public List<Session> Sessions { get; set; } = [];

        public async Task<Session> Create(Session session)
        {
            Sessions.Add(session);
            return session;
        }

        public Task<int> Delete(string token)
        {
            throw new NotImplementedException();
        }

        public async Task<Session> GetByToken(TokenValueObject token)
        {
            return Sessions.FirstOrDefault(x =>
                Equals(x.Token, token));
        }

        public async Task<User> GetUserByUsername(string username)
        {
            return Users.FirstOrDefault(x => x.Username == username);
        }

        public async Task<bool> VerifyCredentials(string username, PasswordValueObject password)
        {
            var user = Users.FirstOrDefault(x => x.Username == username);

            if (user is null)
                return false;

            return user.Password == password;
        }

        public Task<int> Delete(TokenValueObject token)
        {
            throw new NotImplementedException();
        }

        public void ClearLists()
        {
            Sessions.Clear();
            Users.Clear();
        }
    }
}
