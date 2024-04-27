using Domain.Abstract;
using Domain.CompetencesLinks;
using MediatR;

namespace Application.CompetencesLink.CreateCompetenceLink
{
    public class CreateCompetenceLinkCommand: 
        IRequest<Result<CompetenceLink>>
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public LinkType Type { get; set; }
        public Guid CompetenceId { get; set; }
    }
}
