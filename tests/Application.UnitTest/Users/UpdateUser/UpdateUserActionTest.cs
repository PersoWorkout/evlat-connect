using Application.UnitTest.Configurators;
using Application.Users.UpdateUser;
using Domain.Abstract;
using Domain.Users;
using Domain.Users.DTOs;
using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UnitTest.Users.UpdateUser
{
    public class UpdateUserActionTest
    {
        private readonly UpdateUserAction _action;

        public UpdateUserActionTest()
        {
            var mapper = MapperConfigurator.ConfigureUserProfile();

            var validator = new UpdateUserValidator();

            var sender = new Mock<ISender>();
            sender.Setup(x => x.Send(It.IsAny<UpdateUserCommand>(), default))
                .ReturnsAsync(Result<User>.Success(new User()));

            _action = new(sender.Object, validator, mapper);
        }

        [Fact]
        public async Task Execute_ShouldReturnFailure_WhenUserIdIsNotValid()
        {
            //Arrange
            const string userId = "invallid-guid";

            var request = new UpdateUserRequest();

            //Act
            var result = await _action.Execute(userId, request);

            //Assert
            Assert.True(result.IsFailure);
        }

        [Fact]
        public async Task Execute_ShouldReturnFailure_WhenRequestIsInvalid()
        {
            //Arrange
            string userId = Guid.NewGuid().ToString();

            var request = new UpdateUserRequest
            {
                ClassId = "invalid-guid"
            };

            //Act
            var result = await _action.Execute(userId, request);

            //Assert
            Assert.True(result.IsFailure);
        }

        [Fact]
        public async Task Execute_ShouldReturnSuccess()
        {
            //Arrange
            string userId = Guid.NewGuid().ToString();

            var request = new UpdateUserRequest
            {
                Firstname = "another_firstname"
            };

            //Act
            var result = await _action.Execute(userId, request);

            //Assert
            Assert.True(result.IsSucess);
        }
    }
}
