using AutoMapper;
using Domain.Abstract;
using Domain.Competences.DTOs;
using Domain.Competences.Errors;
using MediatR;
using System.Net;

namespace Application.Competences.GetCompetenceById
{
    public class GetCompetenceByIdAction(ISender sender, IMapper mapper)
    {
        private readonly ISender _sender = sender;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<CompetenceResponse>> Execute(string id)
        {
            if (!Guid.TryParse(id, out var competenceId))
                return Result<CompetenceResponse>.Failure(
                    CompetenceErrors.CompetenceNotFound(id),
                    HttpStatusCode.NotFound);

            var result = await _sender.Send(
                new GetCompetenceByIdQuery(competenceId));

            if (result.IsFailure)
                return Result<CompetenceResponse>.Failure(
                    result.Errors,
                    result.StatusCode);

            return Result<CompetenceResponse>.Success(
                _mapper.Map<CompetenceResponse>(result.Data));
        }
    }
}
