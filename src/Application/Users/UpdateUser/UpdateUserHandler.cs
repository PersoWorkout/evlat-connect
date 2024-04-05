using Application.Classes;
using Domain.Abstract;
using Domain.Classes.Errors;
using Domain.Users;
using Domain.Users.Errors;
using MediatR;

namespace Application.Users.UpdateUser
{
    public class UpdateUserHandler(
        IUserRepostory userRepository, 
        IClassRepository classRepository) : IRequestHandler<UpdateUserCommand, Result<User>>
    {
        private readonly IUserRepostory _userRepository = userRepository;
        private readonly IClassRepository _classRepository = classRepository;

        public async Task<Result<User>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserById(request.UserId);
            if (user is null)
                return Result<User>.Failure(
                    UserErrors.UserNotFound(request.UserId.ToString()));

            if (request.ClassId.HasValue && user.Role == UserRole.Student)
            {
                var userClass = await _classRepository
                    .GetById(request.ClassId.Value);

                if (userClass is null)
                    return Result<User>.Failure(
                        ClassErrors.ClassNotFound(request.ClassId.Value.ToString()));
            }

            user.Update(
                request.Firstname, 
                request.Lastname, 
                request.Username, 
                request.Password, 
                request.PhoneNumber,
                request.ClassId);

            await _userRepository.Update(user);

            return Result<User>.Success(user);
        }
    }
}
