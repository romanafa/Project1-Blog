using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Oblig2_Blog.Models;

namespace Oblig2_Blog.Configurations
{

    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<IdentityUser, UserDto>().ReverseMap();
        }
    }
}
