using Application.Users.CreateUser;
using Application.Users.UpdateUser;
using AutoMapper;
using Domain.Users;
using Domain.Users.DTOs;
using Domain.Users.ValueObjects;
using System.Reflection.Metadata.Ecma335;

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

            CreateMap<UpdateUserRequest, UpdateUserCommand>()
                .ForMember(dest => dest.Password,
                    opt => opt.MapFrom(
                        src => PasswordValueObject.Create(src.Password!).Data))
                .ForMember(dest => dest.ClassId,
                    opt => opt.MapFrom(
                        src => Guid.Parse(src.ClassId!)))
                .ForMember(dest => dest.ClassId,
                    opt => opt.Condition(
                        src => !string.IsNullOrEmpty(src.ClassId)))
                .ForMember(dest => dest.PhoneNumber,
                    opt => opt.MapFrom(
                        src => PhoneNumberValueObject.Create(src.PhoneNumber!).Data))
                .ForMember(dest => dest.PhoneNumber,
                    opt => opt.Condition(
                        src => !string.IsNullOrEmpty(src.PhoneNumber)));

            CreateMap<CreateUserCommand, User>();

            CreateMap<User, UserResponse>()
                .ForMember(dest => dest.Role,
                    opt => opt.MapFrom(
                        src => src.Role.ToString()))
                .ForMember(dest => dest.PhoneNumber,
                    opt => opt.MapFrom(
                        src => src.PhoneNumber.Value));
        }
    }
}
