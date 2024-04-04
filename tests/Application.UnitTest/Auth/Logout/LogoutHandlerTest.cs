using Application.Auth.Logout;
using Application.UnitTest.Fakers.Repositories;
using Domain.Auth;
using Domain.Auth.ValueObjects;
using System.Net;

namespace Application.UnitTest.Auth.Logout
{
    public class LogoutHandlerTest
    {
        private readonly FakeAuthRepository _repository;

        private readonly LogoutHandler _handler;

        public LogoutHandlerTest()
        {
            _repository = new FakeAuthRepository();

            _handler = new(_repository);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenSessionNotExist()
        {
            //Arrange
            _repository.ClearLists();

            var command = new LogoutCommand(new TokenValueObject());

            //Act
            var result = await _handler.Handle(command, default);

            //Assert
            Assert.True(result.IsFailure);
            Assert.Equal(HttpStatusCode.Unauthorized, result.StatusCode);
        }

        [Fact]
        public async Task Handle_ShouldReturnSucces_WhenSessionExist()
        {
            //Arrange
            _repository.ClearLists();

            var session = new Session(Guid.NewGuid());
            _repository.Sessions.Add(session);

            var command = new LogoutCommand(session.Token);

            //Act
            var result = await _handler.Handle(command, default);

            //Assert
            Assert.True(result.IsSucess);
        }
    }
}
