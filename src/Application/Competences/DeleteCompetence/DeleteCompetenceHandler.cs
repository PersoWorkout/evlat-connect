using Domain.Abstract;
using Domain.Competences.Errors;
using MediatR;

namespace Application.Competences.DeleteCompetence
{
    public class DeleteCompetenceHandler(
        ICompetenceRepository repository) : IRequestHandler<DeleteCompetenceCommand, Result<object>>
    {
        private readonly ICompetenceRepository _repository = repository;

        public async Task<Result<object>> Handle(DeleteCompetenceCommand request, CancellationToken cancellationToken)
        {
            var competence = await _repository.GetById(request.Id);
            if (competence is null)
                return Result<object>.Failure(
                    CompetenceErrors.CompetenceNotFound(request.Id.ToString()),
                    System.Net.HttpStatusCode.NotFound);

            await _repository.Delete(competence);

            return Result<object>.Success();
        }
    }
}
