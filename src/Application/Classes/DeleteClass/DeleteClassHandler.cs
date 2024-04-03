using Domain.Abstract;
using Domain.Classes.Errors;
using MediatR;
using System.Net;

namespace Application.Classes.DeleteClass
{
    public class DeleteClassHandler(IClassRepository repository) : IRequestHandler<DeleteClassCommand, Result<object>>
    {
        private readonly IClassRepository _repository = repository;

        public async Task<Result<object>> Handle(DeleteClassCommand request, CancellationToken cancellationToken)
        {
            var classEntity = await _repository.GetById(request.Id);
            if(classEntity is null)
                return Result<object>.Failure(
                    ClassErrors.ClassNotFound(request.Id.ToString()),
                    HttpStatusCode.NotFound);

            await _repository.Delete(classEntity);

            return Result<object>.Success();
        }
    }
}
