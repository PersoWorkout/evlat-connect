using Domain.Abstract;
using Domain.Auth.DTOs;
using MediatR;

namespace Application.Users.GetProfessorById
{
    public class GetProfessorByIdQuery(Guid id): IRequest<Result<CurrentProfessorResponse>>
    {
        public Guid Id { get; set; } = id;
    }
}
