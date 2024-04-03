using Application.Classes.DeleteClass;
using Domain.Abstract;
using MediatR;
using Moq;

namespace Application.UnitTest.Classes.DeleteClass
{
    public class DeleteClassActionTest
    {
        private readonly DeleteClassAction _action;

        public DeleteClassActionTest()
        {
            var sender = new Mock<ISender>();
            sender.Setup(x => x.Send(It.IsAny<DeleteClassCommand>(), default))
                .ReturnsAsync(Result<object>.Success());

            _action = new(sender.Object);
        }

        [Fact]
        public async Task Execute_ShouldReturnFailure_WhenIdIsNotValid()
        {
            //Arrange
            const string id = "invalid_guid";

            //Act
            var result = await _action.Execute(id);

            //Assert
            Assert.True(result.IsFailure);
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
