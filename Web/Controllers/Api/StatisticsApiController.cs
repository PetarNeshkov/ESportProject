using E_SportManager.Service.Data.Statistics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_SportManager.Controllers.Api
{
    [Authorize]
    [ApiController]
    [Route("api/statistics")]
    public class StatisticsApiController : ControllerBase
    {
        private readonly IStatisticsService statisticsService;

        public StatisticsApiController(IStatisticsService statisticsService)
            => this.statisticsService = statisticsService;

        [HttpGet]
        public async Task<StatisticsServiceModel> GetStatistics(int id)
            => await statisticsService.Results(id);
    }
}
