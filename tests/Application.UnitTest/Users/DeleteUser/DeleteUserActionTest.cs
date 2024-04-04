using Application.Users.DeleteUser;
using Domain.Abstract;
using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.UnitTest.Users.DeleteUser
{
    public class DeleteUserActionTest
    {
        private readonly DeleteUserAction _action;

        public DeleteUserActionTest()
        {
            var sender = new Mock<ISender>();
            sender.Setup(x => x.Send(It.IsAny<DeleteUserCommand>(), default))
                .ReturnsAsync(Result<object>.Success());

            _action = new(sender.Object);
        }

        [Fact]
        public async Task Execute_ShouldReturnFailure_WhenIdIsNotValidGuid()
        {
            //Arrange
            const string id = "invalid-guid";

            //Act
            var result = await _action.Execute(id);

            //Assert
            Assert.True(result.IsFailure);
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        }

        [Fact]
        public async Task Execute_ShouldReturnSuccess()
        {
            //Arrange
            var id = Guid.NewGuid().ToString();

            //Act
            var result = await _action.Execute(id);

            //Assert
            Assert.True(result.IsSucess);
        }
    }
}
