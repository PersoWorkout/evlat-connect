using Application.UnitTest.Configurators;
using Application.Users.CreateUser;
using Domain.Abstract;
using Domain.Users;
using Domain.Users.DTOs;
using Domain.Users.Errors;
using MediatR;
using Moq;

namespace Application.UnitTest.Users.CreateUser
{
    public class CreateUserActionTest
    {
        private readonly Mock<ISender> _sender;
        private readonly CreateUserAction _action;

        public CreateUserActionTest() 
        {
            _sender = new Mock<ISender>();
            _sender.Setup(x => x.Send(It.IsAny<CreateUserCommand>(), default))
                .ReturnsAsync(Result<User>.Success(new User()));

            var mapper = MapperConfigurator.ConfigureUserProfile();
            var validator = new CreateUserValidator();

            _action = new(_sender.Object, validator, mapper);
        }

        [Fact]
        public async Task Execute_ShouldReturnFailure_WhenRequestIsNotValid()
        {
            //Arrange
            var request = CreateInvalidRequest();

            //Act
            var result = await _action.Execute(request);

            //Assert
            Assert.True(result.IsFailure);
            Assert.Contains(UserErrors.RoleNotDefined, result.Errors);
            Assert.Contains(PasswordErrors.Invalid, result.Errors);
            Assert.Contains(PhoneNumberErrors.Invalid, result.Errors);
        }

        [Fact]
        public async Task Execute_ShouldReturnSuccess_WhenRequestIsValid()
        {
            //Arrange
            var request = CreateValidRequest();

            //Act
            var result = await _action.Execute(request);

            //Assert
            Assert.True(result.IsSucess);
            Assert.IsType<UserResponse>(result.Data);
        }


        private CreateUserRequest CreateInvalidRequest()
        {
            return new CreateUserRequest
            {
                Firstname = "John",
                Lastname = "Doe",
                Role = 24,
                Password = "Invalid",
                PhoneNumber = "invalid"
            };
        }

        private CreateUserRequest CreateValidRequest()
        {
            return new CreateUserRequest
            {
                Firstname = "John",
                Lastname = "Doe",
                Role = UserRole.Student.GetHashCode(),
                Password = "Password123!",
                PhoneNumber = "+33601010101",
                ClassId = Guid.NewGuid().ToString()
            };
        }
    }
}
