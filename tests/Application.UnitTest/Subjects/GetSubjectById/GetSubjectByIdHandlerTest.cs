using Application.Subjects.GetSubjectById;
using Application.UnitTest.Fakers.Repositories;
using Domain.Subjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UnitTest.Subjects.GetSubjectById
{
    public class GetSubjectByIdHandlerTest
    {
        private readonly FakeSubjectRepository _repository;

        public GetSubjectByIdHandler _handler;

        public GetSubjectByIdHandlerTest()
        {
            _repository = new();

            _handler = new(_repository);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenSubjectNotExist()
        {
            //Arrange
            _repository.ClearSubjects();

            var subjectId = Guid.NewGuid();

            var command = new GetSubjectByIdQuery(subjectId);

            //Act
            var result = await _handler.Handle(command, default);

            //Assert
            var expectedError = SubjectErrors.NotFound(
                subjectId.ToString());

            Assert.True(result.IsFailure);
            Assert.Contains(expectedError, result.Errors);
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccess_WhenSubjectExist()
        {
            //Arrange
            _repository.ClearSubjects();

            var subject = new Subject
            {
                Name = "Test"
            };
            _repository.Subjects.Add(subject);

            var command = new GetSubjectByIdQuery(subject.Id);

            //Act
            var result = await _handler.Handle(command, default);

            //Assert
            Assert.True(result.IsSucess);
        }
    }
}
