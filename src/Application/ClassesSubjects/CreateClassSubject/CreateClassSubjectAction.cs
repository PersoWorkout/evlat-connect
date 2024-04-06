using AutoMapper;
using Domain.Abstract;
using Domain.Classes.Errors;
using Domain.ClassesSubjects.DTOs;
using Domain.Subjects;
using FluentValidation;
using MediatR;
using System.Net;

namespace Application.ClassesSubjects.CreateClassSubject
{
    public class CreateClassSubjectAction(
        ISender sender,
        IValidator<CreateClassSubjectRequest> validator,
        IMapper mapper)
    {
        private readonly ISender _sender = sender;
        private readonly IValidator<CreateClassSubjectRequest> _validator = validator;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<ClassSubjectResponse>> Execute(
            string classId, 
            string subjectId, 
            CreateClassSubjectRequest request)
        {
            if (!Guid.TryParse(classId, out var parsedClassId))
                return Result<ClassSubjectResponse>.Failure(
                    ClassErrors.ClassNotFound(classId),
                    HttpStatusCode.NotFound);

            if (!Guid.TryParse(subjectId, out var parsedSubjectId))
                return Result<ClassSubjectResponse>.Failure(
                    SubjectErrors.NotFound(subjectId),
                    HttpStatusCode.NotFound);

            var validation = _validator.Validate(request);
            if (!validation.IsValid)
                return Result<ClassSubjectResponse>.Failure(
                    validation.Errors
                    .Select(x => new Error(
                        x.ErrorCode, 
                        x.ErrorMessage))
                    .ToList());

            var command = _mapper.Map<CreateClassSubjectCommand>(request);
            command.ClassId = parsedClassId;
            command.SubjectId = parsedSubjectId;

            var result = await _sender.Send(command);

            if(result.IsFailure)
                return Result<ClassSubjectResponse>.Failure(
                    result.Errors,
                    result.StatusCode);

            return Result<ClassSubjectResponse>.Success(
                _mapper.Map<ClassSubjectResponse>(result.Data));
        }
    }
}
