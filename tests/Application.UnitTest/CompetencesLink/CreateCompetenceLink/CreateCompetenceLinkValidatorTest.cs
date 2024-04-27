using Application.CompetencesLink.CreateCompetenceLink;
using Domain.CompetencesLinks;
using Domain.CompetencesLinks.DTOs;

namespace Application.UnitTest.CompetencesLink.CreateCompetenceLink
{
    public class CreateCompetenceLinkValidatorTest
    {
        private readonly CreateCompetenceLinkValidator _validator = new();

        [Fact]
        public void Validate_ShouldReturnFalse_WhenNameIsEmpty()
        {
            //Arrange
            var request = new AddCompetenceLinkRequest
            {
                Name = "",
                Path = "random path",
                Type = (int)LinkType.Video,
                CompetenceId = Guid.NewGuid().ToString()
            };

            //Act
            var validation = _validator.Validate(request);

            //Assert
            Assert.False(validation.IsValid);
        }

        [Fact]
        public void Validate_ShouldReturnFalse_WhenPathIsEmpty()
        {
            //Arrange
            var request = new AddCompetenceLinkRequest
            {
                Name = "Name Test",
                Path = "",
                Type = (int)LinkType.Video,
                CompetenceId = Guid.NewGuid().ToString()
            };

            //Act
            var validation = _validator.Validate(request);

            //Assert
            Assert.False(validation.IsValid);
        }

        [Fact]
        public void Validate_ShouldReturnFalse_WhenTypeIsEmpty()
        {
            //Arrange
            var request = new AddCompetenceLinkRequest
            {
                Name = "Name Test",
                Path = "random path",
                Type = 0,
                CompetenceId = Guid.NewGuid().ToString()
            };

            //Act
            var validation = _validator.Validate(request);

            //Assert
            Assert.False(validation.IsValid);
        }

        [Fact]
        public void Validate_ShouldReturnFalse_WhenTypeIsInvalid()
        {
            //Arrange
            var request = new AddCompetenceLinkRequest
            {
                Name = "Name Test",
                Path = "random path",
                Type = 15,
                CompetenceId = Guid.NewGuid().ToString()
            };

            //Act
            var validation = _validator.Validate(request);

            //Assert
            Assert.False(validation.IsValid);
        }

        [Fact]
        public void Validate_ShouldReturnFalse_WhenCompetenceIdIsEmpty()
        {
            //Arrange
            var request = new AddCompetenceLinkRequest
            {
                Name = "Name Test",
                Path = "random path",
                Type = (int)LinkType.Video,
                CompetenceId = ""
            };

            //Act
            var validation = _validator.Validate(request);

            //Assert
            Assert.False(validation.IsValid);
        }

        [Fact]
        public void Validate_ShouldReturnFalse_WhenCompetenceIdIsInvalid()
        {
            //Arrange
            var request = new AddCompetenceLinkRequest
            {
                Name = "Name Test",
                Path = "random path",
                Type = (int)LinkType.Video,
                CompetenceId = "invalid-guid"
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
            var request = new AddCompetenceLinkRequest
            {
                Name = "Name Test",
                Path = "random path",
                Type = (int)LinkType.Video,
                CompetenceId = Guid.NewGuid().ToString()
            };

            //Act
            var validation = _validator.Validate(request);

            //Assert
            Assert.True(validation.IsValid);
        }
    }
}
