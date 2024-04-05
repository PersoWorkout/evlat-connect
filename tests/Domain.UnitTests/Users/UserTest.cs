using Domain.Users;
using Domain.Users.ValueObjects;

namespace Domain.UnitTests.Users
{
    public class UserTest
    {
        private const string Firstname = "John";
        private const string Lastname = "Doe";
        private const string Username = "john.doe";
        private const UserRole Role = UserRole.Student;
        private readonly PasswordValueObject Password = PasswordValueObject.Create("Password123!").Data!;
        private readonly PhoneNumberValueObject PhoneNumber = PhoneNumberValueObject.Create("0601010101").Data!;

        [Fact]
        public void Update_ShouldUpdateFields()
        {
            //Arrange
            const string NewFirstname = "john2";
            const string NewUsername = "john.doe2";

            var user = new User
            {
                Firstname = Firstname,
                Lastname = Lastname,
                Username = Username,
                Role = Role,
                Password = Password,
                PhoneNumber = PhoneNumber
            };

            //Act
            user.Update(firstname: NewFirstname, username: NewUsername);

            //Assert
            Assert.Equal(NewFirstname, user.Firstname);
            Assert.Equal(Lastname, user.Lastname);
            Assert.Equal(NewUsername, user.Username);
            Assert.True(Equals(Password, user.Password));
            Assert.True(Equals(PhoneNumber, user.PhoneNumber));
        }
    }
}
