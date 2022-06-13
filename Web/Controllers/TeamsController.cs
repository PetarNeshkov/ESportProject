using E_SportManager.Data;
using E_SportManager.Data.enums;
using E_SportManager.Models.Teams;
using E_SportManager.Service.Data.Teams;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using static E_SportManager.Common.GlobalConstants;
using static E_SportManager.Common.GlobalConstants.Team;
using static E_SportManager.Common.ErrorMessages.Team;
using E_SportManager.Service.Providers.Pagination;

namespace E_SportManager.Controllers
{
    [Authorize]
    public class TeamsController : Controller
    {
        private readonly ESportDbContext data;
        private readonly ITeamService teamService;

        public TeamsController(ESportDbContext data, ITeamService teamService)
        {
            this.data = data;
            this.teamService = teamService;
        }

        public async Task<IActionResult> Create()=>View(new AddTeamFormModel()
        {
            MidLaners = await teamService.GetRoleAsync(Role.Mid),
            BottomLaners = await teamService.GetRoleAsync(Role.Bottom),
            SupportLaners = await teamService.GetRoleAsync(Role.Support),
            JungleLaners = await teamService.GetRoleAsync(Role.Jungle),
            TopLaners = await teamService.GetRoleAsync(Role.Top)
        });
      

        [HttpPost]
        public async Task<IActionResult> Create(AddTeamFormModel input)
        {

            ModelState.Remove("MidLaners");
            ModelState.Remove("TopLaners");
            ModelState.Remove("BottomLaners");
            ModelState.Remove("SupportLaners");
            ModelState.Remove("JungleLaners");
            if (!ModelState.IsValid)
            {
                input.MidLaners = await teamService.GetRoleAsync(Role.Mid);
                input.BottomLaners = await teamService.GetRoleAsync(Role.Bottom);
                input.SupportLaners = await teamService.GetRoleAsync(Role.Support);
                input.JungleLaners = await teamService.GetRoleAsync(Role.Jungle);
                input.TopLaners = await teamService.GetRoleAsync(Role.Top);

                return View(input);
            }

            var isExisting = await teamService.IsExistingAsync(input.Title);

            if (isExisting)
            {
                ModelState.AddModelError(input.Title, TeamExistingErrorMessage);

                input.MidLaners = await teamService.GetRoleAsync(Role.Mid);
                input.BottomLaners = await teamService.GetRoleAsync(Role.Bottom);
                input.SupportLaners = await teamService.GetRoleAsync(Role.Support);
                input.JungleLaners = await teamService.GetRoleAsync(Role.Jungle);
                input.TopLaners = await teamService.GetRoleAsync(Role.Top);

                return View(input);
            }

            await teamService.CreateTeamAsync(
                input.Title,
                input.ImageUrl,
                input.MidLaner,
                input.TopLaner,
                input.JungleLaner,
                input.BottomLaner,
                input.SupportLaner,
                User.Id());

            TempData[GlobalMessageKey] = $"Team was successfully created!";

            return View(input);
        }

        [AllowAnonymous]
        public async Task<IActionResult> All(AllTeamsQueryModel query,int page=1)
        {
            var count= await teamService.GetTotalTeamsCountAsync();
            var skip=(page-1) * TeamsPerPage;

            var teams = await teamService.GetAllTeamsAsync<TeamServiceModel>(skip);

            query.Teams = teams;
            query.Pagination = PaginationProvider.PaginationHelper(page, count, TeamsPerPage);

            return View(query);
        }
    }
}
