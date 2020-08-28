using AutoMapper;
using Seabattle.Data.Models;
using Seabattle.Logic.Models;

namespace Seabattle.Logic.Profiles
{
    class ShipProfile : Profile
    {
        public ShipProfile()
        {
            CreateMap<ShipDb, ShipDto>()
                .ReverseMap();
        }
    }
}
