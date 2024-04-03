using Application.Auth.Login;
using Application.UnitTest.Configurators;
using Domain.Abstract;
using Domain.Auth;
using Domain.Auth.DTOs;
using MediatR;
using Moq;

namespace Application.UnitTest.Auth.Login
{
    public class LoginActionTest
    {
        private readonly LoginAction _action;

        public LoginActionTest()
        {
            var mapper = MapperConfigurator.ConfigureAuthProfile();
            var validator = new LoginValidator();

            var sender = new Mock<ISender>();
            sender.Setup(x => x.Send(It.IsAny<LoginCommand>(), default))
                .ReturnsAsync(Result<Session>.Success(new Session(Guid.NewGuid())));

            _action = new(sender.Object, validator, mapper);
        }

        [Fact]
        public async Task Execute_ShouldReturnFailure_WhenPayloadIsNotValid()
        {
            var requet = new LoginRequest
            {
                Username = "",
                Password = "",
                PasswordValidation = ""
            };

            //Act
            var result = await _action.Execute(requet);

            //Assert
            Assert.True(result.IsFailure);
        }

        [Fact]
        public async Task Execute_ShouldReturnSuccess()
        {
            var requet = new LoginRequest
            {
                Username = "john.doe",
                Password = "Password123!",
                PasswordValidation = "Password123!"
            };

            //Act
            var result = await _action.Execute(requet);

            //Assert
            Assert.True(result.IsSucess);
        }
    }
}
