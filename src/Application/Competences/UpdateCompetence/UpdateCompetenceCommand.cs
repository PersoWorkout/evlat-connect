using Domain.Abstract;
using Domain.Competences;
using MediatR;

namespace Application.Competences.UpdateCompetence
{
    public class UpdateCompetenceCommand: IRequest<Result<Competence>>
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public Guid? SubjectId { get; set; }
    }
}
