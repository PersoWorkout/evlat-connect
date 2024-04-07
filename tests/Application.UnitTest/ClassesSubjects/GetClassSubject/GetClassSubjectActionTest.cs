using Application.ClassesSubjects.GetClassSubject;
using Application.UnitTest.Configurators;
using Domain.Abstract;
using Domain.ClassesSubjects;
using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UnitTest.ClassesSubjects.GetClassSubject
{
    public class GetClassSubjectActionTest
    {
        private readonly GetClassSubjectAction _action;

        public GetClassSubjectActionTest()
        {
            var mapper = MapperConfigurator.ConfigureClassSubjectProfile();

            var sender = new Mock<ISender>();
            sender.Setup(x => x.Send(It.IsAny<GetClassSubjectQuery>(), default))
                .ReturnsAsync(Result<ClassSubject>.Success(new ClassSubject()));

            _action = new(sender.Object, mapper);
        }

        [Fact]
        public async Task Execute_ShouldReturnFailure_WhenClassIdIsNotValid()
        {
            //Arrange
            const string classId = "Invalid-guid";
            string startedDate = DateTime.Now.ToString();

            //Act
            var result = await _action.Execute(classId, startedDate);

            //Assert
            Assert.True(result.IsFailure);
        }

        [Fact]
        public async Task Execute_ShouldReturnFailure_WhenStartedDateIsNotValid()
        {
            //Arrange
            string classId = Guid.NewGuid().ToString();
            string startedDate = "invalid-date";

            //Act
            var result = await _action.Execute(classId, startedDate);

            //Assert
            Assert.True(result.IsFailure);
        }

        [Fact]
        public async Task Execute_ShouldReturnSuccess()
        {
            //Arrange
            string classId = Guid.NewGuid().ToString();
            string startedDate = DateTime.Now.ToString();

            //Act
            var result = await _action.Execute(classId, startedDate);

            //Assert
            Assert.True(result.IsSucess);
        }
    }
}
