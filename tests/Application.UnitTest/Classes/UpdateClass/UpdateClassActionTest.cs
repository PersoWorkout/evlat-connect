using Application.Classes.UpdateClasse;
using Application.UnitTest.Configurators;
using Domain.Abstract;
using Domain.Classes;
using Domain.Classes.DTOs;
using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.UnitTest.Classes.UpdateClass
{
    public class UpdateClassActionTest
    {
        private readonly UpdateClassAction _action;

        public UpdateClassActionTest()
        {
            var mapper = MapperConfigurator.ConfigureClassProfile();

            var validator = new UpdateClassValidator();

            var sender = new Mock<ISender>();
            sender.Setup(x => x.Send(It.IsAny<UpdateClassCommand>(), default))
                .ReturnsAsync(Result<Class>.Success(new Class()));

            _action = new(sender.Object, validator, mapper);
        }

        [Fact]
        public async Task Execute_ShouldReturnFailure_WhenIdIsNotAValidGuid()
        {
            //Arrange
            const string Id = "invalid-guid";
            var request = new UpdateClassRequest();

            //Act
            var result = await _action.Execute(Id, request);

            //Assert
            Assert.True(result.IsFailure);
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        }

        [Fact]
        public async Task Execute_ShouldReturnFailure_WhenRequestIsNotValid()
        {
            //Arrange
            string Id = Guid.NewGuid().ToString();
            var request = new UpdateClassRequest
            {
                Type = 22
            };

            //Act
            var result = await _action.Execute(Id, request);

            //Assert
            Assert.True(result.IsFailure);
        }

        [Fact]
        public async Task Execute_ShouldReturnSuccess()
        {
            //Arrange
            string Id = Guid.NewGuid().ToString();
            var request = new UpdateClassRequest
            {
                Type = ClassType.Boy.GetHashCode()
            };

            //Act
            var result = await _action.Execute(Id, request);

            //Assert
            Assert.True(result.IsSucess);
        }
    }
}
