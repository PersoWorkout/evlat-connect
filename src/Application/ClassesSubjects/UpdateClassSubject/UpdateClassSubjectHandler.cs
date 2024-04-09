using Application.Classes;
using Application.Subjects;
using Domain.Abstract;
using Domain.ClassesSubjects;
using Domain.ClassesSubjects.DTOs;
using Domain.ClassesSubjects.Errors;
using Domain.Subjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.ClassesSubjects.UpdateClassSubject
{
    public class UpdateClassSubjectHandler : IRequestHandler<UpdateClassSubjectCommand, Result<ClassSubject>>
    {
        private readonly ISubjectRepository _subjectRepository;
        private readonly IClassSubjectRepository _classSubjectRepository;

        public UpdateClassSubjectHandler(
            ISubjectRepository subjectRepository, 
            IClassSubjectRepository classSubjectRepository)
        {
            _subjectRepository = subjectRepository;
            _classSubjectRepository = classSubjectRepository;
        }

        public async Task<Result<ClassSubject>> Handle(UpdateClassSubjectCommand request, CancellationToken cancellationToken)
        {
            var classSubject = await _classSubjectRepository
                .GetByClassAndDate(request.ClassId, request.StartedDate);

            if (classSubject is null)
                return Result<ClassSubject>.Failure(
                    ClassSubjectErrors.ClassSubjectNotFound,
                    HttpStatusCode.NotFound);

            if(request.SubjectId.HasValue)
            {
                var subject = await _subjectRepository.GetById(request.SubjectId.Value);

                if (subject is null)
                    return Result<ClassSubject>.Failure(
                        SubjectErrors.NotFound(request.SubjectId.Value.ToString()),
                        HttpStatusCode.NotFound);
            }

            classSubject.Update(
                request.SubjectId,
                request.FinishedDate,
                request.Message);

            await _classSubjectRepository.Update(classSubject);

            return Result<ClassSubject>.Success(classSubject);
        }
    }
}
