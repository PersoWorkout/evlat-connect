using Application.Classes.UpdateClasse;
using Application.UnitTest.Fakers.Repositories;
using Domain.Classes;

namespace Application.UnitTest.Classes.UpdateClass
{
    public class UpdateClassHandlerTest
    {
        private readonly FakeClassRepository _classRepository;
        private readonly FakeUserRepository _userRepository;

        private readonly UpdateClassHandler _handler;

        public UpdateClassHandlerTest()
        {
            _classRepository = new();
            _userRepository = new();

            _handler = new(_classRepository, _userRepository);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenClassNotExist()
        {
            //Arrange
            _classRepository.ResetClasses();

            var command = new UpdateClassCommand
            {
                Id = Guid.NewGuid(),
                Name = "Test"
            };

            //Act
            var response = await _handler.Handle(command, default);

            //Assert
            Assert.True(response.IsFailure);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenProfessorNotExist()
        {
            //Arrange
            _classRepository.ResetClasses();
            _userRepository.ClearUsers();

            var createdClass = new Class
            {
                Name = "Class Test",
                Promotion = "2024",
                Type = ClassType.Boy,
                ProfessorId = Guid.NewGuid(),
            };
            _classRepository.Classes.Add(createdClass);

            var command = new UpdateClassCommand
            {
                Id = createdClass.Id,
                Name = "Test",
                ProfessorId = Guid.NewGuid()
            };

            //Act
            var response = await _handler.Handle(command, default);

            //Assert
            Assert.True(response.IsFailure);
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccess()
        {
            //Arrange
            _classRepository.ResetClasses();
            _userRepository.ClearUsers();

            var createdClass = new Class
            {
                Name = "Class Test",
                Promotion = "2024",
                Type = ClassType.Boy,
                ProfessorId = Guid.NewGuid(),
            };
            _classRepository.Classes.Add(createdClass);

            var command = new UpdateClassCommand
            {
                Id = createdClass.Id,
                Name = "Test"
            };

            //Act
            var response = await _handler.Handle(command, default);

            //Assert
            Assert.True(response.IsSucess);
        }
    }
}
