using Application.Auth.Login;
using Application.UnitTest.Fakers.Repositories;
using Domain.Auth;
using Domain.Users;
using Domain.Users.ValueObjects;

namespace Application.UnitTest.Auth.Login
{
    public class LoginHandlerTest
    {
        private readonly FakeAuthRepository _repository;

        private readonly LoginHandler _handler;

        public LoginHandlerTest()
        {
            _repository = new();

            _handler =  new(_repository);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenUserNotExist()
        {
            //Arrange
            _repository.ClearLists();

            var command = new LoginCommand
            {
                Username = "john.doe",
                Password = PasswordValueObject.Create("Password123!").Data!
            };

            //Act
            var result = await _handler.Handle(command, default);

            //Assert
            Assert.True(result.IsFailure);
            Assert.Contains(AuthErrors.InvalidCredentials, result.Errors);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenCredentialsAreNotValid()
        {
            //Arrange
            _repository.ClearLists();

            var user = new User
            {
                Firstname = "John",
                Lastname = "Doe",
                Password = PasswordValueObject.Create("Password123!").Data!,
                Username = "john.doe"
            };
            _repository.Users.Add(user);

            var command = new LoginCommand
            {
                Username = user.Username,
                Password = PasswordValueObject.Create("AnotherPassword123!").Data!
            };

            //Act
            var result = await _handler.Handle(command, default);

            //Assert
            Assert.True(result.IsFailure);
            Assert.Contains(AuthErrors.InvalidCredentials, result.Errors);
        }

        [Fact]
        public async Task Handle_ShouldReturnSucess()
        {
            //Arrange
            _repository.ClearLists();

            var user = new User
            {
                Firstname = "John",
                Lastname = "Doe",
                Password = PasswordValueObject.Create("Password123!").Data!,
                Username = "john.doe"
            };
            _repository.Users.Add(user);

            var command = new LoginCommand
            {
                Username = user.Username,
                Password = user.Password
            };

            //Act
            var result = await _handler.Handle(command, default);

            //Assert
            Assert.True(result.IsSucess);
        }
    }
}
