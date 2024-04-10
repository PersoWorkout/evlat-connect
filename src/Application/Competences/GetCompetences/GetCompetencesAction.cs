using AutoMapper;
using Domain.Abstract;
using Domain.Competences.DTOs;
using MediatR;

namespace Application.Competences.GetCompetences
{
    public class GetCompetencesAction(ISender sender, IMapper mapper)
    {
        private readonly ISender _sender = sender;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<IEnumerable<CompetenceResponse>>> Execute()
        {
            var result = await _sender.Send(new GetCompetencesQuery());

            if (result.IsFailure)
                return Result<IEnumerable<CompetenceResponse>>.Failure(
                    result.Errors,
                    result.StatusCode);

            return Result<IEnumerable<CompetenceResponse>>.Success(
                result.Data
                    .Select(_mapper.Map<CompetenceResponse>)
                    .ToList());
        }
    }
}
