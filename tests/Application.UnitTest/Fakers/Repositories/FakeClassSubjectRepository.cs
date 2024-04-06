using Application.ClassesSubjects;
using Domain.ClassesSubjects;

namespace Application.UnitTest.Fakers.Repositories
{
    public class FakeClassSubjectRepository : IClassSubjectRepository
    {
        public List<ClassSubject> ClassesSubjects { get; set; } = [];

        public async Task<ClassSubject> Create(ClassSubject classSubject)
        {
            ClassesSubjects.Add(classSubject);

            return classSubject;
        }

        public async Task<int> Delete(ClassSubject classSubject)
        {
            var idx = ClassesSubjects.FindIndex(x => 
                x.ClassId ==  classSubject.ClassId && 
                x.StartedAt == classSubject.StartedAt && 
                x.SubjectId == classSubject.SubjectId);

            if (idx == -1) return 0;

            ClassesSubjects.RemoveAt(idx);
            return 1;
        }

        public async Task<bool> ExistByClassAndDates(Guid classId, DateTime startedAt, DateTime finishedAt)
        {
            return ClassesSubjects.Any(x =>
            x.ClassId == classId &&
                (x.StartedAt <= startedAt && x.FinishedAt > startedAt) ||
                (x.StartedAt < finishedAt && x.FinishedAt >= finishedAt));
        }

        public async Task<IEnumerable<ClassSubject>> GetByClass(Guid classId, DateTime? from = null, DateTime? to = null)
        {
            return ClassesSubjects.Where(x =>
                x.ClassId == classId &&
                (!from.HasValue || x.StartedAt >= from.Value) &&
                (!to.HasValue || x.FinishedAt <= to.Value))
                .ToList();
        }

        public Task<ClassSubject> GetByClassAndDate(Guid classId, DateTime startedAt)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ClassSubject>> GetByProfessorId(Guid ProfessorId)
        {
            throw new NotImplementedException();
        }

        public Task<ClassSubject> Update(ClassSubject classSubject)
        {
            throw new NotImplementedException();
        }

        public void ClearClassesSubjects()
        {
            ClassesSubjects = [];
        }
    }
}
