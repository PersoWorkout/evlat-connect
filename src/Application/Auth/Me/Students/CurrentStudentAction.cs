using Domain.Abstract;
using Domain.Auth.DTOs;
using Domain.Auth.ValueObjects;
using MediatR;

namespace Application.Auth.Me.Students
{
    public class CurrentStudentAction(ISender sender)
    {
        private readonly ISender _sender = sender;

        public async Task<Result<CurrentStudentResponse>> Execute(TokenValueObject token)
        {
            return await _sender.Send(new CurrentStudentQuery(token));
        }
    }
}
