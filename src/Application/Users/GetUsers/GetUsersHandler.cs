using Domain.Abstract;
using Domain.Users;
using MediatR;

namespace Application.Users.GetUsers
{
    public class GetUsersHandler(
        IUserRepostory repository) : IRequestHandler<GetUsersQuery, Result<IEnumerable<User>>>
    {
        private readonly IUserRepostory _repository = repository;

        public async Task<Result<IEnumerable<User>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            return Result<IEnumerable<User>>.Success(
                await _repository.GetUsers());
        }
    }
}
