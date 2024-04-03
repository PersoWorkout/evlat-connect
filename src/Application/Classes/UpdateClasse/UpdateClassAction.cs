using AutoMapper;
using Domain.Abstract;
using Domain.Classes.DTOs;
using Domain.Classes.Errors;
using FluentValidation;
using MediatR;
using System.Net;

namespace Application.Classes.UpdateClasse
{
    public class UpdateClassAction(
        ISender sender,
        IValidator<UpdateClassRequest> validator,
        IMapper mapper)
    {
        private readonly ISender _sender = sender;
        private readonly IValidator<UpdateClassRequest> _validator = validator;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<ClassResponse>> Execute(string id, UpdateClassRequest request)
        {
            if (!Guid.TryParse(id, out var classId))
                return Result<ClassResponse>.Failure(
                    ClassErrors.ClassNotFound(id),
                    HttpStatusCode.NotFound);

            var validation = _validator.Validate(request);

            if (!validation.IsValid)
                return Result<ClassResponse>.Failure(
                    validation.Errors
                    .Select(x => new Error(
                        x.ErrorCode, 
                        x.ErrorMessage))
                    .ToList());

            var command = _mapper.Map<UpdateClassCommand>(request);
            command.Id = classId;

            var result = await _sender.Send(command);

            if (result.IsFailure)
                return Result<ClassResponse>.Failure(
                    result.Errors,
                    result.StatusCode);

            return Result<ClassResponse>.Success(
                _mapper.Map<ClassResponse>(result.Data));
        }
    }
}
