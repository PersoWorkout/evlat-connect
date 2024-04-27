using Application.CompetencesLink.DeleteCompetenceLink;
using Domain.Abstract;
using Domain.CompetencesLinks.Errors;
using MediatR;
using Moq;

namespace Application.UnitTest.CompetencesLink.DeleteCompetenceLink
{
    public class DeleteCompetenceLinkActionTest
    {
        private readonly DeleteCompetenceLinkAction _action;

        public DeleteCompetenceLinkActionTest()
        {
            var sender = new Mock<ISender>();
            sender
                .Setup(x => x.Send(
                    It.IsAny<DeleteCompetenceLinkCommand>(),
                    default))
                .ReturnsAsync(
                    Result<object>.Success());

            _action = new(sender.Object);
        }

        [Fact]
        public async Task Execute_ShouldReturnFailure_WhenIdIsNotValid()
        {
            //Arrange
            const string Id = "invalid-id";

            //Act
            var result = await _action.Execute(Id);

            //Assert
            Assert.True(result.IsFailure);
            Assert.Contains(
                CompetenceLinkErrors.CompetenceLinkNotFound(Id),
                result.Errors);
        }

        [Fact]
        public async Task Execute_ShouldReturnSuccess()
        {
            //Arrange
            string Id = Guid.NewGuid().ToString();

            //Act
            var result = await _action.Execute(Id);

            //Assert
            Assert.True(result.IsSucess);
        }
    }
}
