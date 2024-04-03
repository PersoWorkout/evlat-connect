using Application.Classes;
using Domain.Classes;

namespace Application.UnitTest.Fakers.Repositories
{
    public class FakeClassRepository : IClassRepository
    {
        public List<Class> Classes { get; set; } = [];

        public async Task<Class> Create(Class entity)
        {
            Classes.Add(entity);

            return entity;
        }

        public Task<int> Delete(Class entity)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Class>> GetAll()
        {
            return Classes.ToList();
        }

        public async Task<Class> GetById(Guid id)
        {
            return Classes.FirstOrDefault(x => x.Id == id);
        }

        public Task<List<Class>> GetByType(ClassType type)
        {
            throw new NotImplementedException();
        }

        public Task<Class> Update(Class entity)
        {
            throw new NotImplementedException();
        }

        public void ResetClasses()
        {
            Classes = [];
        }
    }
}
