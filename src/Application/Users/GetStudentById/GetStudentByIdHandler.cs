using Application.Classes;
using AutoMapper;
using Domain.Abstract;
using Domain.Auth.DTOs;
using Domain.Classes.DTOs;
using Domain.Users.DTOs;
using Domain.Users.Errors;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.GetStudentById
{
    public class GetStudentByIdHandler(
        IUserRepostory userRepository, 
        IClassRepository classRepository,
        IMapper mapper) : IRequestHandler<GetStudentByIdQuery, Result<CurrentStudentResponse>>
    {
        private readonly IUserRepostory _userRepository = userRepository;
        private readonly IClassRepository _classRepository = classRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<CurrentStudentResponse>> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserById(request.Id);
            if (user is null)
                return Result<CurrentStudentResponse>.Failure(
                    UserErrors.StudentNotFound(request.Id.ToString()),
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
