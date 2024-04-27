using Domain.Abstract;
using MediatR;

namespace Application.CompetencesLink.DeleteCompetenceLink
{
    public class DeleteCompetenceLinkCommand(Guid id): IRequest<Result<object>>
    {
        public Guid Id { get; set; } = id;
    }
}
