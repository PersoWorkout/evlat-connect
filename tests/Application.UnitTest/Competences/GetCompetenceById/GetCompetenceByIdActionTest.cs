using Application.Competences.GetCompetenceById;
using Application.UnitTest.Configurators;
using Domain.Abstract;
using Domain.Competences;
using MediatR;
using Moq;

namespace Application.UnitTest.Competences.GetCompetenceById
{
    public class GetCompetenceByIdActionTest
    {
        private readonly GetCompetenceByIdAction _action;

        public GetCompetenceByIdActionTest()
        {
            var mapper = MapperConfigurator.ConfigureCompetenceProfile();

            var sender = new Mock<ISender>();
            sender.Setup(x => x.Send(It.IsAny<GetCompetenceByIdQuery>(), default))
                .ReturnsAsync(Result<Competence>.Success());

            _action = new(sender.Object, mapper);
        }

        [Fact]
        public async Task Execute_ShouldReturnFailure_WhenIdIsNotValid()
        {
            ///Arrange
            string Id = "invalid-guid";

            //Act
            var result = await _action.Execute(Id);

            //Assert
            Assert.True(result.IsFailure);
        }

        [Fact]
        public async Task Execute_ShouldReturnSuccess()
        {
            ///Arrange
            string Id = Guid.NewGuid().ToString();

            //Act
            var result = await _action.Execute(Id);

            //Assert
            Assert.True(result.IsSucess);
        }
    }
}
