using Application.Classes.GetClassById;
using Application.UnitTest.Configurators;
using Domain.Abstract;
using Domain.Classes;
using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UnitTest.Classes.GetClassById
{
    public class GetClassByIdActionTest
    {
        private readonly GetClassByIdAction _action;

        public GetClassByIdActionTest()
        {
            var mapper = MapperConfigurator.ConfigureClassProfile();

            var sender = new Mock<ISender>();
            sender.Setup(x => x.Send(It.IsAny<GetClassByIdQuery>(), default))
                .ReturnsAsync(Result<Class>.Success(new Class()));

            _action = new(sender.Object, mapper);
        }

        [Fact]
        public async Task Execute_ShouldReturnFailure_WhenIdIsNotAValidGuid()
        {
            //Arrange
            const string Id = "invalid-guid";

            //Act
            var result = await _action.Execute(Id);

            //Assert
            Assert.True(result.IsFailure);
        }

        [Fact]
        public async Task Execute_ShouldReturnSuccess()
        {
            //Arrange
            var Id = Guid.NewGuid().ToString();

            //Act
            var result = await _action.Execute(Id);

            //Assert
            Assert.True(result.IsSucess);
        }
    }
}
