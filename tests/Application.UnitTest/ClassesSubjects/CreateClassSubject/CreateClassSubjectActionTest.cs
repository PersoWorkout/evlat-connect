using Application.ClassesSubjects.CreateClassSubject;
using Application.UnitTest.Configurators;
using Domain.Abstract;
using Domain.ClassesSubjects;
using Domain.ClassesSubjects.DTOs;
using MediatR;
using Moq;

namespace Application.UnitTest.ClassesSubjects.CreateClassSubject
{
    public class CreateClassSubjectActionTest
    {
        private readonly CreateClassSubjectAction _action;

        public CreateClassSubjectActionTest()
        {
            var mapper = MapperConfigurator
                .ConfigureClassSubjectProfile();

            var validator = new CreateClassSubjectValidator();

            var sender = new Mock<ISender>();
            sender.Setup(x => x.Send(It.IsAny<CreateClassSubjectCommand>(), default))
                .ReturnsAsync(Result<ClassSubject>.Success(new ClassSubject()));

            _action = new(sender.Object, validator, mapper);
        }

        [Fact]
        public async Task Execute_ShouldReturnFailure_WhenClassIdIsNotValid()
        {
            //Arrange
            const string classId = "invalid-guid";
            string subjectId = Guid.NewGuid().ToString();

            var request = new CreateClassSubjectRequest()
            {
                StartedAt = DateTime.Now,
                FinishedAt = DateTime.Now.AddHours(1)
            };

            //Act
            var result = await _action.Execute(classId, subjectId, request);

            //Assert
            Assert.True(result.IsFailure);
        }

        [Fact]
        public async Task Execute_ShouldReturnFailure_WhenSubjectIdIsNotValid()
        {
            //Arrange
            string classId = Guid.NewGuid().ToString();
            const string subjectId = "invalid-guid";

            var request = new CreateClassSubjectRequest()
            {
                StartedAt = DateTime.Now,
                FinishedAt = DateTime.Now.AddHours(1)
            };

            //Act
            var result = await _action.Execute(classId, subjectId, request);

            //Assert
            Assert.True(result.IsFailure);
        }

        [Fact]
        public async Task Execute_ShouldReturnFailure_WhenRequestIsNotValid()
        {
            //Arrange
            string classId = Guid.NewGuid().ToString();
            string subjectId = Guid.NewGuid().ToString();

            var request = new CreateClassSubjectRequest()
            {
                StartedAt = DateTime.Now.AddHours(1),
                FinishedAt = DateTime.Now
            };

            //Act
            var result = await _action.Execute(classId, subjectId, request);

            //Assert
            Assert.True(result.IsFailure);
        }

        [Fact]
        public async Task Execute_ShouldReturnSuccess()
        {
            //Arrange
            string classId = Guid.NewGuid().ToString();
            string subjectId = Guid.NewGuid().ToString();

            var request = new CreateClassSubjectRequest()
            {
                StartedAt = DateTime.Now,
                FinishedAt = DateTime.Now.AddHours(1)
            };

            //Act
            var result = await _action.Execute(classId, subjectId, request);

            //Assert
            Assert.True(result.IsSucess);
        }
    }
}
