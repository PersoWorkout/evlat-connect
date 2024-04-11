using Application.Competences.UpdateCompetence;
using Application.UnitTest.Configurators;
using AutoMapper;
using Domain.Abstract;
using Domain.Competences;
using Domain.Competences.DTOs;
using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UnitTest.Competences.UpdateCompetence
{
    public class UpdateCompetenceActionTest
    {
        private readonly UpdateCompetenceAction _action;

        public UpdateCompetenceActionTest()
        {
            var validator = new UpdateCompetenceValidator();

            var mapper = MapperConfigurator.ConfigureCompetenceProfile();

            var sender = new Mock<ISender>();
            sender.Setup(x => x.Send(It.IsAny<UpdateCompetenceCommand>(), default))
                .ReturnsAsync(Result<Competence>.Success(new Competence()));

            _action = new(sender.Object, validator, mapper);
        }

        [Fact]
        public async Task Execute_ShouldReturnFailure_WhenIdIsNotValid()
        {
            //Arrange
            string id = "invalid-guid";

            var request = new UpdateCompetenceRequest
            {
                Name = "Test",
                Description = "Test",
                SubjectId = Guid.NewGuid().ToString()
            };

            //Act
            var result = await _action.Execute(id, request);

            //Assert
            Assert.True(result.IsFailure);
        }

        [Fact]
        public async Task Execute_ShouldReturnFailure_WhenRequestIsNotValid()
        {
            //Arrange
            string id = Guid.NewGuid().ToString();

            var request = new UpdateCompetenceRequest
            {
                Name = "Test",
                SubjectId = "invalid-guid"
            };

            //Act
            var result = await _action.Execute(id, request);

            //Assert
            Assert.True(result.IsFailure);
        }

        [Fact]
        public async Task Execute_ShouldReturnSuccess()
        {
            //Arrange
            string id = Guid.NewGuid().ToString();

            var request = new UpdateCompetenceRequest
            {
                Name = "Test",
            };

            //Act
            var result = await _action.Execute(id, request);

            //Assert
            Assert.True(result.IsSucess);
        }
    }
}
