using Domain.Abstract;
using Domain.Classes;
using MediatR;

namespace Application.Classes.GetAll
{
    public class GetClassesQuery: 
        IRequest<Result<IEnumerable<Class>>> { }
}
