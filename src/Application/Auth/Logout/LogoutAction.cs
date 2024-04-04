using Domain.Abstract;
using Domain.Auth.ValueObjects;
using MediatR;

namespace Application.Auth.Logout
{
    public class LogoutAction(ISender sender)
    {
        private readonly ISender _sender = sender;

        public async Task<Result<object>> Execute(TokenValueObject token)
        {
            return await _sender.Send(new LogoutCommand(token));
        }
    }
}
