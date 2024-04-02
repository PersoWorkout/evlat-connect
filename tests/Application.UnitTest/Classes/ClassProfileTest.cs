using Application.Classes.CreateClass;
using Application.UnitTest.Configurators;
using AutoMapper;
using Domain.Classes;
using Domain.Classes.DTOs;

namespace Application.UnitTest.Classes
{
    public class ClassProfileTest
    {
        private readonly IMapper _mapper = MapperConfigurator.ConfigureClassProfile();

        private const string Name = "First Class";
        private const string Promotion = "2024";
        private const ClassType Type = ClassType.Boy;
        private readonly Guid ProfessorId = Guid.NewGuid();


        [Fact]
        public void Map_ShouldMapCreateClassRequest_ToCreateClassCommand()
        {
            //Arrange
            var request = new CreateClassRequest
            {
                Name = Name,
                Promotion = Promotion,
                Type = Type.GetHashCode(),
                ProfessorId = ProfessorId.ToString()
            };

            //Act
            var command = _mapper.Map<CreateClassCommand>(request);

            //Assert
            Assert.IsType<CreateClassCommand>(command);
            Assert.Equal(ProfessorId, command.ProfessorId);
            Assert.Equal(Type, command.Type);
        }

        [Fact]
        public void Map_ShouldMapCreateClassCommand_ToClass()
        {
            //Arrange
            var command = new CreateClassCommand
            {
                Name = Name,
                Promotion = Promotion,
                Type = Type,
                ProfessorId = ProfessorId
            };

            //Act
            var entity = _mapper.Map<Class>(command);

            //Assert
            Assert.IsType<Class>(entity);
            Assert.Equal(ProfessorId, entity.ProfessorId);
            Assert.Equal(Type, entity.Type);
        }

        [Fact]
        public void Map_ShouldMapClass_ToClassResponse()
        {
            //Arrange
            var entity = new Class
            {
                Id = Guid.NewGuid(),
                Name = Name,
                Promotion = Promotion,
                Type = Type,
                ProfessorId = ProfessorId
            };

            //Act
            var response = _mapper.Map<ClassResponse>(entity);

            //Assert
            Assert.IsType<ClassResponse>(response);
            Assert.Equal(ProfessorId.ToString(), response.ProfessorId);
            Assert.Equal(Type.ToString(), response.Type);
        }
    }
}
