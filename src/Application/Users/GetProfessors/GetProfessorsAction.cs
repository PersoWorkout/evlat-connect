using AutoMapper;
using Domain.Abstract;
using Domain.Users.DTOs;
using MediatR;

namespace Application.Users.GetProfessors
{
    public class GetProfessorsAction(ISender sender, IMapper mapper)
    {
        private readonly ISender _sender = sender;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<IEnumerable<UserResponse>>> Execute()
        {
            var result = await _sender.Send(new GetProfessorsQuery());

            if (result.IsFailure)
                return Result<IEnumerable<UserResponse>>.Failure(
                    result.Errors,
                    result.StatusCode);

            return Result<IEnumerable<UserResponse>>.Success(
                result.Data!
                .Select(_mapper.Map<UserResponse>)
                .ToList());
        }
    }
}
