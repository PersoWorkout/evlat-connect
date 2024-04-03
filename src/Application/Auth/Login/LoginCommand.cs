using Domain.Abstract;
using Domain.Auth;
using Domain.Users.ValueObjects;
using MediatR;

namespace Application.Auth.Login
{
    public class LoginCommand: IRequest<Result<Session>>
    {
        public required string Username { get; set; }
        public PasswordValueObject Password { get; set; }
    }
}
