using E_SportManager.Data;
using E_SportManager.Data.enums;
using E_SportManager.Models.Teams;
using E_SportManager.Service.Data.Teams;
using Microsoft.AspNetCore.Mvc;

namespace E_SportManager.Controllers
{
    public class TeamsController : Controller
    {
        private readonly ESportDbContext data;
        private readonly ITeamService teamService;

        public TeamsController(ESportDbContext data,ITeamService teamService)
        {
            this.data = data;
            this.teamService = teamService;
        }

        public async Task<IActionResult> Create() => View(new AddTeamFormModel
        {
            MidLaners = await teamService.GetRoleAsync(Role.Mid),
            BottomLaners= await teamService.GetRoleAsync(Role.Bottom),
            SupportLaners= await teamService.GetRoleAsync(Role.Support),
            JungleLaners= await teamService.GetRoleAsync(Role.Jungle),
            TopLaners= await teamService.GetRoleAsync(Role.Top)
        });
    }
}
