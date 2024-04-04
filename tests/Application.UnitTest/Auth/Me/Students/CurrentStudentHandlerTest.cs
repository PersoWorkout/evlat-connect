using Application.Auth.Me.Students;
using Application.UnitTest.Configurators;
using Application.UnitTest.Fakers.Repositories;
using Domain.Auth;
using Domain.Auth.DTOs;
using Domain.Auth.ValueObjects;
using Domain.Classes;
using Domain.Users;
using Domain.Users.Errors;
using Domain.Users.ValueObjects;

namespace Application.UnitTest.Auth.Me.Students
{
    public class CurrentStudentHandlerTest
    {
        private readonly FakeAuthRepository _authRepository;
        private readonly FakeUserRepository _userRepository;
        private readonly FakeClassRepository _classRepository;
        
        private readonly CurrentStudentHandler _handler;

        public CurrentStudentHandlerTest()
        {
            _authRepository = new FakeAuthRepository();
            _userRepository = new FakeUserRepository();
            _classRepository = new FakeClassRepository();

            var mapper = MapperConfigurator.ConfigureAll();

            _handler = new(
                _authRepository, 
                _userRepository, 
                _classRepository, 
                mapper);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenUserIsNotConnected()
        {
            //Arrange
            _authRepository.ClearLists();

            var query = new CurrentStudentQuery(new TokenValueObject());

            //Act
            var result = await _handler.Handle(query, default);

            //Assert
            Assert.True(result.IsFailure);
            Assert.Contains(AuthErrors.SessionExpired, result.Errors);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenUserNotFound()
        {
            //Arrange
            _authRepository.ClearLists();
            _userRepository.ClearUsers();

            var session = new Session(Guid.NewGuid());
            _authRepository.Sessions.Add(session);

            var query = new CurrentStudentQuery(session.Token);

            //Act
            var result = await _handler.Handle(query, default);

            //Assert
            var expectedErrors = UserErrors.StudentNotFound(
                session.UserId.ToString());

            Assert.True(result.IsFailure);
            Assert.Contains(expectedErrors, result.Errors);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenUserIsNotAStudent()
        {
            //Arrange
            _authRepository.ClearLists();
            _userRepository.ClearUsers();

            var user = new User
            {
                Firstname = "John",
                Lastname = "Doe",
                Username = "john.doe",
                Password = PasswordValueObject.Create("Password123!").Data!,
                PhoneNumber = PhoneNumberValueObject.Create("0601010101").Data!,
                Role = UserRole.Professeur
            };
            _userRepository.Users.Add(user);
            _authRepository.Users.Add(user);

            var session = new Session(user.Id);
            _authRepository.Sessions.Add(session);

            var query = new CurrentStudentQuery(session.Token);

            //Act
            var result = await _handler.Handle(query, default);

            //Assert
            var expectedErrors = UserErrors.StudentNotFound(
                session.UserId.ToString());

            Assert.True(result.IsFailure);
            Assert.Contains(expectedErrors, result.Errors);
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccess_WhenUserHasNotClass()
        {
            //Arrange
            _authRepository.ClearLists();
            _userRepository.ClearUsers();

            var user = new User
            {
                Firstname = "John",
                Lastname = "Doe",
                Username = "john.doe",
                Password = PasswordValueObject.Create("Password123!").Data!,
                PhoneNumber = PhoneNumberValueObject.Create("0601010101").Data!,
                Role = UserRole.Student
            };
            _userRepository.Users.Add(user);
            _authRepository.Users.Add(user);

            var session = new Session(user.Id);
            _authRepository.Sessions.Add(session);

            var query = new CurrentStudentQuery(session.Token);

            //Act
            var result = await _handler.Handle(query, default);

            //Assert
            Assert.True(result.IsSucess);
            Assert.IsType<CurrentStudentResponse>(result.Data);
            Assert.Null(result.Data.Class);
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccess_WhenUserHasClass()
        {
            //Arrange
            _authRepository.ClearLists();
            _userRepository.ClearUsers();
            _classRepository.ResetClasses();

            var classEntity = new Class
            {
                Name = "Class Test",
                Promotion = "2024",
                Type = ClassType.Mixte,
                ProfessorId = Guid.NewGuid()
            };
            _classRepository.Classes.Add(classEntity);

            var user = new User
            {
                Firstname = "John",
                Lastname = "Doe",
                Username = "john.doe",
                Password = PasswordValueObject.Create("Password123!").Data!,
                PhoneNumber = PhoneNumberValueObject.Create("0601010101").Data!,
                Role = UserRole.Student,
                ClassId = classEntity.Id
            };
            _userRepository.Users.Add(user);
            _authRepository.Users.Add(user);

            var session = new Session(user.Id);
            _authRepository.Sessions.Add(session);

            var query = new CurrentStudentQuery(session.Token);

            //Act
            var result = await _handler.Handle(query, default);

            //Assert
            Assert.True(result.IsSucess);
            Assert.IsType<CurrentStudentResponse>(result.Data);
            Assert.Equal(classEntity.Id.ToString(), result.Data.Class!.Id);
        }
    }
}
