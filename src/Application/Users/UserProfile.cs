using Application.Users.CreateUser;
using AutoMapper;
using Domain.Users;
using Domain.Users.DTOs;
using Domain.Users.ValueObjects;

namespace Application.Users
{
    public class UserProfile: Profile
    {
        public UserProfile() 
        {
            CreateMap<CreateUserRequest, CreateUserCommand>()
                .ForMember(dest => dest.Role,
                    opt => opt.MapFrom(
                        src => (UserRole)src.Role))
                .ForMember(dest => dest.Password, 
                    opt => opt.MapFrom(
                        src => PasswordValueObject.Create(src.Password).Data))
                .ForMember(dest => dest.PhoneNumber,
                    opt => opt.MapFrom(
                        src => PhoneNumberValueObject.Create(src.PhoneNumber).Data));

            CreateMap<CreateUserCommand, User>();

            CreateMap<User, UserResponse>()
                .ForMember(dest => dest.Role,
                    opt => opt.MapFrom(
                        src => src.Role.ToString()))
                .ForMember(dest => dest.Password,
                    opt => opt.MapFrom(
                        src => src.Password.Value))
                .ForMember(dest => dest.PhoneNumber,
                    opt => opt.MapFrom(
                        src => src.PhoneNumber.Value));
        }
    }
}
