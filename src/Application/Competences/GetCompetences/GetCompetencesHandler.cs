using Domain.Abstract;
using Domain.Competences;
using MediatR;

namespace Application.Competences.GetCompetences
{
    public class GetCompetencesHandler : IRequestHandler<GetCompetencesQuery, Result<IEnumerable<Competence>>>
    {
        private readonly ICompetenceRepository _repository;

        public GetCompetencesHandler(ICompetenceRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<IEnumerable<Competence>>> Handle(GetCompetencesQuery request, CancellationToken cancellationToken)
        {
            return Result<IEnumerable<Competence>>.Success(
                await _repository.GetAll());
        }
    }
}
