using Application.Classes.CreateClass;
using Application.Classes.UpdateClasse;
using AutoMapper;
using Domain.Classes;
using Domain.Classes.DTOs;

namespace Application.Classes
{
    public class ClassProfile: Profile
    {
        public ClassProfile()
        {
            CreateMap<CreateClassRequest, CreateClassCommand>()
                .ForMember(dest => dest.Type,
                    opt => opt.MapFrom(
                        src => (ClassType)src.Type))
                .ForMember(dest => dest.ProfessorId,
                    opt => opt.MapFrom(
                        src => Guid.Parse(src.ProfessorId)));

            CreateMap<UpdateClassRequest, UpdateClassCommand>()
                .ForMember(dest => dest.Type,
                    opt => opt.MapFrom(
                        src => (ClassType?)src.Type))
                .ForMember(dest => dest.ProfessorId,
                    opt => opt.MapFrom(
                        src => !string.IsNullOrEmpty(src.ProfessorId) ? 
                        Guid.Parse(src.ProfessorId) : 
                        Guid.Empty));

            CreateMap<CreateClassCommand, Class>();

            CreateMap<Class, ClassResponse>()
                .ForMember(dest => dest.Type,
                    opt => opt.MapFrom(
                        src => src.Type.ToString()))
                .ForMember(dest => dest.ProfessorId,
                    opt => opt.MapFrom(
                        src => src.ProfessorId.ToString()));
        }
    }
}
