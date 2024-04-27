using Application.CompetencesLink.CreateCompetenceLink;
using AutoMapper;
using Domain.CompetencesLinks;
using Domain.CompetencesLinks.DTOs;

namespace Application.CompetencesLink
{
    public class CompetenceLinkProfile: Profile
    {
        public CompetenceLinkProfile()
        {
            CreateMap<AddCompetenceLinkRequest, CreateCompetenceLinkCommand>()
                .ForMember(dest => dest.Type,
                    opt => opt.MapFrom(
                        src => (LinkType)src.Type))
                .ForMember(dest => dest.CompetenceId,
                    opt => opt.MapFrom(
                        src => Guid.Parse(src.CompetenceId)));

            CreateMap<CreateCompetenceLinkCommand, CompetenceLink>();

            CreateMap<CompetenceLink, CompetenceLinkResponse>()
                .ForMember(dest => dest.Id,
                    opt => opt.MapFrom(
                        src => src.Id.ToString()))
                .ForMember(dest => dest.CompetenceId,
                    opt => opt.MapFrom(
                        src => src.CompetenceId.ToString()));
        }
    }
}
