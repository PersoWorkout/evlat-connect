using Domain.Abstract;
using Domain.Classes;
using MediatR;

namespace Application.Classes.UpdateClasse
{
    public class UpdateClassCommand: IRequest<Result<Class>>
    {
        public required Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Promotion { get; set; }
        public bool? IsActive { get; set; }
        public ClassType? Type { get; set; }
        public Guid? ProfessorId { get; set; }
    }
}
