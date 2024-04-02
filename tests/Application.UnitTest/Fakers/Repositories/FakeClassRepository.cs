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

        public Task<IEnumerable<Class>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Class> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Class>> GetByType(ClassType type)
        {
            throw new NotImplementedException();
        }

        public Task<Class> Update(Class entity)
        {
            throw new NotImplementedException();
        }
    }
}
