using AutoMapper;
using Domain.Abstract;
using Domain.Users;
using MediatR;

namespace Application.Users.CreateUser
{
    public class CreateUserHandler(
        IUserRepostory repository, 
        IMapper mapper) : IRequestHandler<CreateUserCommand, Result<User>>
    {
        private readonly IUserRepostory _repository = repository;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<User>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            User user = _mapper.Map<User>(request);

            user.Username = await _repository
                .GetNewUsername(user.Firstname, user.Lastname);

            await _repository.Create(user);

            return Result<User>.Success(user);
        }
    }
}
