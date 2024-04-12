using Domain.Competences;

namespace Application.Competences
{
    public interface ICompetenceRepository
    {
        Task<IEnumerable<Competence>> GetAll();
        Task<Competence> Create(Competence competence);
        Task<Competence> GetById(Guid id);
        Task<Competence> Update(Competence competence);
        Task<int> Delete(Competence competence);
    }
}
