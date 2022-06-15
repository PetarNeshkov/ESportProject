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
                ModelState.AddModelError(nameof(input.Title), TeamExistingErrorMessage);

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

            TempData[GlobalMessageKey] = "Team was successfully created!";

            return RedirectToAction(nameof(All));
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

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var team= await teamService.GetByIdAsync(id);

            if (team == null)
            {
                return NotFound();
            }

            //if (!User.IsAdministrator())
            //{
            //    return Unauthorized();
            //}


            await teamService.DeleteTeamAsync(id);

            TempData[GlobalMessageKey] = "Team was successfully deleted!";

            return RedirectToAction(nameof(All));
        }


        public async Task<IActionResult> Edit(int id)
        {
            var team = await teamService.GetByIdAsync<EditTeamFormModel>(id);

            team.MidLaners = await teamService.GetRoleAsync(Role.Mid);
            team.BottomLaners = await teamService.GetRoleAsync(Role.Bottom);
            team.SupportLaners = await teamService.GetRoleAsync(Role.Support);
            team.JungleLaners = await teamService.GetRoleAsync(Role.Jungle);
            team.TopLaners = await teamService.GetRoleAsync(Role.Top);

            if (team==null)
            {
                return NotFound();
            }

            //if (!User.IsAdministrator())
            //{
            //    return Unauthorized();
            //}

            return View(team);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditTeamFormModel input)
        {
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
                ModelState.AddModelError(nameof(input.Title), TeamExistingErrorMessage);

                input.MidLaners = await teamService.GetRoleAsync(Role.Mid);
                input.BottomLaners = await teamService.GetRoleAsync(Role.Bottom);
                input.SupportLaners = await teamService.GetRoleAsync(Role.Support);
                input.JungleLaners = await teamService.GetRoleAsync(Role.Jungle);
                input.TopLaners = await teamService.GetRoleAsync(Role.Top);

                return View(input);
            }

            var teamAuthorId = await teamService.GetTeamAuthorIdAsync(input.Id);

            if (teamAuthorId != User.Id() && !User.IsAdministrator())
            {
                return Unauthorized();
            }

            await teamService.EditTeamAsync(
                input.Id,
                input.Title,
                input.ImageUrl,
                input.MidLaner,
                input.TopLaner,
                input.JungleLaner,
                input.BottomLaner,
                input.SupportLaner);

            TempData[GlobalMessageKey] = "Player was edited successfully!";

            return RedirectToAction(nameof(All));
        }
    }
}
