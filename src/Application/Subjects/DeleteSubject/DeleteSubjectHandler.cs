using Domain.Abstract;
using Domain.Subjects;
using MediatR;
using System.Net;

namespace Application.Subjects.DeleteSubject
{
    public class DeleteSubjectHandler(
        ISubjectRepository repository) : IRequestHandler<DeleteSubjectCommand, Result<object>>
    {
        private readonly ISubjectRepository _repository = repository;

        public async Task<Result<object>> Handle(DeleteSubjectCommand request, CancellationToken cancellationToken)
        {
            var subject = await _repository.GetById(request.Id);
            if (subject is null)
                return Result<object>.Failure(
                    SubjectErrors.NotFound(request.Id.ToString()),
                    HttpStatusCode.NotFound);

            await _repository.Delete(subject);

            return Result<object>.Success();
        }
    }
}
