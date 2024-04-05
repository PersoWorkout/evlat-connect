using Application.UnitTest.Fakers.Repositories;
using Application.Users.UpdateUser;
using Domain.Classes;
using Domain.Classes.Errors;
using Domain.Users;
using Domain.Users.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UnitTest.Users.UpdateUser
{
    public class UpdateUserHandlerTest
    {
        private readonly FakeUserRepository _userRepository;
        private readonly FakeClassRepository _classRepository;

        private readonly UpdateUserHandler _handler;

        public UpdateUserHandlerTest()
        {
            _userRepository = new();
            _classRepository = new();

            _handler = new(_userRepository, _classRepository);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenUserNotExist()
        {
            //Arrange
            _userRepository.ClearUsers();
            _classRepository.ResetClasses();

            var command = new UpdateUserCommand
            {
                UserId = Guid.NewGuid()
            };

            //Act
            var result = await _handler.Handle(command, default);

            //Assert
            var expectedError = UserErrors.UserNotFound(
                command.UserId.ToString());

            Assert.True(result.IsFailure);
            Assert.Contains(expectedError, result.Errors);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenClassNotExist()
        {
            //Arrange
            _userRepository.ClearUsers();
            _classRepository.ResetClasses();

            var user = new User
            {
                Firstname = "John",
                Lastname = "Doe",
                Username = "john.doe",
                Role = UserRole.Student
            };
            _userRepository.Users.Add(user);

            var command = new UpdateUserCommand
            {
                UserId = user.Id,
                ClassId = Guid.NewGuid(),
            };

            //Act
            var result = await _handler.Handle(command, default);

            //Assert
            var expectedError = ClassErrors.ClassNotFound(
                command.ClassId!.Value.ToString());

            Assert.True(result.IsFailure);
            Assert.Contains(expectedError, result.Errors);
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccess()
        {
            //Arrange
            _userRepository.ClearUsers();
            _classRepository.ResetClasses();

            var user = new User
            {
                Firstname = "John",
                Lastname = "Doe",
                Username = "john.doe",
                Role = UserRole.Student
            };
            _userRepository.Users.Add(user);

            var classEntity = new Class
            {
                Name = "Class Test",
                Promotion = "2024",
                Type = ClassType.Mixte
            };
            _classRepository.Classes.Add(classEntity);

            var command = new UpdateUserCommand
            {
                UserId = user.Id,
                ClassId = classEntity.Id,
            };

            //Act
            var result = await _handler.Handle(command, default);

            //Assert
            Assert.True(result.IsSucess);
            Assert.Equal(classEntity.Id, user.ClassId);
        }
    }
}
