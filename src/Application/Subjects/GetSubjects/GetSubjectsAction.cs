using AutoMapper;
using Domain.Abstract;
using Domain.Subjects.DTOs;
using MediatR;

namespace Application.Subjects.GetSubjects
{
    public class GetSubjectsAction(
        ISender sender,
        IMapper mapper)
    {
        private readonly ISender _sender = sender;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<IEnumerable<SubjectResponse>>> Execute()
        {
            var result = await _sender.Send(new GetSubjectsQuery());

            if (result.IsFailure)
                return Result<IEnumerable<SubjectResponse>>.Failure(
                    result.Errors,
                    result.StatusCode);

            return Result<IEnumerable<SubjectResponse>>.Success(
                result.Data!
                    .Select(_mapper.Map<SubjectResponse>)
                    .ToList());
        }
    }
}
