using AutoMapper;
using Domain.Abstract;
using Domain.Competences.DTOs;
using Domain.Competences.Errors;
using FluentValidation;
using MediatR;
using System.Net;

namespace Application.Competences.UpdateCompetence
{
    public class UpdateCompetenceAction(
        ISender sender, 
        IValidator<UpdateCompetenceRequest> validator, 
        IMapper mapper)
    {
        private readonly ISender _sender = sender;
        private readonly IValidator<UpdateCompetenceRequest> _validator = validator;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<CompetenceResponse>> Execute(string id, UpdateCompetenceRequest request)
        {
            if(!Guid.TryParse(id, out var competenceId))
                return Result<CompetenceResponse>.Failure(
                    CompetenceErrors.CompetenceNotFound(id),
                    HttpStatusCode.NotFound);

            var validation = _validator.Validate(request);
            if(!validation.IsValid)
                return Result<CompetenceResponse>.Failure(
                    validation.Errors
                        .Select(x => new Error(x.ErrorCode, x.ErrorMessage))
                        .ToList());

            var command = _mapper.Map<UpdateCompetenceCommand>(request);
            command.Id = competenceId;

            var result = await _sender.Send(command);

            if (result.IsFailure)
                return Result<CompetenceResponse>.Failure(
                    result.Errors,
                    result.StatusCode);

            return Result<CompetenceResponse>.Success(
                _mapper.Map<CompetenceResponse >(result.Data));
        }
    }
}
