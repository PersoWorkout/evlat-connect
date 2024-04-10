using Domain.Abstract;
using Domain.Competences;
using MediatR;

namespace Application.Competences.GetCompetenceById
{
    public class GetCompetenceByIdQuery(Guid id): 
        IRequest<Result<Competence>>
    {
        public Guid Id { get; set; } = id;
    }
}
