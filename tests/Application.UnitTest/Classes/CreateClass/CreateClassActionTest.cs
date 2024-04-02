using Application.Classes.CreateClass;
using Application.UnitTest.Configurators;
using AutoMapper;
using Domain.Abstract;
using Domain.Classes;
using Domain.Classes.DTOs;
using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UnitTest.Classes.CreateClass
{
    public class CreateClassActionTest
    {
        private readonly CreateClassAction _action;

        public CreateClassActionTest() 
        {
            var mapper = MapperConfigurator.ConfigureClassProfile();

            var validator = new CreateClassValidator();

            var sender = new Mock<ISender>();
            sender.Setup(x => x.Send(It.IsAny<CreateClassCommand>(), default))
                .ReturnsAsync(Result<Class>.Success(new Class()));

            _action = new(sender.Object, validator, mapper);
        }

        [Fact]
        public async Task Execute_ShouldReturnFailure_WhenPayloasIsNotValid()
        {
            //Arrange
            var request = new CreateClassRequest
            {
                Name = "",
                Promotion = "",
                Type = 0,
                ProfessorId = ""
            };

            //Act
            var result = await _action.Execute(request);

            //Assert
            Assert.True(result.IsFailure);
        }

        [Fact]
        public async Task Execute_ShouldReturnSuccess_WhenPayloadIsValid()
        {
            //Arrange
            var request = new CreateClassRequest
            {
                Name = "Class Test",
                Promotion = "2024",
                Type = ClassType.Boy.GetHashCode(),
                ProfessorId = Guid.NewGuid().ToString()
            };

            //Act
            var result = await _action.Execute(request);

            //Assert
            Assert.True(result.IsSucess);
        }
    }
}
