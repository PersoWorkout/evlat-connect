using AutoMapper;
using Domain.Abstract;
using Domain.CompetencesLinks.DTOs;
using Domain.CompetencesLinks.Errors;
using MediatR;
using System.Net;

namespace Application.CompetencesLink.GetCompetenceLinkById
{
    public class GetCompetenceLinkByIdAction(ISender sender, IMapper mapper)
    {
        private readonly ISender _sender = sender;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<CompetenceLinkResponse>> Execute(string id)
        {
            if (!Guid.TryParse(id, out var competenceLinkId))
                return Result<CompetenceLinkResponse>.Failure(
                    CompetenceLinkErrors.CompetenceLinkNotFound(id),
                    HttpStatusCode.NotFound);

            var result = await _sender.Send(
                new GetCompetenceLinkByIdQuery(competenceLinkId));

            if (result.IsFailure)
                return Result<CompetenceLinkResponse>.Failure(
                    result.Errors,
                    result.StatusCode);

            return Result<CompetenceLinkResponse>.Success(
                _mapper.Map<CompetenceLinkResponse>(result.Data));
        }
    }
}
