using AutoMapper;
using Domain.Abstract;
using Domain.Auth.DTOs;
using FluentValidation;
using MediatR;

namespace Application.Auth.Login
{
    public class LoginAction(
        ISender sender, 
        IValidator<LoginRequest> validator, 
        IMapper mapper)
    {
        private readonly ISender _sender = sender;
        private readonly IValidator<LoginRequest> _validator = validator;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<AuthenticatedResponse>> Execute(LoginRequest request)
        {
            var validation = _validator.Validate(request);
            if (!validation.IsValid)
                return Result<AuthenticatedResponse>.Failure(
                    validation.Errors
                        .Select(x => 
                            new Error(x.ErrorCode, x.ErrorMessage))
                        .ToList());

            var result = await _sender.Send(
                _mapper.Map<LoginCommand>(request));

            if(result.IsFailure)
                return Result<AuthenticatedResponse>.Failure(
                    result.Errors,
                    result.StatusCode);

            return Result<AuthenticatedResponse>.Success(
                _mapper.Map<AuthenticatedResponse>(result.Data));
        }
    }
}
