using Application.Users;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
