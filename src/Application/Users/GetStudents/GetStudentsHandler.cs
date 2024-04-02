using Domain.Abstract;
using Domain.Users;
using MediatR;

namespace Application.Users.GetStudents
{
    public class GetStudentsHandler(
        IUserRepostory repository) : IRequestHandler<GetStudentsQuery, Result<IEnumerable<User>>>
    {
        private readonly IUserRepostory _repository = repository;

        public async Task<Result<IEnumerable<User>>> Handle(GetStudentsQuery request, CancellationToken cancellationToken)
        {
            return Result<IEnumerable<User>>.Success(
                await _repository.GetStudents());
        }
    }
}
