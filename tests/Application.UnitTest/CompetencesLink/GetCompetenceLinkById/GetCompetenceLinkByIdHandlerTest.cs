using Application.CompetencesLink.GetCompetenceLinkById;
using Application.UnitTest.Fakers.Repositories;
using Domain.CompetencesLinks;
using Domain.CompetencesLinks.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UnitTest.CompetencesLink.GetCompetenceLinkById
{
    public class GetCompetenceLinkByIdHandlerTest
    {
        private readonly FakeCompetenceLinkRepository _repository;

        private readonly GetCompetenceLinkByIdHandler _handler;

        public GetCompetenceLinkByIdHandlerTest()
        {
            _repository = new();

            _handler = new(_repository);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenCompetenceLinkNotFound()
        {
            //Arrange
            _repository.ClearCompetences();

            var request = new GetCompetenceLinkByIdQuery(Guid.NewGuid());

            //Act
            var result = await _handler.Handle(request, default);

            //Assert
            Assert.True(result.IsFailure);
            Assert.Contains(
                CompetenceLinkErrors.CompetenceLinkNotFound(
                    request.Id.ToString()), 
                result.Errors);
        }

        [Fact]
        public async Task Handle_ShouldReturnSucces()
        {
            //Arrange
            _repository.ClearCompetences();

            var competenceLink = new CompetenceLink("test", "test", LinkType.Article, Guid.NewGuid());
            _repository.CompetenceLinks.Add(competenceLink);

            var request = new GetCompetenceLinkByIdQuery(competenceLink.Id);

            //Act
            var result = await _handler.Handle(request, default);

            //Assert
            Assert.True(result.IsSucess);
        }
    }
}
