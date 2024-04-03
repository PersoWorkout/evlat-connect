using Domain.Abstract;
using Domain.Classes;
using MediatR;

namespace Application.Classes.GetClassById
{
    public class GetClassByIdQuery(Guid id): IRequest<Result<Class>>
    {
        public Guid Id { get; set; } = id;
    }
}
