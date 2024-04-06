using AutoMapper;
using Domain.Abstract;
using Domain.Classes.Errors;
using Domain.ClassesSubjects.DTOs;
using Domain.ClassesSubjects.Errors;
using MediatR;

namespace Application.ClassesSubjects.GetClassSubjectsByClass
{
    public class GetClassSubjectsByClassAction(
        ISender sender, 
        IMapper mapper)
    {
        private readonly ISender _sender = sender;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<IEnumerable<ClassSubjectResponse>>> Execute(
            string classId, 
            DateTime? from = null, 
            DateTime? to = null)
        {
            if (!Guid.TryParse(classId, out var parsedClassId))
                return Result<IEnumerable<ClassSubjectResponse>>.Failure(
                    ClassErrors.ClassNotFound(classId),
                    System.Net.HttpStatusCode.NotFound);

            if (from.HasValue && to.HasValue && from > to)
                return Result<IEnumerable<ClassSubjectResponse>>.Failure(
                    ClassSubjectErrors.InvalidDates);

            var result = await _sender.Send(
                new GetClassSubjectsByClassQuery(parsedClassId, from, to));

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
