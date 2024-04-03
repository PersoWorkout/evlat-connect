using Application.Classes.GetClassById;
using Application.UnitTest.Fakers.Repositories;
using Domain.Classes;
using Domain.Classes.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UnitTest.Classes.GetClassById
{
    public class GetClassByIdHandlerTest
    {
        private readonly FakeClassRepository _repository;

        private readonly GetClassByIdHandler _handler;

        public GetClassByIdHandlerTest()
        {
            _repository = new();

            _handler = new(_repository);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenClassNotExist()
        {
            //Arrange
            _repository.ResetClasses();

            var query = new GetClassByIdQuery(Guid.NewGuid());

            //Act
            var result = await _handler.Handle(query, default);

            //Assert
            Assert.True(result.IsFailure);
            Assert.Contains(
                ClassErrors.ClassNotFound(query.Id.ToString()), 
                result.Errors);
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccess()
        {
            //Arrange
            _repository.ResetClasses();

            var createdClass = new Class
            {
                Name = "Class Test",
                Promotion = "2023",
                Type = ClassType.Boy
            };
            _repository.Classes.Add(createdClass);

            var query = new GetClassByIdQuery(createdClass.Id);

            //Act
            var result = await _handler.Handle(query, default);

            //Assert
            Assert.True(result.IsSucess);
        }
    }
}
