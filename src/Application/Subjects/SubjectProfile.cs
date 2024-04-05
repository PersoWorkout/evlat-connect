using Application.Subjects.CreateSubject;
using Application.Subjects.UpdateSubject;
using AutoMapper;
using Domain.Subjects;
using Domain.Subjects.DTOs;

namespace Application.Subjects
{
    public class SubjectProfile: Profile
    {
        public SubjectProfile()
        {
            CreateMap<CreateSubjectRequest, CreateSubjectCommand>();
            CreateMap<CreateSubjectCommand, Subject>();

            CreateMap<UpdateSubjectRequest, UpdateSubjectCommand>();

            CreateMap<Subject, SubjectResponse>()
                .ForMember(dest => dest.Id,
                    opt => opt.MapFrom(
                        src => src.Id.ToString()));
        }
    }
}
