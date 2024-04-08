using Domain.Abstract;
using Domain.ClassesSubjects.Errors;
using MediatR;
using System.Net;

namespace Application.ClassesSubjects.DeleteClassSubject
{
    public class DeleteClassSubjectHandler(
        IClassSubjectRepository repository) : IRequestHandler<DeleteClassSubjectCommand, Result<object>>
    {
        private readonly IClassSubjectRepository _repository = repository;

        public async Task<Result<object>> Handle(DeleteClassSubjectCommand request, CancellationToken cancellationToken)
        {
            var classSubject = await _repository.GetByClassAndDate(request.Id, request.StartedDate);
            if (classSubject is null)
                return Result<object>.Failure(
                    ClassSubjectErrors.ClassSubjectNotFound,
                    HttpStatusCode.NotFound);

            await _repository.Delete(classSubject);
            return Result<object>.Success();
        }
    }
}
