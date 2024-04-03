using Application.UnitTest.Configurators;
using Application.UnitTest.Fakers.Repositories;
using Application.Users.CreateUser;
using Domain.Classes;
using Domain.Classes.Errors;
using Domain.Users;
using Domain.Users.ValueObjects;

namespace Application.UnitTest.Users.CreateUser
{
    public class CreateUserHandlerTest
    {
        private readonly FakeUserRepository _repository;
        private readonly FakeClassRepository _classRepository;
        private readonly CreateUserHandler _handler;

        public CreateUserHandlerTest()
        {
            _repository = new();
            _classRepository = new();
            var mapper = MapperConfigurator.ConfigureUserProfile();

            _handler = new(_repository, _classRepository, mapper);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenClassNotExistAndUserIsStudent()
        {
            //Arrange
            _classRepository.ResetClasses();

            var command = new CreateUserCommand
            {
                Firstname = "John",
                Lastname = "Doe",
                Role = UserRole.Student,
                Password = PasswordValueObject.Create("Password123!").Data!,
                PhoneNumber = PhoneNumberValueObject.Create("+33601010101!").Data!,
                ClassId = Guid.NewGuid(),
                
            };

            //Act
            var result = await _handler.Handle(command, default);

            //Assert
            var expectedError = ClassErrors.ClassNotFound(command.ClassId!.ToString());

            Assert.True(result.IsFailure);
            Assert.Contains(expectedError, result.Errors);
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccessResult()
        {
            //Arrange
            _classRepository.ResetClasses();

            var classTest = new Class
            {
                Id = Guid.NewGuid(),
                Name = "Test",
                Promotion = "2023",
                Type = ClassType.Boy,
                ProfessorId = Guid.NewGuid(),
            };
            _classRepository.Classes.Add(classTest);

            var command = new CreateUserCommand
            {
                Firstname = "John",
                Lastname = "Doe",
                Role = UserRole.Student,
                Password = PasswordValueObject.Create("Password123!").Data!,
                PhoneNumber = PhoneNumberValueObject.Create("+33601010101!").Data!,
                ClassId = classTest.Id
            };

            //Act
            var result = await _handler.Handle(command, default);

            //Assert
            Assert.True(result.IsSucess);
        }
    }
}
