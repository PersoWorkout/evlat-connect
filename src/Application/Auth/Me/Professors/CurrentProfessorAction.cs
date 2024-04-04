using Domain.Abstract;
using Domain.Auth.DTOs;
using Domain.Auth.ValueObjects;
using MediatR;

namespace Application.Auth.Me.Professors
{
    public class CurrentProfessorAction(ISender sender)
    {
        private readonly ISender _sender = sender;

        public async Task<Result<CurrentProfessorResponse>> Execute(TokenValueObject token)
        {
            return await _sender.Send(new CurrentProfessorQuery(token));
        }
    }
}
