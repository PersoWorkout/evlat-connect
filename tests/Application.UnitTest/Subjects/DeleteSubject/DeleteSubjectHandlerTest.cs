using Application.Subjects.DeleteSubject;
using Application.UnitTest.Fakers.Repositories;
using Domain.Subjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.UnitTest.Subjects.DeleteSubject
{
    public class DeleteSubjectHandlerTest
    {
        private readonly FakeSubjectRepository _repository;

        private readonly DeleteSubjectHandler _handler;

        public DeleteSubjectHandlerTest()
        {
            _repository = new();

            _handler = new(_repository);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenSubjectNotExist()
        {
            //Arrange
            _repository.ClearSubjects();

            var command = new DeleteSubjectCommand(Guid.NewGuid());

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

            var command = new DeleteSubjectCommand(subject.Id);

            //Act
            var result = await _handler.Handle(command, default);

            //Assert
            Assert.True(result.IsSucess);
        }
    }
}
