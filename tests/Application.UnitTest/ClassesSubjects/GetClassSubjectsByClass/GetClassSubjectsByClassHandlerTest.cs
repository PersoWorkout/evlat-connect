using Application.ClassesSubjects.GetClassSubjectsByClass;
using Application.UnitTest.Fakers.Repositories;
using Domain.Classes;

namespace Application.UnitTest.ClassesSubjects.GetClassSubjectsByClass
{
    public class GetClassSubjectsByClassHandlerTest
    {
        private readonly FakeClassRepository _classRepository;
        private readonly FakeClassSubjectRepository _classSubjectRepository;

        private readonly GetClassSubjectsByClassHandler _handler;

        public GetClassSubjectsByClassHandlerTest()
        {
            _classRepository = new();
            _classSubjectRepository = new();

            _handler = new(_classSubjectRepository, _classRepository);
        }

        [Fact]
        public async Task Handle_ShoudlReturnFailure_WhenClassNotExist()
        {
            //Arrange
            _classRepository.ResetClasses();

            var query = new GetClassSubjectsByClassQuery(Guid.NewGuid());

            //Act
            var result = await _handler.Handle(query, default);

            //Assert
            Assert.True(result.IsFailure);
        }

        [Fact]
        public async Task Handle_ShoudlReturnSuccess()
        {
            //Arrange
            _classRepository.ResetClasses();

            var classEntity = new Class
            {
                Name = "Class Test",
                Promotion = "2024",
                Type = ClassType.Mixte
            };
            _classRepository.Classes.Add(classEntity);

            var query = new GetClassSubjectsByClassQuery(classEntity.Id);

            //Act
            var result = await _handler.Handle(query, default);

            //Assert
            Assert.True(result.IsSucess);
        }
    }
}
