using Application.Classes.GetClassesByProfessor;
using Application.UnitTest.Configurators;
using Domain.Abstract;
using Domain.Classes;
using MediatR;
using Moq;

namespace Application.UnitTest.Classes.GetClassesByProfessor
{
    public class GetClassesByProfessorActionTest
    {
        private readonly GetClassesByProfessorAction _action;

        public GetClassesByProfessorActionTest()
        {
            var mapper = MapperConfigurator.ConfigureClassProfile();

            var sender = new Mock<ISender>();
            sender.Setup(x => x.Send(It.IsAny<GetClassesByProfessorQuery>(), default))
                .ReturnsAsync(Result<IEnumerable<Class>>.Success([]));

            _action = new(sender.Object, mapper);
        }

        [Fact]
        public async Task Execute_ShouldReturnFailure_WhenIdIsNotValid()
        {
            //Arrange
            const string id = "Invalid-Guid";

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
