using Application.Auth.Login;
using AutoMapper;
using Domain.Auth;
using Domain.Auth.DTOs;
using Domain.Users.ValueObjects;

namespace Application.Auth
{
    public class AuthProfile: Profile
    {
        public AuthProfile()
        {
            CreateMap<LoginRequest, LoginCommand>()
                .ForMember(dest => dest.Password,
                    opt => opt.MapFrom(
                        src => PasswordValueObject.Create(
                            src.Password).Data));

            CreateMap<Session, AuthenticatedResponse>()
                .ForMember(dest => dest.Token,
                    opt => opt.MapFrom(
                        src => src.Token.Value));
        }
    }
}
