using Application.Classes.CreateClass;
using Application.UnitTest.Configurators;
using Application.UnitTest.Fakers.Repositories;
using Domain.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UnitTest.Classes.CreateClass
{
    public class CreateClassHandlerTest
    {
        private readonly CreateClassHandler _handler;

        public CreateClassHandlerTest()
        {
            var mapper = MapperConfigurator.ConfigureClassProfile();

            var repository = new FakeClassRepository();
            
            _handler = new(repository, mapper);
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccess()
        {
            //Arrange
            var command = new CreateClassCommand
            {
                Name = "Class Test",
                Promotion = "2024",
                Type = ClassType.Boy,
                ProfessorId = Guid.NewGuid()
            };

            //Act
            var result = await _handler.Handle(command, default);

            //Assert
            Assert.True(result.IsSucess);
        }
    }
}
