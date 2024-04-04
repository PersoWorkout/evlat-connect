using Application.Users;
using Domain.Abstract;
using Domain.Classes;
using Domain.Users.Errors;
using MediatR;
using System.Net;

namespace Application.Classes.GetClassesByProfessor
{
    public class GetClassesByProfessorHandler(
        IClassRepository classRepository, 
        IUserRepostory userRepository) : IRequestHandler<GetClassesByProfessorQuery, Result<IEnumerable<Class>>>
    {
        private readonly IClassRepository _classRepository = classRepository;
        private readonly IUserRepostory _userRepository = userRepository;

        public async Task<Result<IEnumerable<Class>>> Handle(GetClassesByProfessorQuery request, CancellationToken cancellationToken)
        {
            var professor = await _userRepository.GetUserById(request.ProfessorId);
            if (professor is null)
                return Result<IEnumerable<Class>>.Failure(
                    UserErrors.ProfessorNotFound(request.ProfessorId.ToString()),
                    HttpStatusCode.NotFound);

            var classes = await _classRepository.GetByProfessor(request.ProfessorId);

            return Result<IEnumerable<Class>>.Success(classes);
        }
    }
}
