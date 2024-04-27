using Domain.Abstract;
using Domain.CompetencesLinks.Errors;
using MediatR;
using System.Net;

namespace Application.CompetencesLink.DeleteCompetenceLink
{
    public class DeleteCompetenceLinkHandler(
        ICompetenceLinkRepository repository) : IRequestHandler<DeleteCompetenceLinkCommand, Result<object>>
    {
        private readonly ICompetenceLinkRepository _repository = repository;

        public async Task<Result<object>> Handle(DeleteCompetenceLinkCommand request, CancellationToken cancellationToken)
        {
            var competenceLink = await _repository.GetById(request.Id);
            if (competenceLink is null)
                return Result<object>.Failure(
                    CompetenceLinkErrors.CompetenceLinkNotFound(request.Id.ToString()),
                    HttpStatusCode.NotFound);
            
            await _repository.Delete(competenceLink);

            return Result<object>.Success();
        }
    }
}
