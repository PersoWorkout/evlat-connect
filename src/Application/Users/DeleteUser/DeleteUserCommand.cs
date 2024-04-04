using Domain.Abstract;
using MediatR;

namespace Application.Users.DeleteUser
{
    public class DeleteUserCommand(Guid id): IRequest<Result<object>>
    {
        public Guid Id { get; set; } = id;
    }
}
