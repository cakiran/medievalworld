using AutoMapper;
using medievalworldweb.Dtos.Character;
using medievalworldweb.Dtos.Fight;
using medievalworldweb.Models;

namespace medievalworldweb
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Character, GetCharacterDto>();
            CreateMap<Character, GetFighterDto>()
            .ForMember(dest => dest.UserId, option => option.MapFrom(src => src.User.Id));
        }
    }
}