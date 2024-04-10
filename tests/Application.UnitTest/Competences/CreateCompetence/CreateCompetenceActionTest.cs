using Application.Competences.CreateCompetence;
using Application.UnitTest.Configurators;
using Domain.Abstract;
using Domain.Competences;
using Domain.Competences.DTOs;
using MediatR;
using Moq;

namespace Application.UnitTest.Competences.CreateCompetence
{
    public class CreateCompetenceActionTest
    {
        private readonly CreateCompetenceAction _action;

        public CreateCompetenceActionTest()
        {
            var mapper = MapperConfigurator.ConfigureCompetenceProfile();

            var validator = new CreateCompetenceValidator();

            var sender = new Mock<ISender>();
            sender.Setup(x => x.Send(It.IsAny<CreateCompetenceCommand>(), default))
                .ReturnsAsync(Result<Competence>.Success());

            _action = new(sender.Object,validator, mapper);
        }

        [Fact]
        public async Task Execute_ShouldReturnFailure_WhenRequestIsNotValid()
        {
            //Arrange
            var request = new CreateCompetenceRequest();


            //Act
            var result = await _action.Execute(request);

            //Assert
            Assert.True(result.IsFailure);
        }

        [Fact]
        public async Task Execute_ShouldReturnSuccess()
        {
            //Arrange
            var request = new CreateCompetenceRequest
            {
                Name = "Test",
                Description = "Test",
                SubjectId = Guid.NewGuid().ToString(),
            };


            //Act
            var result = await _action.Execute(request);

            //Assert
            Assert.True(result.IsSucess);
        }
    }
}
