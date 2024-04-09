using Application.Subjects;
using AutoMapper;
using Domain.Abstract;
using Domain.Competences;
using Domain.Competences.Errors;
using MediatR;
using System.Net;

namespace Application.Competences.CreateCompetence
{
    public class CreateCompetenceHandler(
        ICompetenceRepository competenceRepository, 
        ISubjectRepository subjectrepository, 
        IMapper mapper) : IRequestHandler<CreateCompetenceCommand, Result<Competence>>
    {
        private readonly ICompetenceRepository _competenceRepository = competenceRepository;
        private readonly ISubjectRepository _subjectrepository = subjectrepository;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<Competence>> Handle(CreateCompetenceCommand request, CancellationToken cancellationToken)
        {
            var subject = await _subjectrepository.GetById(request.SubjectId);
            if(subject is null)
                return Result<Competence>.Failure(
                    CompetenceErrors.CompetenceNotFound(request.SubjectId.ToString()),
                    HttpStatusCode.NotFound);

            var competence = _mapper.Map<Competence>(request);
            competence = await _competenceRepository.Create(competence);

            return Result<Competence>.Success(competence);
        }
    }
}
