using Domain.Abstract;
using Domain.Subjects;
using MediatR;
using System.Net;

namespace Application.Subjects.GetSubjectById
{
    public class GetSubjectByIdHandler(
        ISubjectRepository repository) : IRequestHandler<GetSubjectByIdQuery, Result<Subject>>
    {
        private readonly ISubjectRepository _repository = repository;

        public async Task<Result<Subject>> Handle(GetSubjectByIdQuery request, CancellationToken cancellationToken)
        {
            var subject = await _repository.GetById(request.Id);
            if (subject is null)
                return Result<Subject>.Failure(
                    SubjectErrors.NotFound(request.Id.ToString()),
                    HttpStatusCode.NotFound);

            return Result<Subject>.Success(subject);            
        }
    }
}
