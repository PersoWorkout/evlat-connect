using Application.Classes.GetAll;
using Application.Classes.GetClasses;
using Application.UnitTest.Fakers.Repositories;
using Domain.Classes;

namespace Application.UnitTest.Classes.GetClasses
{
    public class GetClassesHandlerTest
    {
        private readonly FakeClassRepository _repository;

        private GetClassesHandler _handler;

        public GetClassesHandlerTest()
        {
            _repository = new();

            _handler = new(_repository);
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccess()
        {
            //Arrange
            _repository.ResetClasses();

            var createdClass = new Class
            {
                Name = "Class Test",
                Promotion = "2023",
                Type = ClassType.Boy
            };
            _repository.Classes.Add(createdClass);

            //Act
            var result = await _handler.Handle(new GetClassesQuery(), default);

            //Assert
            Assert.True(result.IsSucess);
            Assert.IsAssignableFrom<List<Class>>(result.Data);
        }
    }
}
