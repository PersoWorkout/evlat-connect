using Application.ClassesSubjects.CreateClassSubject;
using Domain.ClassesSubjects.DTOs;

namespace Application.UnitTest.ClassesSubjects.CreateClassSubject
{
    public class CreateClassSubjectValidatorTest
    {
        private readonly CreateClassSubjectValidator _validator = new();

        [Fact]
        public void Validate_ShouldReturnNotValid_WhenFinishedDateIsLessThanStartingDate()
        {
            //Arrange
            var request = new CreateClassSubjectRequest
            {
                StartedAt = DateTime.Now.AddHours(2),
                FinishedAt = DateTime.Now,
            };

            //Act
            var validation = _validator.Validate(request);

            //Assert
            Assert.False(validation.IsValid);
        }

        [Fact]
        public void Validate_ShouldReturnValid()
        {
            //Arrange
            var request = new CreateClassSubjectRequest
            {
                StartedAt = DateTime.Now,
                FinishedAt = DateTime.Now.AddHours(2),
            };

            //Act
            var validation = _validator.Validate(request);

            //Assert
            Assert.True(validation.IsValid);
        }
    }
}
