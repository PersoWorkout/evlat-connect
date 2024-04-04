using Domain.Abstract;
using Domain.Auth.DTOs;
using Domain.Auth.ValueObjects;
using MediatR;

namespace Application.Auth.Me.Professors
{
    public class CurrentProfessorQuery(TokenValueObject token) : IRequest<Result<CurrentProfessorResponse>>
    {
        public TokenValueObject Token { get; set; } = token;
    }
}
