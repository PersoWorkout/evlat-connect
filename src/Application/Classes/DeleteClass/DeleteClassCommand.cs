using Domain.Abstract;
using MediatR;

namespace Application.Classes.DeleteClass
{
    public class DeleteClassCommand(Guid id): IRequest<Result<object>>
    {
        public Guid Id { get; set; } = id;
    }
}
