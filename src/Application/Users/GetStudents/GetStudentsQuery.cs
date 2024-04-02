using Domain.Abstract;
using Domain.Users;
using MediatR;

namespace Application.Users.GetStudents
{
    public class GetStudentsQuery: IRequest<Result<IEnumerable<User>>>
    {
    }
}
