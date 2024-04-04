using Application.Users;
using Domain.Abstract;
using Domain.Classes;
using Domain.Classes.Errors;
using Domain.Users.Errors;
using MediatR;
using System.Net;

namespace Application.Classes.UpdateClasse
{
    public class UpdateClassHandler(
        IClassRepository repository,
        IUserRepostory userRepository) : IRequestHandler<UpdateClassCommand, Result<Class>>
    {
        private readonly IClassRepository _repository = repository;
        private readonly IUserRepostory _userRepository = userRepository;

        public async Task<Result<Class>> Handle(UpdateClassCommand request, CancellationToken cancellationToken)
        {
            var classEntity = await _repository.GetById(request.Id);
            if(classEntity is null)
                return Result<Class>.Failure(
                    ClassErrors.ClassNotFound(request.Id.ToString()),
                    HttpStatusCode.NotFound);

            if (request.ProfessorId.HasValue)
            {
                var professor = await _userRepository.GetUserById(
                    request.ProfessorId.Value);

                if(professor is null)
                    return Result<Class>.Failure(
                        UserErrors.ProfessorNotFound(request.Id.ToString()),
                        HttpStatusCode.NotFound);
            }

            classEntity.Update(
                request.Promotion,
                request.Name,
                request.IsActive,
                request.Type,
                request.ProfessorId);

            await _repository.Update(classEntity);

            return Result<Class>.Success(classEntity);
        }
    }
}
