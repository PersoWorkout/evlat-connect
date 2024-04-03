using Application.Auth.Login;
using Application.UnitTest.Configurators;
using AutoMapper;
using Domain.Auth;
using Domain.Auth.DTOs;

namespace Application.UnitTest.Auth
{
    public class AuthProfileTest
    {
        private readonly IMapper _mapper = 
            MapperConfigurator.ConfigureAuthProfile();

        [Fact]
        public void Map_ShouldMapLoginRequest_ToLoginCommand()
        {
            //Arrange
            var request = new LoginRequest
            {
                Username = "john.doe",
                Password = "Password123!",
                PasswordValidation = "Password123!"
            };

            //Act
            var command = _mapper.Map<LoginCommand>(request);

            //Assert
            Assert.IsType<LoginCommand>(command);
            Assert.Equal(request.Username, command.Username);
            Assert.Equal(request.Password, command.Password.Value);
        }

        [Fact]
        public void Map_ShouldMapSession_ToAuthenticatedResponse()
        {
            //Arrange
            var session = new Session(Guid.NewGuid());

            //Act
            var response = _mapper.Map<AuthenticatedResponse>(session);

            //Assert
            Assert.IsType<AuthenticatedResponse>(response);
            Assert.Equal(response.Token, session.Token.Value);
            Assert.Equal(response.ExpiresAt, session.ExpiresAt);
        }
    }
}
