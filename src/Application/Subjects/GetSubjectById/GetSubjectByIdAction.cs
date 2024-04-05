using AutoMapper;
using Domain.Abstract;
using Domain.Subjects;
using Domain.Subjects.DTOs;
using MediatR;
using System.Net;

namespace Application.Subjects.GetSubjectById
{
    public class GetSubjectByIdAction(
        ISender sender,
        IMapper mapper)
    {
        private readonly ISender _sender = sender;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<SubjectResponse>> Execute(string id)
        {
            if (!Guid.TryParse(id, out var subjectId))
                return Result<SubjectResponse>.Failure(
                    SubjectErrors.NotFound(id),
                    HttpStatusCode.NotFound);

            var result = await _sender.Send(
                new GetSubjectByIdQuery(subjectId));

            if (result.IsFailure)
                return Result<SubjectResponse>.Failure(
                    result.Errors,
                    result.StatusCode);

            return Result<SubjectResponse>.Success(
                _mapper.Map<SubjectResponse>(result.Data));
        }
    }
}
