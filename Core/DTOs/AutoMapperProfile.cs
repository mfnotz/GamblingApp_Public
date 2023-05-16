using AutoMapper;
using Core.Models;
using Newtonsoft.Json.Linq;

namespace Core.DTOs
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<BetDTO, Bet>();
            CreateMap<Bet, BetDTO>();
            CreateMap<User, UserDTO>();
            CreateMap<UserDTO, User>();
        }
    }
}
