using Domain.Abstract;
using MediatR;

namespace Application.Competences.DeleteCompetence
{
    public class DeleteCompetenceCommand(Guid id): IRequest<Result<object>>
    {
        public Guid Id { get; set; } = id;
    }
}
