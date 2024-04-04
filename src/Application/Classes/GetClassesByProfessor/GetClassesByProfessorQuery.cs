using Domain.Abstract;
using Domain.Classes;
using MediatR;

namespace Application.Classes.GetClassesByProfessor
{
    public class GetClassesByProfessorQuery(Guid professorId) : IRequest<Result<IEnumerable<Class>>>
    {
        public Guid ProfessorId { get; set; } = professorId;
    }
}
