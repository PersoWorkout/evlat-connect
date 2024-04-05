using Application.Subjects.CreateSubject;
using Domain.Subjects.DTOs;

namespace Application.UnitTest.Subjects.CreateSubject
{
    public class CreateSubjectValidatorTest
    {
        private readonly CreateSubjectValidator _validator = new();

        [Fact]
        public void Validate_ShouldReturnFailure_WhenNameIsEmpty()
        {
            //Arrange
            var request = new CreateSubjectRequest
            {
                Name = ""
            };

            //Act
            var validation = _validator.Validate(request);

            //Assert
            Assert.False(validation.IsValid);
        }

        [Fact]
        public void Validate_ShouldReturnSuccess()
        {
            //Arrange
            var request = new CreateSubjectRequest
            {
                Name = "Test"
            };

            //Act
            var validation = _validator.Validate(request);

            //Assert
            Assert.True(validation.IsValid);
        }
    }
}
