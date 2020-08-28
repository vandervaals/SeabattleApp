using AutoMapper;
using Seabattle.Data.Models;
using Seabattle.Logic.Models;

namespace Seabattle.Logic.Profiles
{
    class ShotProfile: Profile
    {
        public ShotProfile()
        {
            CreateMap<ShotDb, ShotDto>()
                .ReverseMap();
        }
    }
}
