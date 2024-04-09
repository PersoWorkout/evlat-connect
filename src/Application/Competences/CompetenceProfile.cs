using Application.Competences.CreateCompetence;
using AutoMapper;
using Domain.Competences;
using Domain.Competences.DTOs;

namespace Application.Competences
{
    public class CompetenceProfile: Profile
    {
        public CompetenceProfile()
        {
            CreateMap<CreateCompetenceRequest, CreateCompetenceCommand>()
                .ForMember(dest => dest.SubjectId,
                    opt => opt.MapFrom(
                        src => Guid.Parse(src.SubjectId)));

            CreateMap<CreateCompetenceCommand, Competence>();

            CreateMap<Competence, CompetenceResponse>()
                .ForMember(dest => dest.SubjectId,
                    opt => opt.MapFrom(
                        src => src.SubjectId.ToString()))
                .ForMember(dest => dest.Id,
                    opt => opt.MapFrom(
                        src => src.Id.ToString()));
        }
    }
}
