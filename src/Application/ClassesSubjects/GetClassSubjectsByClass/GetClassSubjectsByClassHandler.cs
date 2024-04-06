using Application.Classes;
using Domain.Abstract;
using Domain.Classes.Errors;
using Domain.ClassesSubjects;
using MediatR;
using System.Net;

namespace Application.ClassesSubjects.GetClassSubjectsByClass
{
    public class GetClassSubjectsByClassHandler(
        IClassSubjectRepository classSubjectRepository, 
        IClassRepository classRepository) : IRequestHandler<GetClassSubjectsByClassQuery, Result<IEnumerable<ClassSubject>>>
    {
        private readonly IClassSubjectRepository _classSubjectRepository = classSubjectRepository;
        private readonly IClassRepository _classRepository = classRepository;

        public async Task<Result<IEnumerable<ClassSubject>>> Handle(GetClassSubjectsByClassQuery request, CancellationToken cancellationToken)
        {
            var classEntity = await _classRepository.GetById(request.ClassId);
            if(classEntity is null)
                return Result<IEnumerable<ClassSubject>>.Failure(
                    ClassErrors.ClassNotFound(request.ClassId.ToString()),
                    HttpStatusCode.NotFound);

            var classSubjects = await _classSubjectRepository.GetByClass(
                request.ClassId, 
                request.From, 
                request.To);

            return Result<IEnumerable<ClassSubject>>
                .Success(classSubjects);
        }
    }
}
