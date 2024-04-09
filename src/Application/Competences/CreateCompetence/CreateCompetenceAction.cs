using AutoMapper;
using Domain.Abstract;
using Domain.Competences.DTOs;
using FluentValidation;
using MediatR;

namespace Application.Competences.CreateCompetence
{
    public class CreateCompetenceAction(
        ISender sender, 
        IValidator<CreateCompetenceRequest> validator, 
        IMapper mapper)
    {
        private readonly ISender _sender = sender;
        private readonly IValidator<CreateCompetenceRequest> _validator = validator;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<CompetenceResponse>> Execute(CreateCompetenceRequest request)
        {
            var validation = _validator.Validate(request);
            if (!validation.IsValid)
                return Result<CompetenceResponse>.Failure(
                    validation.Errors
                        .Select(x => new Error(x.ErrorCode, x.ErrorMessage))
                        .ToList());

            var result = await _sender.Send(
                _mapper.Map<CreateCompetenceCommand>(request));

            if(result.IsFailure)
                return Result<CompetenceResponse>.Failure(
                    result.Errors,
                    result.StatusCode);

            return Result<CompetenceResponse>.Success(
                _mapper.Map<CompetenceResponse>(result.Data));
        }
    }
}
