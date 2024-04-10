using Domain.Abstract;
using Domain.Competences;
using Domain.Competences.Errors;
using MediatR;
using System.Net;

namespace Application.Competences.GetCompetenceById
{
    public class GetCompetenceByIdHandler(ICompetenceRepository repository) : IRequestHandler<GetCompetenceByIdQuery, Result<Competence>>
    {
        private readonly ICompetenceRepository _repository = repository;

        public async Task<Result<Competence>> Handle(GetCompetenceByIdQuery request, CancellationToken cancellationToken)
        {
            var competence = await _repository.GetById(request.Id);
            if(competence is null)
                return Result<Competence>.Failure(
                    CompetenceErrors.CompetenceNotFound(request.Id.ToString()),
                    HttpStatusCode.NotFound);

            return Result<Competence>.Success(competence);
        }
    }
}
