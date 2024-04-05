using AutoMapper;
using Domain.Abstract;
using Domain.Subjects;
using Domain.Subjects.DTOs;
using MediatR;
using System.Net;

namespace Application.Subjects.UpdateSubject
{
    public class UpdateSubjectAction(
        ISender sender, 
        IMapper mapper)
    {
        private readonly ISender _sender = sender;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<SubjectResponse>> Execute(string id, UpdateSubjectRequest request)
        {
            if(!Guid.TryParse(id, out var subjectId))
                return Result<SubjectResponse>.Failure(
                    SubjectErrors.NotFound(id),
                    HttpStatusCode.NotFound);

            var command = _mapper.Map<UpdateSubjectCommand>(request);
            command.Id = subjectId;

            var result = await _sender.Send(command);

            if (result.IsFailure)
                return Result<SubjectResponse>.Failure(
                    result.Errors,
                    result.StatusCode);

            return Result<SubjectResponse>.Success(
                _mapper.Map<SubjectResponse>(result.Data));
        }
    }
}
