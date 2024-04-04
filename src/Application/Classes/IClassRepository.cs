using Domain.Classes;

namespace Application.Classes
{
    public interface IClassRepository
    {
        Task<IEnumerable<Class>> GetAll();
        Task<List<Class>> GetByType(ClassType type);
        Task<IEnumerable<Class>> GetByProfessor(Guid professorId);
        Task<Class> Create(Class entity);
        Task<Class> GetById(Guid id);
        Task<Class> Update(Class entity);
        Task<int> Delete(Class entity);
    }
}
