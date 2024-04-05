using Domain.Subjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UnitTests.Subjects
{
    public class SubjectTest
    {
        [Fact]
        public void Update_ShouldUpdateField_WhenAreNotNull()
        {
            //Arrange
            const string NewName = "New Subject Test";
            const string NewDescription = "AnotherDescription";

            var subject = new Subject
            {
                Name = "Subject Test",
                Description = "Lorem ipsum dolor si amet"
            };

            //Act
            subject.Update(NewName, NewDescription);

            //Assert
            Assert.Equal(NewName, subject.Name);
            Assert.Equal(NewDescription, subject.Description);
        }

        [Fact]
        public void Update_ShouldNotUpdateName_WhenNameNull()
        {
            //Arrange
            const string subjectName = "Subject Test";
            const string subjectDescription = "Lorem ipsum dolor si amet";

            const string NewDescription = "AnotherDescription";

            var subject = new Subject
            {
                Name = subjectName,
                Description = subjectDescription
            };

            //Act
            subject.Update(description: NewDescription);

            //Assert
            Assert.Equal(subjectName, subject.Name);
            Assert.Equal(NewDescription, subject.Description);
        }
    }
}
