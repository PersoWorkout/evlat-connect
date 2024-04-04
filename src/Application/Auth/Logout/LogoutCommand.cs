using Domain.Abstract;
using Domain.Auth.ValueObjects;
using MediatR;

namespace Application.Auth.Logout
{
    public class LogoutCommand(TokenValueObject token): IRequest<Result<object>>
    {
        public TokenValueObject Token { get; set; } = token;
    }
}
