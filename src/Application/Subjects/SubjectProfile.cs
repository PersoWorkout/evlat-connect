using AutoMapper;
using Domain.Subjects;
using Domain.Subjects.DTOs;

namespace Application.Subjects
{
    public class SubjectProfile: Profile
    {
        public SubjectProfile()
        {
            CreateMap<Subject, SubjectResponse>()
                .ForMember(dest => dest.Id,
                    opt => opt.MapFrom(
                        src => src.Id.ToString()));
        }
    }
}
