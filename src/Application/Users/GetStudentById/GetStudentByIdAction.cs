using AutoMapper;
using Domain.Abstract;
using Domain.Auth.DTOs;
using Domain.Users.Errors;
using MediatR;
using System.Net;

namespace Application.Users.GetStudentById
{
    public class GetStudentByIdAction(
        ISender sender, 
        IMapper mapper)
    {
        private readonly ISender _sender = sender;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<CurrentStudentResponse>> Execute(string id)
        {
            if (!Guid.TryParse(id, out var studentId))
                return Result<CurrentStudentResponse>.Failure(
                    UserErrors.StudentNotFound(id),
                    HttpStatusCode.NotFound);

            return await _sender.Send(
                new GetStudentByIdQuery(studentId));
        }
    }
}
