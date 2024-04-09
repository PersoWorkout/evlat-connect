using Domain.Abstract;
using Domain.Competences;
using MediatR;

namespace Application.Competences.CreateCompetence
{
    public class CreateCompetenceCommand : IRequest<Result<Competence>>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid SubjectId { get; set; }
    }
}
