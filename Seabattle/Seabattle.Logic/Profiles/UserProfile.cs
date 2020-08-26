using AutoMapper;
using Microsoft.AspNet.Identity.EntityFramework;
using Seabattle.Logic.Models;

namespace Seabattle.Logic.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<IdentityUser, UserDto>();
        }
    }
}
