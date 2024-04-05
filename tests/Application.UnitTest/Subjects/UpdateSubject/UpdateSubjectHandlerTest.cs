using Application.Subjects.UpdateSubject;
using Application.UnitTest.Fakers.Repositories;
using Domain.Subjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.UnitTest.Subjects.UpdateSubject
{
    public class UpdateSubjectHandlerTest
    {
        private readonly FakeSubjectRepository _repository;

        private UpdateSubjectHandler _handler;

        public UpdateSubjectHandlerTest()
        {
            _repository = new();

            _handler = new(_repository);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenSubjectNotExist()
        {
            //Arrange
            _repository.ClearSubjects();

            var command = new UpdateSubjectCommand
            {
                Id = Guid.NewGuid(),
                Name = "Test"
            };

            //Act
            var result = await _handler.Handle(command, default);

            //Assert
            Assert.True(result.IsFailure);
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        }
        [Fact]
        public async Task Handle_ShouldReturnSuccess()
        {
            //Arrange
            _repository.ClearSubjects();

            var subject = new Subject
            {
                Name = "First"
            };
            _repository.Subjects.Add(subject);

            var command = new UpdateSubjectCommand
            {
                Id = subject.Id,
                Name = "Test"
            };

            //Act
            var result = await _handler.Handle(command, default);

            //Assert
            Assert.True(result.IsSucess);
            Assert.Equal(command.Name, result.Data.Name);
        }

    }
}
