using Application.CompetencesLink.CreateCompetenceLink;
using Application.UnitTest.Configurators;
using Application.UnitTest.Fakers.Repositories;
using Domain.Competences;
using Domain.CompetencesLinks;
using Domain.CompetencesLinks.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UnitTest.CompetencesLink.CreateCompetenceLink
{
    public class CreateCompetenceLinkHandlerTest
    {
        private readonly FakeCompetenceRepository _competenceRepository;

        private readonly CreateCompetenceLinkHandler _handler;

        public CreateCompetenceLinkHandlerTest()
        {
            var mapper = MapperConfigurator.ConfigureCompetenceLinkProfile();
            _competenceRepository = new();
            var competenceLinkRepository = new FakeCompetenceLinkRepository();

            _handler = new(competenceLinkRepository, _competenceRepository, mapper);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenCompetenceNotExtist()
        {
            //Arrange
            _competenceRepository.ClearCompetences();

            var command = new CreateCompetenceLinkCommand()
            {
                CompetenceId = Guid.NewGuid(),
                Name = "Test",
                Path = "Test",
                Type = LinkType.Article,
            };

            //Act
            var result = await _handler.Handle(command, default);

            //Assert
            Assert.True(result.IsFailure);
            Assert.Contains(
                CompetenceLinkErrors.CompetenceNotFound(command.CompetenceId.ToString()), 
                result.Errors);
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccess()
        {
            //Arrange
            _competenceRepository.ClearCompetences();

            var competence = new Competence
            {
                Name = "Test",
                Description = "Test",
                SubjectId = Guid.NewGuid(),
            };
            _competenceRepository.Competences.Add(competence);

            var command = new CreateCompetenceLinkCommand()
            {
                CompetenceId = competence.Id,
                Name = "Test",
                Path = "Test",
                Type = LinkType.Article,
            };

            //Act
            var result = await _handler.Handle(command, default);

            //Assert
            Assert.True(result.IsSucess);
        }
    }
}
