using AutoMapper;
using Domain.Abstract;
using Domain.Classes.DTOs;
using FluentValidation;
using MediatR;

namespace Application.Classes.CreateClass
{
    public class CreateClassAction(
        ISender sender, 
        IValidator<CreateClassRequest> validator, 
        IMapper mapper)
    {
        private readonly ISender _sender = sender;
        private readonly IValidator<CreateClassRequest> _validator = validator;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<ClassResponse>> Execute(CreateClassRequest request)
        {
            var validation = _validator.Validate(request);
            if(!validation.IsValid)
                return Result<ClassResponse>.Failure(
                    validation.Errors.Select(x => 
                        new Error(x.ErrorCode, x.ErrorMessage))
                    .ToList());

            var result = await _sender.Send(
                _mapper.Map<CreateClassCommand>(request));

            if (result.IsFailure)
                return Result<ClassResponse>.Failure(
                    result.Errors,
                    result.StatusCode);

            return Result<ClassResponse>.Success(
                _mapper.Map<ClassResponse>(result.Data));
        }
    }
}
