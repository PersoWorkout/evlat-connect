using Application.Subjects.CreateSubject;
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

            CreateMap<Subject, SubjectResponse>()
                .ForMember(dest => dest.Id,
                    opt => opt.MapFrom(
                        src => src.Id.ToString()));
        }
    }
}
