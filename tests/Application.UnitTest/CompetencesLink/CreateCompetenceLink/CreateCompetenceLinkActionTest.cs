using Application.CompetencesLink.CreateCompetenceLink;
using Application.UnitTest.Configurators;
using Domain.Abstract;
using Domain.CompetencesLinks;
using Domain.CompetencesLinks.DTOs;
using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UnitTest.CompetencesLink.CreateCompetenceLink
{
    public class CreateCompetenceLinkActionTest
    {
        private readonly CreateCompetenceLinkAction _action;

        public CreateCompetenceLinkActionTest()
        {
            var validator = new CreateCompetenceLinkValidator();
            var mapper = MapperConfigurator.ConfigureCompetenceLinkProfile();

            var sender = new Mock<ISender>();
            sender
                .Setup(x => x.Send(
                    It.IsAny<CreateCompetenceLinkCommand>(),
                    default))
                .ReturnsAsync(Result<CompetenceLink>.Success(
                    new CompetenceLink(
                        "test", 
                        "test", 
                        LinkType.Article, 
                        Guid.NewGuid())));

            _action = new(sender.Object, validator, mapper);
        }

        [Fact]
        public async Task Execute_ShouldReturnFailure_WhenRequestIsNotValid()
        {
            //Arrange
            var request = new AddCompetenceLinkRequest
            {
                CompetenceId = "invalid-guid",
                Name = "",
                Path = "",
                Type = 0
            };

            //Act
            var result = await _action.Execute(request);

            //Assert
            Assert.True(result.IsFailure);
        }

        [Fact]
        public async Task Execute_ShouldReturnSuccess()
        {
            //Arrange
            var request = new AddCompetenceLinkRequest
            {
                CompetenceId = Guid.NewGuid().ToString(),
                Name = "Test",
                Path = "http://test.com",
                Type = (int)LinkType.Article,
            };

            //Act
            var result = await _action.Execute(request);

            //Assert
            Assert.True(result.IsSucess);
        }
    }
}
