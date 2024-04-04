using Domain.Abstract;
using Domain.Users.Errors;
using MediatR;
using System.Net;

namespace Application.Users.DeleteUser
{
    public class DeleteUserAction(ISender sender)
    {
        private readonly ISender _sender = sender;

        public async Task<Result<object>> Execute(string id)
        {
            if (!Guid.TryParse(id, out var userId))
                return Result<object>.Failure(
                    UserErrors.UserNotFound(id),
                    HttpStatusCode.NotFound);

            return await _sender.Send(new DeleteUserCommand(userId));
        }
    }
}
