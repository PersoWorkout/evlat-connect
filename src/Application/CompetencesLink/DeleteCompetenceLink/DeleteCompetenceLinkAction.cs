using Domain.Abstract;
using Domain.CompetencesLinks.Errors;
using MediatR;
using System.Net;

namespace Application.CompetencesLink.DeleteCompetenceLink
{
    public class DeleteCompetenceLinkAction(ISender sender)
    {
        private readonly ISender _sender = sender;

        public async Task<Result<object>> Execute(string id)
        {
            if(!Guid.TryParse(id, out var competenceLinkId)) 
                return Result<object>.Failure(
                    CompetenceLinkErrors.CompetenceLinkNotFound(id),
                    HttpStatusCode.NotFound);

            return await _sender.Send(
                new DeleteCompetenceLinkCommand(competenceLinkId));
        }
    }
}
