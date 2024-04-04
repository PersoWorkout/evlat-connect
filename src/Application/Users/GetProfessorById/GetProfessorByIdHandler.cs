using Application.Classes;
using AutoMapper;
using Domain.Abstract;
using Domain.Auth.DTOs;
using Domain.Classes.DTOs;
using Domain.Users.DTOs;
using Domain.Users.Errors;
using Domain.Users;
using MediatR;
using System.Net;

namespace Application.Users.GetProfessorById
{
    public class GetProfessorByIdHandler(
        IUserRepostory userRepository, 
        IClassRepository classRepository, 
        IMapper mapper) : IRequestHandler<GetProfessorByIdQuery, Result<CurrentProfessorResponse>>
    {
        private readonly IUserRepostory _userRepository = userRepository;
        private readonly IClassRepository _classRepository = classRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<CurrentProfessorResponse>> Handle(GetProfessorByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserById(request.Id);
            if (user is null || user.Role != UserRole.Professeur)
                return Result<CurrentProfessorResponse>.Failure(
                    UserErrors.ProfessorNotFound(request.Id.ToString()),
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
