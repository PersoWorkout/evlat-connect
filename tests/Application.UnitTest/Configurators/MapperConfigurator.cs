using Application.Classes;
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
    }
}
