using Application.Classes.CreateClass;
using Domain.Classes;
using Domain.Classes.DTOs;
using System.Xml.Linq;

namespace Application.UnitTest.Classes.CreateClass
{
    public class CreateClassValidatorTest
    {
        private readonly CreateClassValidator _validator = new();

        private const string Name = "First Class";
        private const string Promotion = "2024";
        private const ClassType Type = ClassType.Boy;
        private readonly Guid ProfessorId = Guid.NewGuid();

        [Fact]
        public void Validate_ShouldReturnFalse_WhenNameIsEmpty()
        {
            //Arrange
            var request = new CreateClassRequest
            {
                Name = "",
                Promotion = Promotion,
                Type = Type.GetHashCode(),
                ProfessorId = ProfessorId.ToString()
            };

            //Act
            var validation = _validator.Validate(request);

            //Assert
            Assert.False(validation.IsValid);
        }

        [Fact]
        public void Validate_ShouldReturnFalse_WhenPromotionIsEmpty()
        {
            //Arrange
            var request = new CreateClassRequest
            {
                Name = Name,
                Promotion = "",
                Type = Type.GetHashCode(),
                ProfessorId = ProfessorId.ToString()
            };

            //Act
            var validation = _validator.Validate(request);

            //Assert
            Assert.False(validation.IsValid);
        }

        [Fact]
        public void Validate_ShouldReturnFalse_WhenTypeIsNotDefined()
        {
            //Arrange
            var request = new CreateClassRequest
            {
                Name = Name,
                Promotion = Promotion,
                Type = 25,
                ProfessorId = ProfessorId.ToString()
            };

            //Act
            var validation = _validator.Validate(request);

            //Assert
            Assert.False(validation.IsValid);
        }

        [Fact]
        public void Validate_ShouldReturnFalse_WhenProfessorIdIsEmpty()
        {
            //Arrange
            var request = new CreateClassRequest
            {
                Name = Name,
                Promotion = Promotion,
                Type = Type.GetHashCode(),
                ProfessorId = ""
            };

            //Act
            var validation = _validator.Validate(request);

            //Assert
            Assert.False(validation.IsValid);
        }

        [Fact]
        public void Validate_ShouldReturnFalse_WhenProfessorIdIsInvalidGuid()
        {
            //Arrange
            var request = new CreateClassRequest
            {
                Name = Name,
                Promotion = Promotion,
                Type = Type.GetHashCode(),
                ProfessorId = "invalid-guid"
            };

            //Act
            var validation = _validator.Validate(request);

            //Assert
            Assert.False(validation.IsValid);
        }

        [Fact]
        public void Validate_ShouldReturnTrue_WhenAllIsValid()
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
            var validation = _validator.Validate(request);

            //Assert
            Assert.True(validation.IsValid);
        }
    }
}
