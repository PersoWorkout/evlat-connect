using AutoMapper;
using Domain.Abstract;
using Domain.CompetencesLinks.DTOs;
using FluentValidation;
using MediatR;

namespace Application.CompetencesLink.CreateCompetenceLink
{
    public class CreateCompetenceLinkAction(
        ISender sender, 
        IValidator<AddCompetenceLinkRequest> validator, 
        IMapper mapper)
    {
        private readonly ISender _sender = sender;
        private readonly IValidator<AddCompetenceLinkRequest> _validator = validator;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<CompetenceLinkResponse>> Execute(AddCompetenceLinkRequest request)
        {
            var validation = _validator.Validate(request);
            if (!validation.IsValid)
                return Result<CompetenceLinkResponse>.Failure(
                    validation.Errors
                        .Select(x => new Error(x.ErrorCode, x.ErrorMessage))
                        .ToList());

            var result = await _sender.Send(
                _mapper.Map<CreateCompetenceLinkCommand>(request));

            if (result.IsFailure)
                return Result<CompetenceLinkResponse>.Failure(
                    result.Errors,
                    result.StatusCode);

            return Result<CompetenceLinkResponse>.Success(
                _mapper.Map<CompetenceLinkResponse>(result.Data));
        }
    }
}
