using Domain.Subjects;

namespace Application.Subjects
{
    public interface ISubjectRepository
    {
        Task<IEnumerable<Subject>> GetAll();
        Task<Subject> Create(Subject subject);
        Task<Subject> GetById();
        Task<Subject> Update(Subject subject);
        Task<Subject> Delete(Subject subject);
    }
}
