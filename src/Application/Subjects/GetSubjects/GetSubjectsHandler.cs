using Domain.Abstract;
using Domain.Subjects;
using MediatR;

namespace Application.Subjects.GetSubjects
{
    public class GetSubjectsHandler(
        ISubjectRepository repository) : IRequestHandler<GetSubjectsQuery, Result<IEnumerable<Subject>>>
    {
        private readonly ISubjectRepository _repository = repository;

        public async Task<Result<IEnumerable<Subject>>> Handle(GetSubjectsQuery request, CancellationToken cancellationToken)
        {
            return Result<IEnumerable<Subject>>.Success(
                await _repository.GetAll());
        }
    }
}
