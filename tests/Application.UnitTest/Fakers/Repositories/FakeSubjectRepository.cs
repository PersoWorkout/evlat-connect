using Application.Subjects;
using Domain.Subjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UnitTest.Fakers.Repositories
{
    public class FakeSubjectRepository : ISubjectRepository
    {
        public List<Subject> Subjects { get; set; } = [];

        public async Task<Subject> Create(Subject subject)
        {
            Subjects.Add(subject);
            return subject;
        }

        public Task<Subject> Delete(Subject subject)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Subject>> GetAll()
        {
            return Subjects;
        }

        public async Task<Subject> GetById(Guid id)
        {
            var idx = Subjects.FindIndex(x => x.Id == id);

            if (idx == -1)
                return null;

            return Subjects[idx];
        }

        public Task<Subject> Update(Subject subject)
        {
            throw new NotImplementedException();
        }

        public void ClearSubjects()
        {
            Subjects = [];
        }
    }
}
