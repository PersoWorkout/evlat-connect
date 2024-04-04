using AutoMapper;
using Domain.Abstract;
using Domain.Classes.DTOs;
using Domain.Users.Errors;
using MediatR;
using System.Net;

namespace Application.Classes.GetClassesByProfessor
{
    public class GetClassesByProfessorAction(ISender sender, IMapper mapper)
    {
        private readonly ISender _sender = sender;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<IEnumerable<ClassResponse>>> Execute(string professorId)
        {
            if (!Guid.TryParse(professorId, out var parsedId))
                return Result<IEnumerable<ClassResponse>>.Failure(
                        UserErrors.ProfessorNotFound(professorId),
                        HttpStatusCode.NotFound);

            var result = await _sender.Send(
                new GetClassesByProfessorQuery(parsedId));

            if(result.IsFailure)
                return Result<IEnumerable<ClassResponse>>.Failure(
                        result.Errors,
                        result.StatusCode);

            return Result<IEnumerable<ClassResponse>>.Success(
                result.Data
                .Select(_mapper.Map<ClassResponse>)
                .ToList());
        }
    }
}
