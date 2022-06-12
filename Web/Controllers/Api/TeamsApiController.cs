using E_SportManager.Models.Teams;
using E_SportManager.Service.Data.Teams;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
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
        public async Task<TeamImageServiceModel> GetImage(string title)
            =>await teamService.GetImageAsync(title);
    }
}
