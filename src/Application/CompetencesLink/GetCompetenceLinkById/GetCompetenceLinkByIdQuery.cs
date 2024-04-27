using Domain.Abstract;
using Domain.CompetencesLinks;
using MediatR;

namespace Application.CompetencesLink.GetCompetenceLinkById
{
    public class GetCompetenceLinkByIdQuery(Guid id): IRequest<Result<CompetenceLink>>
    {
        public Guid Id { get; set; } = id;
    }
}
