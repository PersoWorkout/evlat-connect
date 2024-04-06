using Domain.ClassesSubjects;

namespace Application.ClassesSubjects
{
    public interface IClassSubjectRepository
    {
        Task<IEnumerable<ClassSubject>> GetByClass(
            Guid classId, 
            DateTime? from = null, 
            DateTime? at = null);
        Task<IEnumerable<ClassSubject>> GetByProfessorId(Guid ProfessorId);
        Task<ClassSubject> Create(ClassSubject classSubject);
        Task<ClassSubject> GetByClassAndDate(Guid classId, DateTime startedAt);
        Task<bool> ExistByClassAndDates(Guid classId, DateTime startedAt, DateTime finishedAt);
        Task<ClassSubject> Update(ClassSubject classSubject);
        Task<int> Delete(ClassSubject classSubject);
    }
}
