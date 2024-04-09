using Application.ClassesSubjects.UpdateClassSubject;
using Application.UnitTest.Fakers.Repositories;
using Domain.ClassesSubjects;
using Domain.Subjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UnitTest.ClassesSubjects.UpdateClassSubject
{
    public class UpdateClassSubjectHandlerTest
    {
        private readonly FakeSubjectRepository _subjectRepository;
        private readonly FakeClassSubjectRepository _classSubjectRepository;

        private readonly UpdateClassSubjectHandler _handler;

        public UpdateClassSubjectHandlerTest()
        {
            _subjectRepository = new();
            _classSubjectRepository = new();

            _handler = new(_subjectRepository, _classSubjectRepository);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenClassSubjectNotExist()
        {
            //Arrange
            _classSubjectRepository.ClearClassesSubjects();
            _subjectRepository.ClearSubjects();

            var command = new UpdateClassSubjectCommand(
                Guid.NewGuid(), 
                DateTime.Now, 
                message: "Another Message");

            //Act
            var result = await _handler.Handle(command, default);

            //Assert
            Assert.True(result.IsFailure);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenSubjectNotExist()
        {
            //Arrange
            _classSubjectRepository.ClearClassesSubjects();

            var classSubject = new ClassSubject
            {
                ClassId = Guid.NewGuid(),
                StartedAt = DateTime.Now,
                SubjectId = Guid.NewGuid(),
                Message = "Initial Message"
            };
            _classSubjectRepository.ClassesSubjects.Add(classSubject);

            _subjectRepository.ClearSubjects();

            var command = new UpdateClassSubjectCommand(
                classSubject.ClassId,
                classSubject.StartedAt,
                subjectId: Guid.NewGuid(),
                message: "Another Message");

            //Act
            var result = await _handler.Handle(command, default);

            //Assert
            Assert.True(result.IsFailure);
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccess()
        {
            //Arrange
            _classSubjectRepository.ClearClassesSubjects();

            var classSubject = new ClassSubject
            {
                ClassId = Guid.NewGuid(),
                StartedAt = DateTime.Now,
                SubjectId = Guid.NewGuid(),
                Message = "Initial Message"
            };
            _classSubjectRepository.ClassesSubjects.Add(classSubject);

            _subjectRepository.ClearSubjects();

            var subject = new Subject
            {
                Name = "Test",
                Description = "Test",
            };
            _subjectRepository.Subjects.Add(subject);

            var command = new UpdateClassSubjectCommand(
                classSubject.ClassId,
                classSubject.StartedAt,
                subjectId: subject.Id,
                message: "Another Message");

            //Act
            var result = await _handler.Handle(command, default);

            //Assert
            Assert.True(result.IsSucess);
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccess_WhenSubjectIsNull()
        {
            //Arrange
            _classSubjectRepository.ClearClassesSubjects();

            var classSubject = new ClassSubject
            {
                ClassId = Guid.NewGuid(),
                StartedAt = DateTime.Now,
                SubjectId = Guid.NewGuid(),
                Message = "Initial Message"
            };
            _classSubjectRepository.ClassesSubjects.Add(classSubject);

            _subjectRepository.ClearSubjects();

            var command = new UpdateClassSubjectCommand(
                classSubject.ClassId,
                classSubject.StartedAt,
                message: "Another Message");

            //Act
            var result = await _handler.Handle(command, default);

            //Assert
            Assert.True(result.IsSucess);
        }
    }
}
