using Application.Classes.UpdateClasse;
using Domain.Classes.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UnitTest.Classes.UpdateClass
{
    public class UpdateClassValidatorTest
    {
        private readonly UpdateClassValidator _validator = new();

        [Fact]
        public void Validate_ShouldReturnFalse_WhenTypeIsNotValid()
        {
            //Arrange
            var request = new UpdateClassRequest
            {
                Type = 22
            };

            //Act
            var validation = _validator.Validate(request);

            //Assert
            Assert.False(validation.IsValid);
        }

        [Fact]
        public void Validate_ShouldReturnFalse_WhenProfessorIdIsNotValid()
        {
            //Arrange
            var request = new UpdateClassRequest
            {
                ProfessorId = "invalidGuid"
            };

            //Act
            var validation = _validator.Validate(request);

            //Assert
            Assert.False(validation.IsValid);
        }

        [Fact]
        public void Validate_ShouldReturnTrue_WhenOneFieldIsNotEmptyAndValid()
        {
            //Arrange
            var request = new UpdateClassRequest
            {
                ProfessorId = Guid.NewGuid().ToString()
            };

            //Act
            var validation = _validator.Validate(request);

            //Assert
            Assert.True(validation.IsValid);
        }
    }
}
