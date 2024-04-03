using Domain.Auth;
using Domain.Users;
using Domain.Users.ValueObjects;

namespace Application.Auth
{
    public interface IAuthRepository
    {
        Task<Session> GetByToken(string token);
        Task<Session> Create(Session session);
        Task<int> Delete(string token);
        Task<User> GetUserByUsername(string username);
        Task<bool> VerifyCredentials(string username, PasswordValueObject password);
    }
}
