using Application.Users;
using Application.Users.CreateUser;
using AutoMapper;
using Domain.Users;
using Domain.Users.DTOs;
using Domain.Users.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UnitTest.Users
{
    public class UserProfileTest
    {
        private readonly IMapper _mapper;

        public UserProfileTest() 
        {
            _mapper = new MapperConfiguration(config =>
                config.AddProfile<UserProfile>())
                .CreateMapper();
        }

        private const string Firstname = "John";
        private const string Lastname = "Doe";
        private const UserRole Role = UserRole.Student;
        private const string Password = "Password123!";
        private const string PhoneNumber = "0601010101";

        [Fact]
        public void Map_ShouldMapCreateUserRequest_ToCreateUserCommand()
        {
            //Arrange
            var request = new CreateUserRequest
            {
                Firstname =Firstname,
                Lastname = Lastname,
                Role = Role.GetHashCode(),
                Password = Password,
                PhoneNumber = PhoneNumber
            };

            //Act
            var command = _mapper.Map<CreateUserCommand>(request);

            //Assert
            Assert.IsType<CreateUserCommand>(command);
            Assert.Equal(request.Firstname, command.Firstname);
            Assert.Equal(request.Lastname, command.Lastname);
            Assert.Equal(request.Role, command.Role.GetHashCode());
            Assert.Equal(request.Password, command.Password.Value);
            Assert.Equal(request.PhoneNumber, command.PhoneNumber.Value);
        }

        [Fact]
        public void Map_ShouldMapCreateUserCommand_ToUser()
        {
            //Arrange
            var command = new CreateUserCommand
            {
                Firstname = Firstname,
                Lastname = Lastname,
                Role = Role,
                Password = PasswordValueObject.Create(Password).Data!,
                PhoneNumber = PhoneNumberValueObject.Create(PhoneNumber).Data!
            };

            //Act
            var user = _mapper.Map<User>(command);

            //Assert
            //Assert
            Assert.IsType<User>(user);
            Assert.Equal(user.Firstname, command.Firstname);
            Assert.Equal(user.Lastname, command.Lastname);
            Assert.Equal(user.Role, command.Role);
            Assert.True(Equals(user.Password, command.Password));
            Assert.True(Equals(user.PhoneNumber, command.PhoneNumber));
        }

        [Fact]
        public void Map_ShouldMapUse_ToUserResponse()
        {
            //Arrange
            var user = new User
            {
                Firstname = Firstname,
                Lastname = Lastname,
                Role = Role,
                Password = PasswordValueObject.Create(Password).Data!,
                PhoneNumber = PhoneNumberValueObject.Create(PhoneNumber).Data!
            };

            //Act
            var response = _mapper.Map<UserResponse>(user);

            //Assert
            Assert.IsType<UserResponse>(response);
            Assert.Equal(user.Firstname, response.Firstname);
            Assert.Equal(user.Lastname, response.Lastname);
            Assert.Equal(user.Role.ToString(), response.Role);
            Assert.Equal(user.Password.Value, response.Password);
            Assert.Equal(user.PhoneNumber.Value, response.PhoneNumber);
        }
    }
}
