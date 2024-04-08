using Application.ClassesSubjects.DeleteClassSubject;
using Domain.Abstract;
using MediatR;
using Moq;

namespace Application.UnitTest.ClassesSubjects.DeleteClassSubject
{
    public class DeleteClassSubjectActionTest
    {
        private readonly DeleteClassSubjectAction _action;

        public DeleteClassSubjectActionTest()
        {
            var sender = new Mock<ISender>();
            sender.Setup(x => x.Send(It.IsAny<DeleteClassSubjectCommand>(), default))
                .ReturnsAsync(Result<object>.Success());

            _action = new(sender.Object);
        }

        [Fact]
        public async Task Execute_ShouldReturnFailure_WhenClassIdIsNotValid()
        {
            //Arrange
            string classId = "invalid-guid";
            string date = DateTime.Now.ToString();

            //Act
            var result = await _action.Execute(classId, date);

            //Assert
            Assert.True(result.IsFailure);
        }

        [Fact]
        public async Task Execute_ShouldReturnFailure_WhenDateIsNotValid()
        {
            //Arrange
            string classId = Guid.NewGuid().ToString();
            string date = "invalid-date";

            //Act
            var result = await _action.Execute(classId, date);

            //Assert
            Assert.True(result.IsFailure);
        }

        [Fact]
        public async Task Execute_ShouldReturnSuccess()
        {
            //Arrange
            string classId = Guid.NewGuid().ToString();
            string date = DateTime.Now.ToString();

            //Act
            var result = await _action.Execute(classId, date);

            //Assert
            Assert.True(result.IsSucess);
        }
    }
}
