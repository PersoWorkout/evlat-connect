using Application.Competences.CreateCompetence;
using Application.UnitTest.Configurators;
using Application.UnitTest.Fakers.Repositories;
using Domain.Subjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UnitTest.Competences.CreateCompetence
{
    public class CreateCompetenceHandlerTest
    {
        private readonly FakeCompetenceRepository _competenceRepository;
        private readonly FakeSubjectRepository _subjectRepository;

        private readonly CreateCompetenceHandler _handler;

        public CreateCompetenceHandlerTest()
        {
            var mapper = MapperConfigurator.ConfigureCompetenceProfile();

            _competenceRepository = new();
            _subjectRepository = new();

            _handler = new(
                _competenceRepository, 
                _subjectRepository, 
                mapper);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenSubjectNotExist()
        {
            //Arrange
            _competenceRepository.ClearCompetences();
            _subjectRepository.ClearSubjects();

            var command = new CreateCompetenceCommand
            {
                Name = "Competence Name",
                Description = "Lorem ipsum dolor sit amet",
                SubjectId = Guid.NewGuid()
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
            _subjectRepository.ClearSubjects();

            var subject = new Subject
            {
                Name = "Subject Name",
                Description = "Lorem ipsum dolor sit amet"
            };
            _subjectRepository.Subjects.Add(subject);

            var command = new CreateCompetenceCommand
            {
                Name = "Competence Name",
                Description = "Lorem ipsum dolor sit amet",
                SubjectId = subject.Id
            };

            //Act
            var result = await _handler.Handle(command, default);

            //Assert
            Assert.True(result.IsSucess);
        }
    }
}
