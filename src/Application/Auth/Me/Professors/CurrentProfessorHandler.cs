using Application.Classes;
using Application.Users;
using AutoMapper;
using Domain.Abstract;
using Domain.Auth;
using Domain.Auth.DTOs;
using Domain.Classes.DTOs;
using Domain.Users.DTOs;
using Domain.Users.Errors;
using Domain.Users;
using MediatR;
using System.Net;

namespace Application.Auth.Me.Professors
{
    public class CurrentProfessorHandler(
        IAuthRepository authRepository,
        IUserRepostory userRepository,
        IClassRepository classRepository,
        IMapper mapper): IRequestHandler<CurrentProfessorQuery, Result<CurrentProfessorResponse>>
    {
        private readonly IAuthRepository _authRepository = authRepository;
        private readonly IUserRepostory _userRepository = userRepository;
        private readonly IClassRepository _classRepository = classRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<CurrentProfessorResponse>> Handle(CurrentProfessorQuery request, CancellationToken cancellationToken)
        {
            var session = await _authRepository.GetByToken(request.Token);
            if (session is null)
                return Result<CurrentProfessorResponse>.Failure(
                    AuthErrors.SessionExpired,
                    HttpStatusCode.Unauthorized);

            var user = await _userRepository.GetProfessorById(session.UserId);
            if (user is null || user.Role != UserRole.Professeur)
                return Result<CurrentProfessorResponse>.Failure(
                    UserErrors.ProfessorNotFound(session.UserId.ToString()),
                    HttpStatusCode.NotFound);

            var classes = await _classRepository.GetByProfessor(user.Id);

            var result = new CurrentProfessorResponse
            {
                User = _mapper.Map<UserResponse>(user),
                Classes = classes
                    .Select(_mapper.Map<ClassResponse>)
                    .ToList()
            };

            return Result<CurrentProfessorResponse>.Success(result);
        }
    }
}
