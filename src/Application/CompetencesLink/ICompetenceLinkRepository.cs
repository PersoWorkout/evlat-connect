using Domain.CompetencesLinks;

namespace Application.CompetencesLink
{
    public interface ICompetenceLinkRepository
    {
        Task<IEnumerable<CompetenceLink>> GetAllByCompetence(Guid competenceId);
        Task<CompetenceLink> Create(CompetenceLink competenceLink);
        Task<CompetenceLink> GetById(Guid id);
        Task<CompetenceLink> Update(CompetenceLink competenceLink);
        Task<int> Delete(CompetenceLink competenceLink);
    }
}
