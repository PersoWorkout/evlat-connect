using Domain.Abstract;
using Domain.Subjects;
using MediatR;
using System.Net;

namespace Application.Subjects.UpdateSubject
{
    public class UpdateSubjectHandler(
        ISubjectRepository repository) : IRequestHandler<UpdateSubjectCommand, Result<Subject>>
    {
        private readonly ISubjectRepository _repository = repository;

        public async Task<Result<Subject>> Handle(UpdateSubjectCommand request, CancellationToken cancellationToken)
        {
            var subject = await _repository.GetById(request.Id);
            if (subject is null)
                return Result<Subject>.Failure(
                    SubjectErrors.NotFound(request.Id.ToString()),
                    HttpStatusCode.NotFound);

            subject.Update(request.Name, request.Description);
            await _repository.Update(subject);

            return Result<Subject>.Success(subject);
        }
    }
}
