using Application.Competences;
using Domain.Competences;

namespace Application.UnitTest.Fakers.Repositories
{
    public class FakeCompetenceRepository : ICompetenceRepository
    {
        public List<Competence> Competences { get; set; } = [];

        public async Task<Competence> Create(Competence competence)
        {
            Competences.Add(competence);

            return competence;
        }

        public Task<int> Delete(Competence competence)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Competence>> GetAll()
        {
            return Competences;
        }

        public async Task<Competence> GetById(Guid id)
        {
            return Competences.FirstOrDefault(x => x.Id == id);
        }

        public async Task<Competence> Update(Competence competence)
        {
            var idx = Competences.FindIndex(x => x.Id == competence.Id);

            if (idx == -1) return null;

            Competences[idx] = competence;

            return competence;
        }

        public void ClearCompetences()
        {
            Competences = [];
        }
    }
}
