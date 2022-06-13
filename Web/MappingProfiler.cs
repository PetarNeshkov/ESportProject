using AutoMapper;
using E_SportManager.Data;
using E_SportManager.Models.Players;
using E_SportManager.Models.Teams;
using E_SportManager.Service.Data.Teams;

namespace E_SportManager
{
    public class MappingProfiler : Profile
    {
        public MappingProfiler()
        {
            CreateMap<Player, PlayerServiceModel>();
            CreateMap<Player, EditPlayerFormModel>();
            CreateMap<Player, PlayerDetailsViewModel>();
            CreateMap<Player, PlayerListServiceModel>();

            CreateMap<Team, TeamServiceModel>();
        }
    }
}
