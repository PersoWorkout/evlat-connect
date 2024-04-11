using Application.Subjects;
using Domain.Abstract;
using Domain.Competences;
using Domain.Competences.Errors;
using Domain.Subjects;
using MediatR;
using System.Net;

namespace Application.Competences.UpdateCompetence
{
    public class UpdateCompetenceHandler : IRequestHandler<UpdateCompetenceCommand, Result<Competence>>
    {
        private readonly ICompetenceRepository _competenceRepository;
        private readonly ISubjectRepository _subjectRepository;

        public UpdateCompetenceHandler(ICompetenceRepository competenceRepository, ISubjectRepository subjectRepository)
        {
            _competenceRepository = competenceRepository;
            _subjectRepository = subjectRepository;
        }

        public async Task<Result<Competence>> Handle(UpdateCompetenceCommand request, CancellationToken cancellationToken)
        {
            var competence = await _competenceRepository.GetById(request.Id);
            if(competence is null)
                return Result<Competence>.Failure(
                    CompetenceErrors.CompetenceNotFound(request.Id.ToString()),
                    HttpStatusCode.NotFound);

            if (request.SubjectId.HasValue)
            {
                var subject = await _subjectRepository
                    .GetById(request.SubjectId.Value);

                if (subject is null)
                    return Result<Competence>.Failure(
                        SubjectErrors.NotFound(request.SubjectId.Value.ToString()),
                        HttpStatusCode.NotFound);
            }

            competence.Update(
                request.Name, 
                request.Description, 
                subjectId: request.SubjectId);

            await _competenceRepository.Update(competence);

            return Result<Competence>.Success(competence);
        }
    }
}
