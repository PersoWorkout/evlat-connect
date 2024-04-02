using Domain.Users;
using Domain.Users.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UnitTests.Users
{
    public class UserTest
    {
        private const string Firstname = "John";
        private const string Lastname = "Doe";
        private const string Username = "john.doe";
        private const UserRole Role = UserRole.Student;
        private PasswordValueObject Password = PasswordValueObject.Create("Password123!").Data!;
        private PhoneNumberValueObject PhoneNumber = PhoneNumberValueObject.Create("0601010101").Data!;

        [Fact]
        public void UpdateByUser_ShouldUpdateNoFields_WhenValuesAreNull()
        {
            //Arrange
            var user = new User(Firstname, Lastname, Username, Role, Password, PhoneNumber);

            //Act
            user.UpdateByUser();

            //Assert
            Assert.Equal(Firstname, user.Firstname);
            Assert.Equal(Lastname, user.Lastname);
            Assert.Equal(Username, user.Username);
            Assert.Equal(Role, user.Role);
            Assert.True(Equals(Password, user.Password));
            Assert.True(Equals(PhoneNumber, user.PhoneNumber));
        }

        [Fact]
        public void UpdateByUser_ShouldUpdateFields()
        {
            //Arrange
            const string NewFirstname = "john.doe2";
            var NewPassword = PasswordValueObject.Create("NewPassword123!").Data!;

            var user = new User(Firstname, Lastname, Username, Role, Password, PhoneNumber);

            //Act
            user.UpdateByUser(firstname: NewFirstname, password: NewPassword);

            //Assert
            Assert.Equal(NewFirstname, user.Firstname);
            Assert.Equal(Lastname, user.Lastname);
            Assert.Equal(Username, user.Username);
            Assert.Equal(Role, user.Role);
            Assert.True(Equals(NewPassword, user.Password));
            Assert.True(Equals(PhoneNumber, user.PhoneNumber));
        }

        [Fact]
        public void UpdateByAdmin_ShouldUpdateFields()
        {
            //Arrange
            const string NewFirstname = "john.doe2";
            const UserRole NewRole = UserRole.Professeur;

            var user = new User(Firstname, Lastname, Username, Role, Password, PhoneNumber);

            //Act
            user.UpdateByAdmin(firstname: NewFirstname, role: NewRole);

            //Assert
            Assert.Equal(NewFirstname, user.Firstname);
            Assert.Equal(Lastname, user.Lastname);
            Assert.Equal(Username, user.Username);
            Assert.Equal(NewRole, user.Role);
            Assert.True(Equals(Password, user.Password));
            Assert.True(Equals(PhoneNumber, user.PhoneNumber));
        }
    }
}
