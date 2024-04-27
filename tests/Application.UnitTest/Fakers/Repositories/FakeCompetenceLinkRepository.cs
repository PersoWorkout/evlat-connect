using Application.CompetencesLink;
using Domain.Competences;
using Domain.CompetencesLinks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UnitTest.Fakers.Repositories
{
    public class FakeCompetenceLinkRepository : ICompetenceLinkRepository
    {
        public List<CompetenceLink> CompetenceLinks = [];

        public async Task<CompetenceLink> Create(CompetenceLink competenceLink)
        {
            CompetenceLinks.Add(competenceLink);

            return competenceLink;
        }

        public async Task<int> Delete(CompetenceLink competenceLink)
        {
            var idx = CompetenceLinks.FindIndex(x => x.Id == competenceLink.Id);

            if (idx == -1) return 0;

            CompetenceLinks.RemoveAt(idx);
            return 1;
        }

        public async Task<IEnumerable<CompetenceLink>> GetAllByCompetence(Guid competenceId)
        {
            return CompetenceLinks
                .Where(x => x.CompetenceId == competenceId)
                .ToList();
        }

        public async Task<CompetenceLink> GetById(Guid id)
        {
            return CompetenceLinks
                .FirstOrDefault(x => x.Id == id);
        }

        public async Task<CompetenceLink> Update(CompetenceLink competenceLink)
        {
            var idx = CompetenceLinks.FindIndex(x => x.Id == competenceLink.Id);

            if (idx == -1) return null;

            CompetenceLinks[idx] = competenceLink;
            return competenceLink;
        }

        public void ClearCompetences()
        {
            CompetenceLinks.Clear();
        }
    }
}
