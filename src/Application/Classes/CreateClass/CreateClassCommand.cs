using Domain.Abstract;
using Domain.Classes;
using MediatR;

namespace Application.Classes.CreateClass
{
    public class CreateClassCommand: IRequest<Result<Class>>
    {
        public required string Name { get; set; }
        public required string Promotion {  get; set; }
        public required ClassType Type { get; set; }
        public required Guid ProfessorId {  get; set; }
    }
}
