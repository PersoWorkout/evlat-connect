using Application.Users;
using Domain.Abstract;
using Domain.ClassesSubjects;
using Domain.Users;
using Domain.Users.Errors;
using MediatR;
using System.Net;

namespace Application.ClassesSubjects.GetClassSubjectsByProfessor
{
    public class GetClassSubjectsByProfessorHandler(
        IClassSubjectRepository classSubjectRepository, 
        IUserRepostory userRepository) : IRequestHandler<GetClassSubjectsByProfessorQuery, Result<IEnumerable<ClassSubject>>>
    {
        private readonly IClassSubjectRepository _classSubjectRepository = classSubjectRepository;
        private readonly IUserRepostory _userRepository = userRepository;
        public async Task<Result<IEnumerable<ClassSubject>>> Handle(GetClassSubjectsByProfessorQuery request, CancellationToken cancellationToken)
        {
            var professor = await _userRepository.GetUserById(request.ProfessorId);
            if (professor is null || professor.Role != UserRole.Professeur)
                return Result<IEnumerable<ClassSubject>>.Failure(
                    UserErrors.ProfessorNotFound(request.ProfessorId.ToString()),
                    HttpStatusCode.NotFound);

            var classSubjects = await _classSubjectRepository.GetByProfessorId(
                request.ProfessorId,
                request.From,
                request.To);

            return Result<IEnumerable<ClassSubject>>
                .Success(classSubjects);
        }
    }
}
