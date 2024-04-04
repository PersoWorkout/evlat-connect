using Domain.Abstract;
using Domain.Auth.DTOs;
using MediatR;

namespace Application.Auth.Me.Students
{
    public class CurrentStudentAction(ISender sender)
    {
        private readonly ISender _sender = sender;

        public async Task<Result<CurrentStudentResponse>> Execute(CurrentStudentQuery request)
        {
            return await _sender.Send(request);
        }
    }
}
