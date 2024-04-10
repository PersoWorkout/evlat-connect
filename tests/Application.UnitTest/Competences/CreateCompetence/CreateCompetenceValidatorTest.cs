using Application.Competences.CreateCompetence;
using Domain.Competences.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UnitTest.Competences.CreateCompetence
{
    public class CreateCompetenceValidatorTest
    {
        private readonly CreateCompetenceValidator _validator = new();

        [Fact]
        public void Validate_ShoulReturnFailure_WhenNameIsEmpty()
        {
            //Arrange
            const string Name = "";
            const string Description = "Lorem ispum dolor sit amet";
            string SubjectId = Guid.NewGuid().ToString();

            var request = new CreateCompetenceRequest
            {
                Name = Name,
                Description = Description,
                SubjectId = SubjectId
            };

            //Aact
            var validation = _validator.Validate(request);

            //Assert
            Assert.False(validation.IsValid);
        }

        [Fact]
        public void Validate_ShoulReturnFailure_WhenSubjectidIsEmpty()
        {
            //Arrange
            const string Name = "Name Test";
            const string Description = "Lorem ispum dolor sit amet";
            string SubjectId = string.Empty;

            var request = new CreateCompetenceRequest
            {
                Name = Name,
                Description = Description,
                SubjectId = SubjectId
            };

            //Aact
            var validation = _validator.Validate(request);

            //Assert
            Assert.False(validation.IsValid);
        }

        [Fact]
        public void Validate_ShoulReturnFailure_WhenSubjectIdIsNotAVlidGuid()
        {
            //Arrange
            const string Name = "Name Test";
            const string Description = "Lorem ispum dolor sit amet";
            string SubjectId = "invalid-guid";

            var request = new CreateCompetenceRequest
            {
                Name = Name,
                Description = Description,
                SubjectId = SubjectId
            };

            //Aact
            var validation = _validator.Validate(request);

            //Assert
            Assert.False(validation.IsValid);
        }

        [Fact]
        public void Validate_ShoulReturnSuccess()
        {
            //Arrange
            const string Name = "Name Test";
            const string Description = "Lorem ispum dolor sit amet";
            string SubjectId = Guid.NewGuid().ToString();

            var request = new CreateCompetenceRequest
            {
                Name = Name,
                Description = Description,
                SubjectId = SubjectId
            };

            //Aact
            var validation = _validator.Validate(request);

            //Assert
            Assert.True(validation.IsValid);
        }
    }
}
