using Domain.Abstract;
using Domain.Users;
using MediatR;

namespace Application.Users.GetProfessors
{
    public class GetProfessorsHandler(
        IUserRepostory repository) : IRequestHandler<GetProfessorsQuery, Result<IEnumerable<User>>>
    {
        private readonly IUserRepostory _repository = repository;

        public async Task<Result<IEnumerable<User>>> Handle(GetProfessorsQuery request, CancellationToken cancellationToken)
        {
            return Result<IEnumerable<User>>.Success(
                await _repository.GetProfessors());
        }
    }
}
