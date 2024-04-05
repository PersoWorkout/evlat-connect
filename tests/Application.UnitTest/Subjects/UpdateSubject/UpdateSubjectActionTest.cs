using Application.Subjects.UpdateSubject;
using Application.UnitTest.Configurators;
using Domain.Abstract;
using Domain.Subjects;
using Domain.Subjects.DTOs;
using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UnitTest.Subjects.UpdateSubject
{
    public class UpdateSubjectActionTest
    {
        private readonly UpdateSubjectAction _action;

        public UpdateSubjectActionTest()
        {
            var mapper = MapperConfigurator.ConfigureSubjectProfile();

            var sender = new Mock<ISender>();
            sender.Setup(x => x.Send(It.IsAny<UpdateSubjectCommand>(), default))
                .ReturnsAsync(Result<Subject>.Success(new Subject()));

            _action = new(sender.Object, mapper);
        }

        [Fact]
        public async Task Execute_ShouldReturnFailure_WhenIdIsNotValid()
        {
            //Arrange
            string id = "invalid-guid";

            var request = new UpdateSubjectRequest
            {
                Name = "Test"
            };

            //Act
            var result = await _action.Execute(id, request);

            //Assert
            Assert.True(result.IsFailure);
        }

        [Fact]
        public async Task Execute_ShouldReturnSuccess()
        {
            //Arrange
            string id = Guid.NewGuid().ToString();

            var request = new UpdateSubjectRequest
            {
                Name = "Test"
            };

            //Act
            var result = await _action.Execute(id, request);

            //Assert
            Assert.True(result.IsSucess);
        }
    }
}
