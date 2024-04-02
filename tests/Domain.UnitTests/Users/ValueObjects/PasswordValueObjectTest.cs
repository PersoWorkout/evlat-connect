using Domain.Users.Errors;
using Domain.Users.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UnitTests.Users.ValueObjects
{
    public class PasswordValueObjectTest
    {
        [Fact]
        public void Create_ShouldReturnFailure_WhenValueIsEmpty()
        {
            //Arrange
            string value = string.Empty;

            //Act
            var passwordValue = PasswordValueObject.Create(value); 

            //Assert
            Assert.True(passwordValue.IsFailure);
            Assert.Contains(PasswordErrors.Empty, passwordValue.Errors);
        }

        [Fact]
        public void Create_ShouldReturnFailure_WhenValueIsNotAValidPassword()
        {
            //Arrange
            const string Value = "invalid";

            //Act
            var passwordValue = PasswordValueObject.Create(Value);

            //Assert
            Assert.True(passwordValue.IsFailure);
            Assert.Contains(PasswordErrors.Invalid, passwordValue.Errors);
        }

        [Fact]
        public void Create_ShouldReturnSuccess_WhenValueIsValid()
        {
            //Arrange
            const string Value = "Password123!";

            //Act
            var passwordValue = PasswordValueObject.Create(Value);

            //Assert
            Assert.True(passwordValue.IsSucess);
            Assert.Equal(Value, passwordValue.Data!.Value);
        }
    }
}
