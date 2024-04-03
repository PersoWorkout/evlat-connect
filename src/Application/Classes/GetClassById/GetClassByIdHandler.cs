using Domain.Abstract;
using Domain.Classes;
using Domain.Classes.Errors;
using MediatR;
using System.Net;

namespace Application.Classes.GetClassById
{
    public class GetClassByIdHandler(
        IClassRepository repository) : IRequestHandler<GetClassByIdQuery, Result<Class>>
    {
        private readonly IClassRepository _repository = repository;

        public async Task<Result<Class>> Handle(GetClassByIdQuery request, CancellationToken cancellationToken)
        {
            var classEntity = await _repository.GetById(request.Id);
            if(classEntity is null)
                return Result<Class>.Failure(
                    ClassErrors.ClassNotFound(request.Id.ToString()),
                    HttpStatusCode.NotFound);

            return Result<Class>.Success(classEntity);
        }
    }
}
