using AutoMapper;
using Domain.Abstract;
using Domain.ClassesSubjects.DTOs;
using Domain.ClassesSubjects.Errors;
using Domain.Subjects;
using MediatR;

namespace Application.ClassesSubjects.UpdateClassSubject
{
    public class UpdateClassSubjectAction(
        ISender sender,
        IMapper mapper)
    {
        private readonly ISender _sender = sender;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<ClassSubjectResponse>> Execute(
            string classId, 
            string startedAt, 
            UpdateClassSubjectRequest request)
        {
            if (!Guid.TryParse(classId, out var parsedClassId))
                return Result<ClassSubjectResponse>.Failure(
                    ClassSubjectErrors.InvalidClassId);

            if (!DateTime.TryParse(startedAt, out DateTime parsedStartedAt))
                return Result<ClassSubjectResponse>.Failure(
                    ClassSubjectErrors.InvalidStartedDateFormat);

            var subjectIdIsGuid = !Guid.TryParse(
                request.SubjectId, 
                out Guid parsedSubjectId);

            if (!string.IsNullOrEmpty(request.SubjectId) && subjectIdIsGuid)
                    return Result<ClassSubjectResponse>.Failure(
                    SubjectErrors.NotFound(request.SubjectId),
                    System.Net.HttpStatusCode.NotFound);                          

            if (request.FinishedAt.HasValue && request.FinishedAt <= parsedStartedAt)
                return Result<ClassSubjectResponse>.Failure(
                    ClassSubjectErrors.FinishDateInvalid);

            var command = new UpdateClassSubjectCommand(
                parsedClassId,
                parsedStartedAt,
                !string.IsNullOrEmpty(request.SubjectId) ? 
                    parsedSubjectId : 
                    null,
                request.FinishedAt,
                request.Message);

            var result = await _sender.Send(command);

            if (result.IsFailure)
                return Result<ClassSubjectResponse>.Failure(
                    result.Errors,
                    result.StatusCode);

            return Result<ClassSubjectResponse>.Success(
                _mapper.Map<ClassSubjectResponse>(result.Data));
        }
    }
}
