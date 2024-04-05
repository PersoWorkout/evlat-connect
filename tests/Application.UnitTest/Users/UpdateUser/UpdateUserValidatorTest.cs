using Application.Users.UpdateUser;
using Domain.Users.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UnitTest.Users.UpdateUser
{
    public class UpdateUserValidatorTest
    {
        private readonly UpdateUserValidator _validator = new();

        [Fact]
        public void Validate_ShouldReturnNotValid_WhenClassIdIsNotValid()
        {
            //Arrange
            var request = new UpdateUserRequest
            {
                ClassId = "Invalid-Guid"
            };

            //Act
            var validation = _validator.Validate(request);

            //Assert
            Assert.False(validation.IsValid);
        }

        [Fact]
        public void Validate_ShouldReturnNotValid_WhenPasswordIsNotValid()
        {
            //Arrange
            var request = new UpdateUserRequest
            {
                Password = "password"
            };

            //Act
            var validation = _validator.Validate(request);

            //Assert
            Assert.False(validation.IsValid);
        }

        [Fact]
        public void Validate_ShouldReturnNotValid_WhenPasswordConfirmationIsNotValid()
        {
            //Arrange
            var request = new UpdateUserRequest
            {
                Password = "Password123!",
                PasswordConfirmation = "password!"
            };

            //Act
            var validation = _validator.Validate(request);

            //Assert
            Assert.False(validation.IsValid);
        }

        [Fact]
        public void Validate_ShouldReturnValid()
        {
            //Arrange
            var request = new UpdateUserRequest
            {
                Firstname = "another_firstname"
            };

            //Act
            var validation = _validator.Validate(request);

            //Assert
            Assert.True(validation.IsValid);
        }
    }
}
