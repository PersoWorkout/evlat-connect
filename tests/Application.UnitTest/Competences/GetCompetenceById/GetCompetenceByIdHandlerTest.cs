using Application.Competences.GetCompetenceById;
using Application.UnitTest.Fakers.Repositories;
using Domain.Competences;

namespace Application.UnitTest.Competences.GetCompetenceById
{
    public class GetCompetenceByIdHandlerTest
    {
        private readonly FakeCompetenceRepository _repository;

        private readonly GetCompetenceByIdHandler _handler;

        public GetCompetenceByIdHandlerTest()
        {
            _repository = new();

            _handler = new(_repository);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenCompetenceNotExist()
        {
            //Arrange
            _repository.ClearCompetences();

            var query = new GetCompetenceByIdQuery(Guid.NewGuid());

            //Act
            var result = await _handler.Handle(query, default);

            //Assert
            Assert.True(result.IsFailure);
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccess()
        {
            //Arrange
            _repository.ClearCompetences();

            var competence = new Competence
            {
                Name = "Test",
                Description = "Lorem ipsum",
                SubjectId = Guid.NewGuid()
            };
            _repository.Competences.Add(competence);

            var query = new GetCompetenceByIdQuery(competence.Id);

            //Act
            var result = await _handler.Handle(query, default);

            //Assert
            Assert.True(result.IsSucess);
        }
    }
}
