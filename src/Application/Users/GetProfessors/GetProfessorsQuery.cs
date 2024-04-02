using Domain.Abstract;
using Domain.Users;
using MediatR;

namespace Application.Users.GetProfessors
{
    public class GetProfessorsQuery: IRequest<Result<IEnumerable<User>>>
    {
    }
}
