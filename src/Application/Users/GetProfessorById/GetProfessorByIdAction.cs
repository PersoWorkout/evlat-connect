using Domain.Abstract;
using Domain.Auth.DTOs;
using Domain.Users.Errors;
using MediatR;
using System.Net;

namespace Application.Users.GetProfessorById
{
    public class GetProfessorByIdAction(ISender sender)
    {
        private readonly ISender _sender = sender;

        public async Task<Result<CurrentProfessorResponse>> Execute(string id)
        {
            if (!Guid.TryParse(id, out var studentId))
                return Result<CurrentProfessorResponse>.Failure(
                    UserErrors.ProfessorNotFound(id),
                    HttpStatusCode.NotFound);

            return await _sender.Send(
                new GetProfessorByIdQuery(studentId));
        }
    }
}
