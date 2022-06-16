using AutoMapper;
using E_SportManager.Areas.Admin.Models;
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
            CreateMap<Player, AdminPlayerServiceModel>();
            CreateMap<Player, AllPlayersAdminQueryModel>();

            CreateMap<Team, TeamServiceModel>();
            CreateMap<Team, EditTeamFormModel>()
             .ForMember(
                x => x.MidLaner,
                x => x.MapFrom(x => x.MidLaner.Name))
             .ForMember(
                x => x.TopLaner,
                x => x.MapFrom(x => x.TopLaner.Name))
             .ForMember(
                x => x.JungleLaner,
                x => x.MapFrom(x => x.JungleLaner.Name))
             .ForMember(
                x => x.BottomLaner,
                x => x.MapFrom(x => x.BottomLaner.Name))
             .ForMember(
                x => x.SupportLaner,
                x => x.MapFrom(x => x.SupportLaner.Name));
            CreateMap<Team, TeamDetailsViewModel>()
                 .ForMember(
                x => x.MidLaner,
                x => x.MapFrom(x => x.MidLaner.Name))
             .ForMember(
                x => x.TopLaner,
                x => x.MapFrom(x => x.TopLaner.Name))
             .ForMember(
                x => x.JungleLaner,
                x => x.MapFrom(x => x.JungleLaner.Name))
             .ForMember(
                x => x.BottomLaner,
                x => x.MapFrom(x => x.BottomLaner.Name))
             .ForMember(
                x => x.SupportLaner,
                x => x.MapFrom(x => x.SupportLaner.Name));

            CreateMap<Team, BattleTeamFormModel>()
              .ForMember(
                x => x.FirstTeamName,
                x => x.MapFrom(x => x.Title))
              .ForMember(
                x => x.SecondTeamName,
                x => x.MapFrom(x => x.Title));
        }
    }
}
