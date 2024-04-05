using Application.Subjects.GetSubjectById;
using Application.UnitTest.Configurators;
using Domain.Abstract;
using Domain.Subjects;
using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UnitTest.Subjects.GetSubjectById
{
    public class GetSubjectByIdActionTest
    {
        private readonly GetSubjectByIdAction _action;

        public GetSubjectByIdActionTest()
        {
            var mapper = MapperConfigurator.ConfigureSubjectProfile();

            var sender = new Mock<ISender>();
            sender.Setup(x => x.Send(It.IsAny<GetSubjectByIdQuery>(), default))
                .ReturnsAsync(Result<Subject>.Success(new Subject()));

            _action = new(sender.Object, mapper);
        }

        [Fact]
        public async Task Execute_ShouldReturnFailure_WhenIdIsNotValid()
        {
            //Arrange
            const string id = "Invalid-guid";

            //Act
            var result = await _action.Execute(id);

            //Assert
            Assert.True(result.IsFailure);
        }

        [Fact]
        public async Task Execute_ShouldReturnSuccess()
        {
            //Arrange
            string id = Guid.NewGuid().ToString();

            //Act
            var result = await _action.Execute(id);

            //Assert
            Assert.True(result.IsSucess);
        }
    }
}
