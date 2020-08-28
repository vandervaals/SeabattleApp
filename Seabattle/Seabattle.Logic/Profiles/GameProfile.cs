using AutoMapper;
using Seabattle.Data.Models;
using Seabattle.Logic.Models;

namespace Seabattle.Logic.Profiles
{
    class GameProfile : Profile
    {
        public GameProfile()
        {
            CreateMap<GameDb, GameDto>()
                .ReverseMap();
        }
    }
}
