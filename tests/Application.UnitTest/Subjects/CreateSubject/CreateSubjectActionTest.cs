using Application.Subjects.CreateSubject;
using Application.UnitTest.Configurators;
using Domain.Abstract;
using Domain.Subjects;
using Domain.Subjects.DTOs;
using MediatR;
using Moq;

namespace Application.UnitTest.Subjects.CreateSubject
{
    public class CreateSubjectActionTest
    {
        private readonly CreateSubjectAction _action;

        public CreateSubjectActionTest()
        {
            var mapper = MapperConfigurator.ConfigureSubjectProfile();

            var validator = new CreateSubjectValidator();

            var sender = new Mock<ISender>();
            sender.Setup(x => x.Send(It.IsAny<CreateSubjectCommand>(), default))
                .ReturnsAsync(Result<Subject>.Success(new Subject()));

            _action = new(sender.Object, validator, mapper);
        }

        [Fact]
        public async Task Execute_ShouldReturnFailure_WhenRequestIsNotValid()
        {
            //Arrange
            var request = new CreateSubjectRequest
            {
                Name = "",
                Description = "Lorem ipsum dolor sit amet"
            };

            //Act
            var result = await _action.Execute(request);

            //Assert
            Assert.True(result.IsFailure);
        }

        [Fact]
        public async Task Execute_ShouldReturnSuccess()
        {
            //Arrange
            var request = new CreateSubjectRequest
            {
                Name = "Subject Test",
                Description = "Lorem ipsum dolor sit amet"
            };

            //Act
            var result = await _action.Execute(request);

            //Assert
            Assert.True(result.IsSucess);
        }
    }
}
