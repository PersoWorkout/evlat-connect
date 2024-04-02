using Application.UnitTest.Configurators;
using Application.UnitTest.Fakers.Repositories;
using Application.Users.CreateUser;
using Domain.Users;
using Domain.Users.ValueObjects;

namespace Application.UnitTest.Users.CreateUser
{
    public class CreateUserHandlerTest
    {
        private readonly FakeUserRepository _repository;
        private readonly CreateUserHandler _handler;

        public CreateUserHandlerTest()
        {
            _repository = new();
            var mapper = MapperConfigurator.ConfigureUserProfile();

            _handler = new(_repository, mapper);
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccessResult()
        {
            //Arrange
            var command = new CreateUserCommand
            {
                Firstname = "John",
                Lastname = "Doe",
                Role = UserRole.Student,
                Password = PasswordValueObject.Create("Password123!").Data!,
                PhoneNumber = PhoneNumberValueObject.Create("+33601010101!").Data!
            };

            //Act
            var result = await _handler.Handle(command, default);

            //Assert
            Assert.True(result.IsSucess);
        }
    }
}
