using AutoMapper;
using Domain.Abstract;
using Domain.Subjects.DTOs;
using FluentValidation;
using MediatR;

namespace Application.Subjects.CreateSubject
{
    public class CreateSubjectAction(
        ISender sender, 
        IValidator<CreateSubjectRequest> validator, 
        IMapper mapper)
    {
        private readonly ISender _sender = sender;
        private readonly IValidator<CreateSubjectRequest> _validator = validator;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<SubjectResponse>> Execute(CreateSubjectRequest request)
        {
            var validation = _validator.Validate(request);
            if (!validation.IsValid)
                return Result<SubjectResponse>.Failure(
                    validation.Errors
                        .Select(x => new Error(
                            x.ErrorCode, 
                            x.ErrorMessage))
                        .ToList());

            var result = await _sender.Send(
                _mapper.Map<CreateSubjectCommand>(request));

            if (result.IsFailure)
                return Result<SubjectResponse>.Failure(
                    result.Errors,
                    result.StatusCode);

            return Result<SubjectResponse>.Success(
                _mapper.Map<SubjectResponse>(result.Data));
        }
    }
}
