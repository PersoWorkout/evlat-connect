using AutoMapper;
using Domain.Abstract;
using Domain.Users.DTOs;
using FluentValidation;
using MediatR;

namespace Application.Users.CreateUser
{
    public class CreateUserAction(
        ISender sender, 
        IValidator<CreateUserRequest> validator, 
        IMapper mapper)
    {
        private readonly ISender _sender = sender;
        private readonly IValidator<CreateUserRequest> _validator = validator;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<UserResponse>> Execute(CreateUserRequest request)
        {
            var validation = _validator.Validate(request);
            if (!validation.IsValid)
                return Result<UserResponse>.Failure(
                    validation.Errors.Select(x =>
                        new Error(x.ErrorCode, x.ErrorMessage)).ToList());

            var command = _mapper.Map<CreateUserCommand>(request);
            var result = await _sender.Send(
                _mapper.Map<CreateUserCommand>(request));

            if (result.IsFailure)
                return Result<UserResponse>.Failure(
                    result.Errors,
                    result.StatusCode);

            return Result<UserResponse>.Success(
                _mapper.Map<UserResponse>(result.Data));
        }
    }
}
