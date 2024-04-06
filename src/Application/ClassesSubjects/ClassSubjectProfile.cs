using Application.ClassesSubjects.CreateClassSubject;
using AutoMapper;
using Domain.ClassesSubjects;
using Domain.ClassesSubjects.DTOs;

namespace Application.ClassesSubjects
{
    public class ClassSubjectProfile: Profile
    {
        public ClassSubjectProfile()
        {
            CreateMap<CreateClassSubjectRequest, CreateClassSubjectCommand>();
            CreateMap<CreateClassSubjectCommand, ClassSubject>();

            CreateMap<ClassSubject, ClassSubjectResponse>().
                ForMember(dest => dest.ClassId,
                    opt => opt.MapFrom(
                        src => src.ClassId.ToString()))
                .ForMember(dest => dest.SubjectId,
                    opt => opt.MapFrom(
                        src => src.SubjectId.ToString()));
                
        }
    }
}
