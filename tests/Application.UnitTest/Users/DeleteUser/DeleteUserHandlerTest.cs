using Application.UnitTest.Fakers.Repositories;
using Application.Users.DeleteUser;
using Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.UnitTest.Users.DeleteUser
{
    public class DeleteUserHandlerTest
    {
        private readonly FakeUserRepository _repository;

        private readonly DeleteUserHandler _handler;

        public DeleteUserHandlerTest()
        {
            _repository = new();

            _handler = new(_repository);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenUserNotExist()
        {
            //Arrange
            _repository.ClearUsers();

            var command = new DeleteUserCommand(Guid.NewGuid());

            //Act
            var result = await _handler.Handle(command, default);

            //Assert
            Assert.True(result.IsFailure);
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccess()
        {
            //Arrange
            _repository.ClearUsers();

            var user = new User
            {
                Firstname = "John",
                Lastname = "Doe",
                Username = "john.doe",
                Role = UserRole.Student
            };
            _repository.Users.Add(user);

            var command = new DeleteUserCommand(user.Id);

            //Act
            var result = await _handler.Handle(command, default);

            //Assert
            Assert.True(result.IsSucess);
        }
    }
}
