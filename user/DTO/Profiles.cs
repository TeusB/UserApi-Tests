using AutoMapper;
using user.DTO;
using user.Models;

namespace DTO.Profiles
{
    public class ProfileC : Profile
    {
        public ProfileC()
        {
            CreateMap<InsertUser, User>();
            CreateMap<UpdateUser, User>();
        }
    }
}
