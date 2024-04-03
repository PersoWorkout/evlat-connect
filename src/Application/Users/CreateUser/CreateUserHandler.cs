using Application.Classes;
using AutoMapper;
using Domain.Abstract;
using Domain.Classes.Errors;
using Domain.Users;
using MediatR;
using System.Net;

namespace Application.Users.CreateUser
{
    public class CreateUserHandler(
        IUserRepostory userRepository,
        IClassRepository classRepository,
        IMapper mapper) : IRequestHandler<CreateUserCommand, Result<User>>
    {
        private readonly IUserRepostory _repository = userRepository;
        private readonly IClassRepository _classRepository = classRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<User>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            User user = _mapper.Map<User>(request);

            user.Username = await _repository
                .GetNewUsername(user.Firstname, user.Lastname);

            if(user.Role == UserRole.Student && request.ClassId.HasValue)
            {
                var classById = await _classRepository.GetById(
                    request.ClassId.Value);

                if(classById is null)
                {
                    return Result<User>.Failure(
                        ClassErrors.ClassNotFound(
                            request.ClassId.Value.ToString()),
                        HttpStatusCode.NotFound);
                }

                user.ClassId = classById.Id;
            }

            await _repository.Create(user);

            return Result<User>.Success(user);
        }
    }
}
