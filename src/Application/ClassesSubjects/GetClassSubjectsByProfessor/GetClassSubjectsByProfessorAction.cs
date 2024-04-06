using AutoMapper;
using Domain.Abstract;
using Domain.ClassesSubjects.DTOs;
using Domain.ClassesSubjects.Errors;
using Domain.Users.Errors;
using MediatR;

namespace Application.ClassesSubjects.GetClassSubjectsByProfessor
{
    public class GetClassSubjectsByProfessorAction(ISender sender, IMapper mapper)
    {
        private readonly ISender _sender = sender;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<IEnumerable<ClassSubjectResponse>>> Execute(
            string professorId,
            DateTime? from = null,
            DateTime? to = null)
        {
            if (!Guid.TryParse(professorId, out var parsedProfessorId))
                return Result<IEnumerable<ClassSubjectResponse>>.Failure(
                    UserErrors.ProfessorNotFound(professorId),
                    System.Net.HttpStatusCode.NotFound);

            if (from.HasValue && to.HasValue && from > to)
                return Result<IEnumerable<ClassSubjectResponse>>.Failure(
                    ClassSubjectErrors.InvalidDates);

            var result = await _sender.Send(
                new GetClassSubjectsByProfessorQuery(parsedProfessorId, from, to));

            if (result.IsFailure)
                return Result<IEnumerable<ClassSubjectResponse>>.Failure(
                    result.Errors,
                    result.StatusCode);

            return Result<IEnumerable<ClassSubjectResponse>>.Success(
                result.Data!.Select(
                    _mapper.Map<ClassSubjectResponse>));
        }
    }
}
