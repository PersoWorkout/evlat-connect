using Application.Classes.GetAll;
using Domain.Abstract;
using Domain.Classes;
using MediatR;

namespace Application.Classes.GetClasses
{
    public class GetClassesHandler(
        IClassRepository repository) : IRequestHandler<GetClassesQuery, Result<IEnumerable<Class>>>
    {
        private readonly IClassRepository _repository = repository;

        public async Task<Result<IEnumerable<Class>>> Handle(GetClassesQuery request, CancellationToken cancellationToken)
        {
            return Result<IEnumerable<Class>>.Success(
                await _repository.GetAll());
        }
    }
}
