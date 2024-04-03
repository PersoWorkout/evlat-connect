using Application.Classes.DeleteClass;
using Application.UnitTest.Fakers.Repositories;
using Domain.Classes;

namespace Application.UnitTest.Classes.DeleteClass
{
    public class DeleteClassHandlerTest
    {
        private readonly FakeClassRepository _repository;

        private readonly DeleteClassHandler _handler;

        public DeleteClassHandlerTest()
        {
            _repository = new();

            _handler = new(_repository);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenClassNotExist()
        {
            //Arrange
            _repository.ResetClasses();

            var command = new DeleteClassCommand(Guid.NewGuid());

            //Act
            var result = await _handler.Handle(command, default);

            //Assert
            Assert.True(result.IsFailure);
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccess()
        {
            //Arrange
            _repository.ResetClasses();
            var createdClass = new Class
            {
                Name = "Test",
                Promotion = "2024",
                Type = ClassType.Boy
            };
            _repository.Classes.Add(createdClass);

            var command = new DeleteClassCommand(createdClass.Id);

            //Act
            var result = await _handler.Handle(command, default);

            //Assert
            Assert.True(result.IsSucess);
        }
    }
}
