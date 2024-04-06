using Application.ClassesSubjects.GetClassSubjectsByClass;
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
using Xunit.Abstractions;

namespace Application.UnitTest.ClassesSubjects.GetClassSubjectsByClass
{
    public class GetClassSubjectsByClassActionTest
    {
        private readonly GetClassSubjectsByClassAction _action;

        public GetClassSubjectsByClassActionTest()
        {
            var mapper = MapperConfigurator.ConfigureClassSubjectProfile();

            var sender = new Mock<ISender>();
            sender.Setup(x => x.Send(It.IsAny<GetClassSubjectsByClassQuery>(), default))
                .ReturnsAsync(Result<IEnumerable<ClassSubject>>.Success([]));

            _action = new(sender.Object, mapper);
        }

        [Fact]
        public async Task Execute_ShouldReturnFailure_WhenClassIdIsNotValid()
        {
            //Arrange
            const string classId = "invalid-guid";

            //Act
            var result = await _action.Execute(classId);

            //Assert
            Assert.True(result.IsFailure);            
        }

        [Fact]
        public async Task Execute_ShouldReturnFailure_WhenDatesAreInvalid()
        {
            //Arrange
            string classId = Guid.NewGuid().ToString();
            DateTime from = DateTime.Now.AddHours(1);
            DateTime to = DateTime.Now;

            //Act
            var result = await _action.Execute(classId, from, to);

            //Assert
            Assert.True(result.IsFailure);
        }

        [Fact]
        public async Task Execute_ShouldReturnSuccess()
        {
            //Arrange
            string classId = Guid.NewGuid().ToString();
            DateTime from = DateTime.Now;
            DateTime to = DateTime.Now.AddHours(1);

            //Act
            var result = await _action.Execute(classId, from, to);

            //Assert
            Assert.True(result.IsSucess);
        }
    }
}
