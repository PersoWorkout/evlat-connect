using Application.Auth.Login;
using Domain.Auth.DTOs;

namespace Application.UnitTest.Auth.Login
{
    public class LoginValidatorTest
    {
        private readonly LoginValidator _validator = new();

        [Fact]
        public void Validate_ShouldReturnFailure_WhenUsernameIsEmpty()
        {
            //Arrange
            var request = new LoginRequest
            {
                Username = "",
                Password = "Password123!",
                PasswordValidation = "Password123!"
            };

            //Act
            var validation = _validator.Validate(request);

            //Assert
            Assert.False(validation.IsValid);
        }

        [Fact]
        public void Validate_ShouldReturnFailure_WhenPasswordIsEmpty()
        {
            //Arrange
            var request = new LoginRequest
            {
                Username = "John",
                Password = "",
                PasswordValidation = "Password123!"
            };

            //Act
            var validation = _validator.Validate(request);

            //Assert
            Assert.False(validation.IsValid);
        }

        [Fact]
        public void Validate_ShouldReturnFailure_WhenPasswordNotValid()
        {
            //Arrange
            var request = new LoginRequest
            {
                Username = "John",
                Password = "password",
                PasswordValidation = "password"
            };

            //Act
            var validation = _validator.Validate(request);

            //Assert
            Assert.False(validation.IsValid);
        }

        [Fact]
        public void Validate_ShouldReturnFailure_WhenPasswordValidationIsNotCorrect()
        {
            //Arrange
            var request = new LoginRequest
            {
                Username = "John",
                Password = "Password123!",
                PasswordValidation = "password"
            };

            //Act
            var validation = _validator.Validate(request);

            //Assert
            Assert.False(validation.IsValid);
        }

        [Fact]
        public void Validate_ShouldReturnSuccess_WhenAllAreCorrect()
        {
            //Arrange
            var request = new LoginRequest
            {
                Username = "John",
                Password = "Password123!",
                PasswordValidation = "Password123!"
            };

            //Act
            var validation = _validator.Validate(request);

            //Assert
            Assert.True(validation.IsValid);
        }
    }
}
