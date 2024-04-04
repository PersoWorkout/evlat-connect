using Domain.Abstract;
using Domain.Auth.DTOs;
using Domain.Users;
using MediatR;

namespace Application.Users.GetStudentById
{
    public class GetStudentByIdQuery(Guid id): IRequest<Result<CurrentStudentResponse>>
    {
        public Guid Id { get; set; } = id;
    }
}
