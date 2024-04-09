using Application.ClassesSubjects.UpdateClassSubject;
using Application.UnitTest.Configurators;
using Domain.Abstract;
using Domain.ClassesSubjects;
using Domain.ClassesSubjects.DTOs;
using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UnitTest.ClassesSubjects.UpdateClassSubject
{
    public class UpdateClassSubjectActionTest
    {
        private readonly UpdateClassSubjectAction _action;

        public UpdateClassSubjectActionTest()
        {
            var sender = new Mock<ISender>();
            sender.Setup( x => x.Send(It.IsAny<UpdateClassSubjectCommand>(), default))
                .ReturnsAsync(Result<ClassSubject>.Success(new ClassSubject()));

            var mapper = MapperConfigurator.ConfigureClassSubjectProfile();

            _action = new(sender.Object, mapper);
        }

        [Fact]
        public async Task Execute_ShouldReturnFailure_WhenClassIdIsNotAValidGuid()
        {
            //Arrange
            string classId = "invalid-guid";
            string startedDate = DateTime.Now.ToString();

            var request = new UpdateClassSubjectRequest
            {
                FinishedAt = DateTime.Now,
                SubjectId = Guid.NewGuid().ToString(),
                Message = "Another Message"
            };

            //Act
            var result = await _action.Execute(classId, startedDate, request);

            //Assert
            Assert.True(result.IsFailure);
        }

        [Fact]
        public async Task Execute_ShouldReturnFailure_WhenStartedAtIsNotAValidDate()
        {
            //Arrange
            string classId = Guid.NewGuid().ToString();
            string startedDate = "invalid-date";

            var request = new UpdateClassSubjectRequest
            {
                FinishedAt = DateTime.Now,
                SubjectId = Guid.NewGuid().ToString(),
                Message = "Another Message"
            };

            //Act
            var result = await _action.Execute(classId, startedDate, request);

            //Assert
            Assert.True(result.IsFailure);
        }

        [Fact]
        public async Task Execute_ShouldReturnFailure_WhenSubjectIdIsNotAValidGuid()
        {
            //Arrange
            string classId = Guid.NewGuid().ToString();
            string startedDate = DateTime.Now.ToString();

            var request = new UpdateClassSubjectRequest
            {
                FinishedAt = DateTime.Now,
                SubjectId = "invalid-guid",
                Message = "Another Message"
            };

            //Act
            var result = await _action.Execute(classId, startedDate, request);

            //Assert
            Assert.True(result.IsFailure);
        }

        [Fact]
        public async Task Execute_ShouldReturnFailure_WhenFinishedAtIsLessThanStartedAt()
        {
            //Arrange
            string classId = Guid.NewGuid().ToString();
            string startedDate = DateTime.Now.ToString();

            var request = new UpdateClassSubjectRequest
            {
                FinishedAt = DateTime.Now.AddHours(-1),
                SubjectId = Guid.NewGuid().ToString(),
                Message = "Another Message"
            };

            //Act
            var result = await _action.Execute(classId, startedDate, request);

            //Assert
            Assert.True(result.IsFailure);
        }

        [Fact]
        public async Task Execute_ShouldReturnSuccess()
        {
            //Arrange
            string classId = Guid.NewGuid().ToString();
            string startedDate = DateTime.Now.AddHours(-1).ToString();

            var request = new UpdateClassSubjectRequest
            {
                FinishedAt = DateTime.Now,
                SubjectId = Guid.NewGuid().ToString(),
                Message = "Another Message"
            };

            //Act
            var result = await _action.Execute(classId, startedDate, request);

            //Assert
            Assert.True(result.IsSucess);
        }
    }
}
