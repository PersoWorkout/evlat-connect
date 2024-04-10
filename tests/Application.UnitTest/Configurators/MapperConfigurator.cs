using Application.Auth;
using Application.Classes;
using Application.ClassesSubjects;
using Application.Competences;
using Application.Subjects;
using Application.Users;
using AutoMapper;

namespace Application.UnitTest.Configurators
{
    public static class MapperConfigurator
    {
        public static IMapper ConfigureUserProfile()
        {
            return new MapperConfiguration(config =>
                config.AddProfile<UserProfile>())
                .CreateMapper();
        }

        public static IMapper ConfigureClassProfile()
        {
            return new MapperConfiguration(config =>
                config.AddProfile<ClassProfile>())
                .CreateMapper();
        }

        public static IMapper ConfigureAuthProfile()
        {
            return new MapperConfiguration(config =>
                config.AddProfile<AuthProfile>())
                .CreateMapper();
        }

        public static IMapper ConfigureSubjectProfile()
        {
            return new MapperConfiguration(config =>
                config.AddProfile<SubjectProfile>())
                .CreateMapper();
        }

        public static IMapper ConfigureClassSubjectProfile()
        {
            return new MapperConfiguration(config =>
                config.AddProfile<ClassSubjectProfile>())
                .CreateMapper();
        }

        public static IMapper ConfigureCompetenceProfile()
        {
            return new MapperConfiguration(config =>
                config.AddProfile<CompetenceProfile>())
                .CreateMapper();
        }

        public static IMapper ConfigureAll()
        {
            return new MapperConfiguration(config =>
            {
                config.AddProfile<AuthProfile>();
                config.AddProfile<ClassProfile>();
                config.AddProfile<UserProfile>();
                config.AddProfile<SubjectProfile>();
                
            }).CreateMapper();          
        }
    }
}
