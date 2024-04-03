using AutoMapper;
using Domain.Abstract;
using Domain.Classes.DTOs;
using MediatR;

namespace Application.Classes.GetAll
{
    public class GetClassesAction(ISender sender, IMapper mapper)
    {
        private readonly ISender _sender = sender;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<List<ClassResponse>>> Execute()
        {
            var result = await _sender.Send(new GetClassesQuery());

            if (result.IsFailure)
                return Result<List<ClassResponse>>.Failure(
                    result.Errors,
                    result.StatusCode);

            return Result<List<ClassResponse>>.Success(
                result.Data!
                .Select(_mapper.Map<ClassResponse>)
                .ToList());
                
        }
    }
}
