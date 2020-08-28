using AutoMapper;
using Seabattle.Data.Models;
using Seabattle.Logic.Models;

namespace Seabattle.Logic.Profiles
{
    class CellProfile : Profile
    {
        public CellProfile()
        {
            CreateMap<CellDb, CellDto>()
                .ReverseMap();
        }
    }
}
