using AutoMapper;
using Domain.Abstract;
using Domain.Users.DTOs;
using Domain.Users.Errors;
using FluentValidation;
using MediatR;
using System.Net;

namespace Application.Users.UpdateUser
{
    public class UpdateUserAction(
        ISender sender, 
        IValidator<UpdateUserRequest> validator, 
        IMapper mapper)
    {
        private readonly ISender _sender = sender;
        private readonly IValidator<UpdateUserRequest> _validator = validator;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<UserResponse>> Execute(string id, UpdateUserRequest request)
        {
            if (!Guid.TryParse(id, out var userId))
                return Result<UserResponse>.Failure(
                    UserErrors.UserNotFound(id),
                    HttpStatusCode.NotFound);

            var validation = _validator.Validate(request);
            if(!validation.IsValid)
                return Result<UserResponse>.Failure(
                    validation.Errors
                        .Select(x => new Error(
                            x.ErrorCode, 
                            x.ErrorMessage))
                        .ToList());

            var command = _mapper.Map<UpdateUserCommand>(request);
            command.UserId = userId;

            var result = await _sender.Send(command);

            if (result.IsFailure)
                return Result<UserResponse>.Failure(
                    result.Errors,
                    result.StatusCode);

            return Result<UserResponse>.Success(
                _mapper.Map<UserResponse>(result.Data));
        }
    }
}
