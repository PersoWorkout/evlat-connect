using Application.Classes;
using Application.Subjects;
using AutoMapper;
using Domain.Abstract;
using Domain.Classes.Errors;
using Domain.ClassesSubjects;
using Domain.ClassesSubjects.Errors;
using Domain.Subjects;
using MediatR;

namespace Application.ClassesSubjects.CreateClassSubject
{
    public class CreateClassSubjectHandler(
        IClassSubjectRepository classSubjectRepository, 
        IClassRepository classRepository, 
        ISubjectRepository subjectRepository, 
        IMapper mapper) : IRequestHandler<CreateClassSubjectCommand, Result<ClassSubject>>
    {
        private readonly IClassSubjectRepository _classSubjectRepository = classSubjectRepository;
        private readonly IClassRepository _classRepository = classRepository;
        private readonly ISubjectRepository _subjectRepository = subjectRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<ClassSubject>> Handle(CreateClassSubjectCommand request, CancellationToken cancellationToken)
        {
            var classEntity = await _classRepository.GetById(request.ClassId);
            if (classEntity is null)
                return Result<ClassSubject>.Failure(
                    ClassErrors.ClassNotFound(request.ClassId.ToString()),
                    System.Net.HttpStatusCode.NotFound);

            var subject = await _subjectRepository.GetById(request.SubjectId);
            if (subject is null)
                return Result<ClassSubject>.Failure(
                    SubjectErrors.NotFound(request.SubjectId.ToString()),
                    System.Net.HttpStatusCode.NotFound);

            if (await _classSubjectRepository
                .ExistByClassAndDates(
                    request.ClassId,
                    request.StartedAt,
                    request.FinishedAt))
                return Result<ClassSubject>.Failure(
                    ClassSubjectErrors.CoursAlreadyExist);

            var classSubject = _mapper.Map<ClassSubject>(request);
            await _classSubjectRepository.Create(classSubject);

            return Result<ClassSubject>.Success(classSubject);
        }
    }
}
