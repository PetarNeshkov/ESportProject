using AutoMapper;
using E_SportManager.Data;
using E_SportManager.Models.Players;

namespace E_SportManager
{
    public class MappingProfiler : Profile
    {
        public MappingProfiler()
        {
            CreateMap<Player, PlayerServiceModel>();
            CreateMap<Player, EditPlayerFormModel>();
            CreateMap<Player,PlayerDeleteViewModel>();
            CreateMap<Player, PlayerDetailsViewModel>();
        }
    }
}
