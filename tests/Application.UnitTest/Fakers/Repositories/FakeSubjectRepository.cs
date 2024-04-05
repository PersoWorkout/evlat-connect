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

        public async Task<int> Delete(Subject subject)
        {
            var idx = Subjects.FindIndex(x => x.Id == subject.Id);

            if (idx == -1)
                return 0;

            Subjects.RemoveAt(idx);
            return 1;
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

        public async Task<Subject> Update(Subject subject)
        {
            var idx = Subjects.FindIndex(x => x.Id == subject.Id);

            if (idx == -1)
                return null;

            Subjects[idx] = subject;

            return subject;
        }

        public void ClearSubjects()
        {
            Subjects = [];
        }
    }
}
