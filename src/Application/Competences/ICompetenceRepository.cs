using Domain.Competences;

namespace Application.Competences
{
    public interface ICompetenceRepository
    {
        Task<IEnumerable<Competence>> GetAll();
        Task<Competence> Create(Competence competence);
        Task<Competence> GetById(Guid id);
        Task<Competence> Update(Competence competence);
        Task<Competence> AddLink(Link link);
        Task<Competence> UpdateLink(Link link);
        Task<Competence> Delete(Link link);
        Task<int> Delete(Competence competence);
    }
}
