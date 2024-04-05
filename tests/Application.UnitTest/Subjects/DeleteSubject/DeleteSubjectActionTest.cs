using Application.Subjects.DeleteSubject;
using Domain.Abstract;
using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UnitTest.Subjects.DeleteSubject
{
    public class DeleteSubjectActionTest
    {
        private readonly DeleteSubjectAction _action;

        public DeleteSubjectActionTest() 
        {
            var sender = new Mock<ISender>();
            sender.Setup(x => x.Send(It.IsAny<DeleteSubjectCommand>(), default))
                .ReturnsAsync(Result<object>.Success());

            _action = new(sender.Object);
        }

        [Fact]
        public async Task Execute_ShouldReturnFailure_WhenIdIsNotValid()
        {
            //Arrange
            const string id = "invalid-guid";

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
