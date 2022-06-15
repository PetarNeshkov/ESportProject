using E_SportManager.Data;
using E_SportManager.Models.Players;
using E_SportManager.Service.Data.Players;
using E_SportManager.Service.Providers.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using static E_SportManager.Common.ErrorMessages.Player;
using static E_SportManager.Common.GlobalConstants;
using static E_SportManager.Common.GlobalConstants.Player;

namespace E_SportManager.Controllers
{
    [Authorize]
    public class PlayersController : Controller
    {
        private readonly ESportDbContext data;
        private readonly IPlayerService playerService;

        public PlayersController(ESportDbContext data, IPlayerService playerService)
        {
            this.data = data;
            this.playerService = playerService;
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(AddPlayerFormModel input)
        {
            var isExisting = await playerService.IsExistingAsync(input.Name);

            if (!ModelState.IsValid)
            {
                return View();
            }

            if (isExisting)
            {
                ModelState.AddModelError(nameof(input.Name), PlayerExistingErrorMessage);
                return View(input);
            }


            var playerId = await playerService.CreatePlayerAsync(
                input.Name,
                input.ImageUrl,
                input.YearsOfExperience,
                input.Role,
                input.Division,
                input.Description,
                User.Id());


            TempData[GlobalMessageKey] = "Player was successfully created and is awaiting for approval!";

            return RedirectToAction(nameof(Details), new { id = playerId });
        }

        [AllowAnonymous]
        public async Task<IActionResult> All(AllPlayersQueryModel query, int page = 1)
        {
            var count = await playerService.GetTotalPlayersCountAsync();
            var skip = (page - 1) * PlayersPerPage;

            var players = await playerService.GetAllPlayersAsync<PlayerServiceModel>(skip);

            query.Players = players;
            query.Pagination = PaginationProvider.PaginationHelper(page, count, PlayersPerPage);

            return View(query);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var player = await playerService.GetByIdAsync<EditPlayerFormModel>(id);

            if (player == null)
            {
                return NotFound();
            }

            if (User.IsAdministrator() || player.AuthorId!=User.Id())
            {
                return Unauthorized();
            }

            return View(player);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditPlayerFormModel input)
        {
            if (!ModelState.IsValid)
            {
                return View(input);
            }

            var playerAuthorId = await playerService.GetPlayerAuthorIdAsync(input.Id);

            if (User.IsAdministrator() || playerAuthorId!=User.Id())
            {
                return Unauthorized();
            }

            var isExisting = await playerService.IsExistingAsync(input.Name,input.Id);

            if (isExisting)
            {
                ModelState.AddModelError(nameof(input.Name), PlayerExistingErrorMessage);
                return View(input);
            }


            await playerService.EditPlayerAsync(
                input.Id,
                input.Name,
                input.ImageUrl,
                input.YearsOfExperience,
                input.Role,
                input.Division,
                input.Description);

            TempData[GlobalMessageKey] = "Player was successfully edited and awaiting for approval!";

            return RedirectToAction(nameof(Details), new {id=input.Id});
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var player=await playerService.GetByIdAsync(id);

            if (player==null)
            {
                return NotFound();
            }

            if (User.IsAdministrator() || player.AuthorId!=User.Id())
            {
                return Unauthorized();
            }

            await playerService.DeletePlayerAsync(id);

            TempData[GlobalMessageKey] = "Player was successfully deleted!";


            return RedirectToAction(nameof(All));
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var player = await playerService.GetByIdAsync<PlayerDetailsViewModel>(id);

            if (player ==null)
            {
                return NotFound();
            }

            return View(player);
        }
    }
}
