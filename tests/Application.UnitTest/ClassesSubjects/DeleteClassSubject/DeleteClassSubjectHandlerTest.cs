using Application.Classes.DeleteClass;
using Application.ClassesSubjects.DeleteClassSubject;
using Application.UnitTest.Fakers.Repositories;
using Domain.ClassesSubjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UnitTest.ClassesSubjects.DeleteClassSubject
{
    public class DeleteClassSubjectHandlerTest
    {
        private readonly FakeClassSubjectRepository _repository;

        private readonly DeleteClassSubjectHandler _handler;

        public DeleteClassSubjectHandlerTest()
        {
            _repository = new();

            _handler = new(_repository);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenClassSubjectNotExist()
        {
            //Arrange
            _repository.ClearClassesSubjects();

            var command = new DeleteClassSubjectCommand(Guid.NewGuid(), DateTime.Now);

            //Act
            var result = await _handler.Handle(command, default);

            //Assert
            Assert.True(result.IsFailure);
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccess()
        {
            //Arrange
            _repository.ClearClassesSubjects();

            var classSubject = new ClassSubject
            {
                ClassId = Guid.NewGuid(),
                SubjectId = Guid.NewGuid(),
                StartedAt = DateTime.Now,
                Message = "Test"
            };
            _repository.ClassesSubjects.Add(classSubject);

            var command = new DeleteClassSubjectCommand(classSubject.ClassId, classSubject.StartedAt);

            //Act
            var result = await _handler.Handle(command, default);

            //Assert
            Assert.True(result.IsSucess);
        }
    }
}
