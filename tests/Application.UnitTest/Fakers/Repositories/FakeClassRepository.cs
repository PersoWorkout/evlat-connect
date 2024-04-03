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

        public async Task<int> Delete(Class entity)
        {
            var idx = Classes.FindIndex(x => x.Id == entity.Id);

            if (idx == -1)
                return 0;

            Classes.RemoveAt(idx);
            return 1;
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

        public async Task<Class> Update(Class entity)
        {
            var classId = Classes.FindIndex(x => x.Id == entity.Id);

            Classes[classId] = entity;

            return entity;
        }

        public void ResetClasses()
        {
            Classes = [];
        }
    }
}
