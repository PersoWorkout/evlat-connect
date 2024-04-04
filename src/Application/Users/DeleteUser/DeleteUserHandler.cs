using Domain.Abstract;
using Domain.Users.Errors;
using MediatR;
using System.Net;

namespace Application.Users.DeleteUser
{
    public class DeleteUserHandler(
        IUserRepostory repository) : IRequestHandler<DeleteUserCommand, Result<object>>
    {
        private readonly IUserRepostory _repository = repository;

        public async Task<Result<object>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetUserById(request.Id);

            if (user is null)
                return Result<object>.Failure(
                    UserErrors.UserNotFound(request.Id.ToString()),
                    HttpStatusCode.NotFound);

            await _repository.Delete(user);

            return Result<object>.Success(user);
        }
    }
}
