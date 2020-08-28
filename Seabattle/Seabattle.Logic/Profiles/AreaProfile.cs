using AutoMapper;
using Seabattle.Data.Models;
using Seabattle.Logic.Models;

namespace Seabattle.Logic.Profiles
{
    class AreaProfile: Profile
    {
        public AreaProfile()
        {
            CreateMap<AreaDb, AreaDto>()
                .ReverseMap();
        }
    }
}
