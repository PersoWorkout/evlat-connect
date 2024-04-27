using Application.CompetencesLink.GetCompetenceLinkById;
using Application.UnitTest.Configurators;
using Domain.Abstract;
using Domain.CompetencesLinks;
using Domain.CompetencesLinks.Errors;
using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UnitTest.CompetencesLink.GetCompetenceLinkById
{
    public class GetCompetenceLinkByIdActionTest
    {
        private readonly GetCompetenceLinkByIdAction _action;

        public GetCompetenceLinkByIdActionTest()
        {
            var mapper = MapperConfigurator.ConfigureCompetenceLinkProfile();
            var sender = new Mock<ISender>();
            sender
                .Setup(x => x.Send(
                    It.IsAny<GetCompetenceLinkByIdQuery>(),
                    default))
                .ReturnsAsync(
                    Result<CompetenceLink>.Success());

            _action = new(sender.Object, mapper);
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
