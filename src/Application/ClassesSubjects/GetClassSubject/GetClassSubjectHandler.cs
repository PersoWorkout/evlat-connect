using Domain.Abstract;
using Domain.ClassesSubjects;
using Domain.ClassesSubjects.Errors;
using MediatR;

namespace Application.ClassesSubjects.GetClassSubject
{
    public class GetClassSubjectHandler : IRequestHandler<GetClassSubjectQuery, Result<ClassSubject>>
    {
        private readonly IClassSubjectRepository _repository;

        public GetClassSubjectHandler(IClassSubjectRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<ClassSubject>> Handle(GetClassSubjectQuery request, CancellationToken cancellationToken)
        {
            var classSubject = await _repository.GetByClassAndDate(
                request.ClassId, 
                request.StartedDate);

            if (classSubject is null)
                return Result<ClassSubject>.Failure(
                    ClassSubjectErrors.ClassSubjectNotFound);

            return Result<ClassSubject>.Success(classSubject);
        }
    }
}
