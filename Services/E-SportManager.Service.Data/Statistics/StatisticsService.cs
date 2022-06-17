using E_SportManager.Service.Data.Teams;

namespace E_SportManager.Service.Data.Statistics
{
    public class StatisticsService : IStatisticsService
    {
        private readonly ITeamService teamService;

        public StatisticsService(ITeamService teamService)
            => this.teamService = teamService;


        public async Task<StatisticsServiceModel> Results(int id)
        {
            return new StatisticsServiceModel
            {
                Rating = await teamService.GetWonGamesAsync(id),
                WonGames = await teamService.GetTeamRatingAsync(id),
                LostGames = await teamService.GetLostGamesAsync(id)
            };
        }
    }
}
