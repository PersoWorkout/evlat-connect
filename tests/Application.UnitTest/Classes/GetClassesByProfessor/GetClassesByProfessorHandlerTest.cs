using Application.Classes.GetClassesByProfessor;
using Application.UnitTest.Fakers.Repositories;
using Domain.Classes;
using Domain.Users;

namespace Application.UnitTest.Classes.GetClassesByProfessor
{
    public class GetClassesByProfessorHandlerTest
    {
        private readonly FakeUserRepository _userRepository;
        private readonly FakeClassRepository _classRepository;

        private readonly GetClassesByProfessorHandler _handler;

        public GetClassesByProfessorHandlerTest()
        {
            _userRepository = new();
            _classRepository = new();

            _handler = new(_classRepository, _userRepository);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenProfessorNotExist()
        {
            //Arrange
            _userRepository.ClearUsers();
            _classRepository.ResetClasses();

            var query = new GetClassesByProfessorQuery(Guid.NewGuid());

            //Act
            var result = await _handler.Handle(query, default);

            //Assert
            Assert.True(result.IsFailure);
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccess()
        {
            //Arrange
            _userRepository.ClearUsers();
            _classRepository.ResetClasses();

            var user = new User
            {
                Firstname = "John",
                Lastname = "Doe",
                Username = "john.doe",
                Role = UserRole.Professeur
            };
            _userRepository.Users.Add(user);

            var classEntity = new Class
            {
                Name = "Class Test",
                Promotion = "2024",
                ProfessorId = user.Id,
                Type = ClassType.Mixte
            };
            _classRepository.Classes.Add(classEntity);

            var query = new GetClassesByProfessorQuery(user.Id);

            //Act
            var result = await _handler.Handle(query, default);

            //Assert
            Assert.True(result.IsSucess);
            Assert.IsAssignableFrom<IEnumerable<Class>>(result.Data);
            Assert.NotEmpty(result.Data);
        }
    }
}
