using Application.Competences;
using AutoMapper;
using Domain.Abstract;
using Domain.CompetencesLinks;
using Domain.CompetencesLinks.Errors;
using MediatR;
using System.Net;

namespace Application.CompetencesLink.CreateCompetenceLink
{
    public class CreateCompetenceLinkHandler(
        ICompetenceLinkRepository competenceLinkRepository, 
        ICompetenceRepository competenceRepository,
        IMapper mapper) : IRequestHandler<CreateCompetenceLinkCommand, Result<CompetenceLink>>
    {
        private readonly ICompetenceLinkRepository _competenceLinkRepository = competenceLinkRepository;
        private readonly ICompetenceRepository _competenceRepository = competenceRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<CompetenceLink>> Handle(CreateCompetenceLinkCommand request, CancellationToken cancellationToken)
        {
            var competence = await _competenceRepository.GetById(request.CompetenceId);
            if (competence is null)
                return Result<CompetenceLink>.Failure(
                    CompetenceLinkErrors.CompetenceNotFound(request.CompetenceId.ToString()),
                    HttpStatusCode.NotFound);

            var link = _mapper.Map<CompetenceLink>(request);
            await _competenceLinkRepository.Create(link);

            return Result<CompetenceLink>.Success(link);
        }
    }
}
