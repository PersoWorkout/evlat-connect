using Domain.Abstract;
using Domain.CompetencesLinks;
using Domain.CompetencesLinks.Errors;
using MediatR;
using System.Net;

namespace Application.CompetencesLink.GetCompetenceLinkById
{
    public class GetCompetenceLinkByIdHandler(
        ICompetenceLinkRepository repository) : IRequestHandler<GetCompetenceLinkByIdQuery, Result<CompetenceLink>>
    {
        private readonly ICompetenceLinkRepository _repository = repository;

        public async Task<Result<CompetenceLink>> Handle(GetCompetenceLinkByIdQuery request, CancellationToken cancellationToken)
        {
            var competenceLink = await _repository.GetById(request.Id);

            if(competenceLink is null)
                return Result<CompetenceLink>.Failure(
                    CompetenceLinkErrors.CompetenceLinkNotFound(request.Id.ToString()),
                    HttpStatusCode.NotFound);

            return Result<CompetenceLink>.Success(competenceLink);
        }
    }
}
