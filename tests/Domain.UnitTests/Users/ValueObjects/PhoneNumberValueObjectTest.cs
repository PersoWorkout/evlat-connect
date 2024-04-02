using Domain.Users.Errors;
using Domain.Users.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UnitTests.Users.ValueObjects
{
    public class PhoneNumberValueObjectTest
    {
        [Fact]
        public void Create_ShouldReturnFailure_WhenValueIsEmpty()
        {
            //Arrange
            string value = string.Empty;

            //Act
            var valueObject = PhoneNumberValueObject.Create(value);

            //Assert
            Assert.True(valueObject.IsFailure);
            Assert.Contains(PhoneNumberErrors.Empty, valueObject.Errors);
        }

        [Fact]
        public void Create_ShouldReturnFailure_WhenValueIsInvalid()
        {
            //Arrange
            const string Value = "invalid";

            //Act
            var valueObject = PhoneNumberValueObject.Create(Value);

            //Assert
            Assert.True(valueObject.IsFailure);
            Assert.Contains(PhoneNumberErrors.Invalid, valueObject.Errors);
        }

        [Fact]
        public void Create_ShouldReturnSuccess_WhenValueIsInternationalFormat()
        {
            //Arrange
            const string Value = "+33601010101";

            //Act
            var valueObject = PhoneNumberValueObject.Create(Value);

            //Assert
            Assert.True(valueObject.IsSucess);
            Assert.Equal(Value, valueObject.Data!.Value);
        }

        [Fact]
        public void Create_ShouldReturnSuccess_WhenValueIsFrenchFormat()
        {
            //Arrange
            const string Value = "0601010101";

            //Act
            var valueObject = PhoneNumberValueObject.Create(Value);

            //Assert
            Assert.True(valueObject.IsSucess);
            Assert.Equal(Value, valueObject.Data!.Value);
        }
    }
}