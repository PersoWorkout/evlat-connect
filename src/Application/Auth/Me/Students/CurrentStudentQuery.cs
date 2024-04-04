using Domain.Abstract;
using Domain.Auth.DTOs;
using Domain.Auth.ValueObjects;
using MediatR;

namespace Application.Auth.Me.Students
{
    public class CurrentStudentQuery(TokenValueObject token) : IRequest<Result<CurrentStudentResponse>>
    {
        public TokenValueObject Token { get; set; } = token;
    }
}
