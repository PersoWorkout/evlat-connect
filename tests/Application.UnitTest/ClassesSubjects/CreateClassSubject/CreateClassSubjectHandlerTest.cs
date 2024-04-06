using Application.ClassesSubjects.CreateClassSubject;
using Application.UnitTest.Configurators;
using Application.UnitTest.Fakers.Repositories;
using Domain.Classes;
using Domain.ClassesSubjects;
using Domain.Subjects;

namespace Application.UnitTest.ClassesSubjects.CreateClassSubject
{
    public class CreateClassSubjectHandlerTest
    {
        private readonly FakeClassSubjectRepository _classSubjectRepository;
        private readonly FakeClassRepository _classRepository;
        private readonly FakeSubjectRepository _subjectrepository;

        private readonly CreateClassSubjectHandler _handler;

        public CreateClassSubjectHandlerTest()
        {
            var mapper = MapperConfigurator
                .ConfigureClassSubjectProfile();

            _subjectrepository = new();
            _classRepository = new();
            _classSubjectRepository = new();

            _handler = new(
                _classSubjectRepository, 
                _classRepository, 
                _subjectrepository,
                mapper);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenClassNotExist()
        {
            //Arrange
            _classRepository.ResetClasses();

            var command = new CreateClassSubjectCommand
            {
                ClassId = Guid.NewGuid(),
                SubjectId = Guid.NewGuid(),
                StartedAt = DateTime.Now,
                FinishedAt = DateTime.Now
            };

            //Act
            var result = await _handler.Handle(command, default);

            //Assert
            Assert.True(result.IsFailure);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenSubjectNotExist()
        {
            //Arrange
            _classRepository.ResetClasses();
            _subjectrepository.ClearSubjects();

            var classEntity = new Class
            {
                Name = "Class Test",
                Promotion = "2024",
                Type = ClassType.Mixte
            };
            _classRepository.Classes.Add(classEntity);


            var command = new CreateClassSubjectCommand
            {
                ClassId = classEntity.Id,
                SubjectId = Guid.NewGuid(),
                StartedAt = DateTime.Now,
                FinishedAt = DateTime.Now
            };

            //Act
            var result = await _handler.Handle(command, default);

            //Assert
            Assert.True(result.IsFailure);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenClassSubjectAlreadyExist()
        {
            //Arrange
            _classRepository.ResetClasses();
            _subjectrepository.ClearSubjects();
            _classSubjectRepository.ClearClassesSubjects();

            var classEntity = new Class
            {
                Name = "Class Test",
                Promotion = "2024",
                Type = ClassType.Mixte
            };
            _classRepository.Classes.Add(classEntity);

            var subject = new Subject
            {
                Name = "Subject test",
                Description = "Lorem ipsum dolor sit amet"
            };
            _subjectrepository.Subjects.Add(subject);

            var classSubject = new ClassSubject
            {
                ClassId = classEntity.Id,
                SubjectId = subject.Id,
                StartedAt = DateTime.Now,
                FinishedAt = DateTime.Now.AddHours(1)
            };
            _classSubjectRepository.ClassesSubjects.Add(classSubject);


            var command = new CreateClassSubjectCommand
            {
                ClassId = classSubject.ClassId,
                SubjectId = classSubject.SubjectId,
                StartedAt = classSubject.StartedAt,
                FinishedAt = classSubject.FinishedAt
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
            _classRepository.ResetClasses();
            _subjectrepository.ClearSubjects();
            _classSubjectRepository.ClearClassesSubjects();

            var classEntity = new Class
            {
                Name = "Class Test",
                Promotion = "2024",
                Type = ClassType.Mixte
            };
            _classRepository.Classes.Add(classEntity);

            var subject = new Subject
            {
                Name = "Subject test",
                Description = "Lorem ipsum dolor sit amet"
            };
            _subjectrepository.Subjects.Add(subject);

            var command = new CreateClassSubjectCommand
            {
                ClassId = classEntity.Id,
                SubjectId = subject.Id,
                StartedAt = DateTime.Now,
                FinishedAt = DateTime.Now.AddHours(1)
            };

            //Act
            var result = await _handler.Handle(command, default);

            //Assert
            Assert.True(result.IsSucess);
        }
    }
}
