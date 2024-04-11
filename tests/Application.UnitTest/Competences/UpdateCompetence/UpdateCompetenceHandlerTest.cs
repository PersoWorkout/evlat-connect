using Application.Competences.UpdateCompetence;
using Application.UnitTest.Fakers.Repositories;
using Domain.Competences;
using Domain.Subjects;

namespace Application.UnitTest.Competences.UpdateCompetence
{
    public class UpdateCompetenceHandlerTest
    {
        private readonly FakeCompetenceRepository _competenceRepository;
        private readonly FakeSubjectRepository _subjectRepository;

        private readonly UpdateCompetenceHandler _handler;

        public UpdateCompetenceHandlerTest()
        {
            _competenceRepository = new();
            _subjectRepository = new();

            _handler = new(_competenceRepository, _subjectRepository);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenCompetenceNotExist()
        {
            //Arrange
            _subjectRepository.ClearSubjects();
            _competenceRepository.ClearCompetences();

            var command = new UpdateCompetenceCommand
            {
                Id = Guid.NewGuid(),
                Name = "Test"
            };

            //Act
            var result = await _handler.Handle(command, default);

            //Assert
            Assert.True(result.IsFailure);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenSubjectIdNotExist()
        {
            //Arrange
            _competenceRepository.ClearCompetences();
            var competence = new Competence
            {
                Name = "Test",
                Description = "Test",
                SubjectId = Guid.NewGuid(),
            };
            _competenceRepository.Competences.Add(competence);

            _subjectRepository.ClearSubjects();

            var command = new UpdateCompetenceCommand
            {
                Id = competence.Id,
                Name = "Test",
                SubjectId= Guid.NewGuid(),
            };

            //Act
            var result = await _handler.Handle(command, default);

            //Assert
            Assert.True(result.IsFailure);
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccess()
        {
            //Arrange
            _competenceRepository.ClearCompetences();
            var competence = new Competence
            {
                Name = "Test",
                Description = "Test",
                SubjectId = Guid.NewGuid(),
            };
            _competenceRepository.Competences.Add(competence);

            _subjectRepository.ClearSubjects();
            var subject = new Subject
            {
                Name = "Test",
                Description = "Test"
            };
            _subjectRepository.Subjects.Add(subject);

            var command = new UpdateCompetenceCommand
            {
                Id = competence.Id,
                Name = "Test",
                SubjectId = subject.Id,
            };

            //Act
            var result = await _handler.Handle(command, default);

            //Assert
            Assert.True(result.IsSucess);
        }
    }
}
