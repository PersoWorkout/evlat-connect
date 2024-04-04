using Domain.Auth;
using Domain.Auth.ValueObjects;
using Domain.Users;
using Domain.Users.ValueObjects;

namespace Application.Auth
{
    public interface IAuthRepository
    {
        Task<Session> GetByToken(TokenValueObject token);
        Task<Session> Create(Session session);
        Task<int> Delete(Session session);
        Task<User> GetUserByUsername(string username);
        Task<bool> VerifyCredentials(string username, PasswordValueObject password);
    }
}
