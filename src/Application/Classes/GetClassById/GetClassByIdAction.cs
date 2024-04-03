using AutoMapper;
using Domain.Abstract;
using Domain.Classes.DTOs;
using Domain.Classes.Errors;
using MediatR;

namespace Application.Classes.GetClassById
{
    public class GetClassByIdAction(ISender sender, IMapper mapper)
    {
        private readonly ISender _sender = sender;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<ClassResponse>> Execute(string id)
        {
            if (!Guid.TryParse(id, out var parsedId))
                return Result<ClassResponse>.Failure(
                    ClassErrors.ClassNotFound(id));

            var result = await _sender.Send(
                new GetClassByIdQuery(parsedId));

            if (result.IsFailure)
                return Result<ClassResponse>.Failure(
                    result.Errors,
                    result.StatusCode);

            return Result<ClassResponse>.Success(
                _mapper.Map<ClassResponse>(result.Data));
        }
    }
}
