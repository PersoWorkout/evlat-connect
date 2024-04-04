using Application.Auth.Me.Professors;
using Application.Auth.Me.Students;
using Application.UnitTest.Configurators;
using Application.UnitTest.Fakers.Repositories;
using Domain.Auth.ValueObjects;
using Domain.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Users.Errors;
using Domain.Users.ValueObjects;
using Domain.Users;
using Domain.Auth.DTOs;
using Domain.Classes;

namespace Application.UnitTest.Auth.Me.Professors
{
    public class CurrentProfessorHandlerTest
    {
        private readonly FakeAuthRepository _authRepository;
        private readonly FakeUserRepository _userRepository;
        private readonly FakeClassRepository _classRepository;

        private readonly CurrentProfessorHandler _handler;

        public CurrentProfessorHandlerTest()
        {
            _authRepository = new();
            _userRepository = new();
            _classRepository = new();

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

            var query = new CurrentProfessorQuery(new TokenValueObject());

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

            var query = new CurrentProfessorQuery(session.Token);

            //Act
            var result = await _handler.Handle(query, default);

            //Assert
            var expectedErrors = UserErrors.ProfessorNotFound(
                session.UserId.ToString());

            Assert.True(result.IsFailure);
            Assert.Contains(expectedErrors, result.Errors);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenUserIsNotAProdfessor()
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

            var query = new CurrentProfessorQuery(session.Token);

            //Act
            var result = await _handler.Handle(query, default);

            //Assert
            var expectedErrors = UserErrors.ProfessorNotFound(
                session.UserId.ToString());

            Assert.True(result.IsFailure);
            Assert.Contains(expectedErrors, result.Errors);
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccess()
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

            var classEntity = new Class
            {
                Name = "Class Test",
                Promotion = "2024",
                Type = ClassType.Mixte,
                ProfessorId = user.Id
            };
            _classRepository.Classes.Add(classEntity);

            var session = new Session(user.Id);
            _authRepository.Sessions.Add(session);

            var query = new CurrentProfessorQuery(session.Token);

            //Act
            var result = await _handler.Handle(query, default);

            //Assert
            Assert.True(result.IsSucess);
            Assert.IsType<CurrentProfessorResponse>(result.Data);
            Assert.NotEmpty(result.Data.Classes!);
        }
    }
}
