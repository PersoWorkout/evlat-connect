using AutoMapper;
using Domain.Abstract;
using Domain.ClassesSubjects.DTOs;
using Domain.ClassesSubjects.Errors;
using MediatR;

namespace Application.ClassesSubjects.GetClassSubject
{
    public class GetClassSubjectAction(ISender sender, IMapper mapper)
    {
        private readonly ISender _sender = sender;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<ClassSubjectResponse>> Execute(string classId, string startedDateString)
        {
            if (!Guid.TryParse(classId, out var parsedClassId))
                return Result<ClassSubjectResponse>.Failure(
                    ClassSubjectErrors.InvalidClassId);

            if (!DateTime.TryParse(startedDateString, out var parsedDate))
                return Result<ClassSubjectResponse>.Failure(
                    ClassSubjectErrors.InvalidStartedDateFormat);

            var result = await _sender.Send(
                new GetClassSubjectQuery(parsedClassId, parsedDate));

            if (result.IsFailure)
                return Result<ClassSubjectResponse>.Failure(
                    result.Errors,
                    result.StatusCode);

            return Result<ClassSubjectResponse>.Success(
                _mapper.Map<ClassSubjectResponse>(result.Data));
        }
    }
}
