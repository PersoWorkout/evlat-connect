using Application.Classes;
using Application.Users;
using AutoMapper;
using Domain.Abstract;
using Domain.Auth;
using Domain.Auth.DTOs;
using Domain.Classes.DTOs;
using Domain.Users;
using Domain.Users.DTOs;
using Domain.Users.Errors;
using MediatR;
using System.Net;

namespace Application.Auth.Me.Students
{
    public class CurrentStudentHandler(
        IAuthRepository authRepository, 
        IUserRepostory userRepository, 
        IClassRepository classRepository,
        IMapper mapper) : IRequestHandler<CurrentStudentQuery, Result<CurrentStudentResponse>>
    {
        private readonly IAuthRepository _authRepository = authRepository;
        private readonly IUserRepostory _userRepository = userRepository;
        private readonly IClassRepository _classRepository = classRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<CurrentStudentResponse>> Handle(CurrentStudentQuery request, CancellationToken cancellationToken)
        {
            var session = await _authRepository.GetByToken(request.Token);
            if (session is null)
                return Result<CurrentStudentResponse>.Failure(
                    AuthErrors.SessionExpired,
                    HttpStatusCode.Unauthorized);

            var user = await _userRepository.GetStudentById(session.UserId);
            if (user is null || user.Role != UserRole.Student)
                return Result<CurrentStudentResponse>.Failure(
                    UserErrors.StudentNotFound(session.UserId.ToString()),
                    HttpStatusCode.NotFound);

            var result = new CurrentStudentResponse
            {
                User = _mapper.Map<UserResponse>(user)
            };

            if (user.ClassId.HasValue)
            {
                var userClasse = await _classRepository
                    .GetById(user.ClassId.Value);

                if (userClasse is not null)
                    result.Class = _mapper.Map<ClassResponse>(userClasse);
            }

            return Result<CurrentStudentResponse>.Success(result);
        }
    }
}
