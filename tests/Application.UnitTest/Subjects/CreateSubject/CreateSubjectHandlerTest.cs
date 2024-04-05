using Application.Subjects.CreateSubject;
using Application.UnitTest.Configurators;
using Application.UnitTest.Fakers.Repositories;

namespace Application.UnitTest.Subjects.CreateSubject
{
    public class CreateSubjectHandlerTest
    {
        private readonly FakeSubjectRepository _repository;

        private readonly CreateSubjectHandler _handler;

        public CreateSubjectHandlerTest()
        {
            var mapper = MapperConfigurator.ConfigureSubjectProfile();

            _repository = new();

            _handler = new(_repository, mapper);
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccess()
        {
            //Arrange
            var command = new CreateSubjectCommand
            {
                Name = "Subject Test",
                Description = "Lorem ipsum dolor sit amet"
            };

            //Act
            var result = await _handler.Handle(command, default);

            //Assert
            Assert.True(result.IsSucess);
        }
    }
}
