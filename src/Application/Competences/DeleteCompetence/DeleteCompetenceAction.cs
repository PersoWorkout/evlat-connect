using Domain.Abstract;
using Domain.Competences.Errors;
using MediatR;
using System.Net;

namespace Application.Competences.DeleteCompetence
{
    public class DeleteCompetenceAction(ISender sender)
    {
        private readonly ISender _sender = sender;

        public async Task<Result<object>> Execute(string id)
        {
            if(!Guid.TryParse(id, out var parsedId))
                return Result<object>.Failure(
                    CompetenceErrors.CompetenceNotFound(id),
                    HttpStatusCode.NotFound);

            return await _sender.Send(
                new DeleteCompetenceCommand(parsedId));
        }
    }
}
