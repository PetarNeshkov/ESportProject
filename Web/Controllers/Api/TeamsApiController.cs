using E_SportManager.Service.Data.Teams;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_SportManager.Controllers.Api
{
    [Authorize]
    [ApiController]
  
    [Route("api/images")]
    public class TeamsApiController : ControllerBase
    {
        private readonly ITeamService teamService;

        public TeamsApiController(ITeamService teamService)
            =>this.teamService = teamService;

        [HttpGet]
        [Route("player")]
        public async Task<TeamImageServiceModel> GetImage(string title)
            =>await teamService.GetImageAsync(title);

        [HttpGet]
        [Route("team")]
        public async Task<TeamImageServiceModel> GetTeamImage(string title)
            => await teamService.GetImageTeamAsync(title);
    }
}
