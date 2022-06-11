using E_SportManager.Data;
using E_SportManager.Models.Players;
using E_SportManager.Service.Data.Players;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using static E_SportManager.Common.ErrorMessages.Player;
using static E_SportManager.Common.GlobalConstants;

namespace E_SportManager.Controllers
{
    public class PlayersController:Controller
    {
        private readonly ESportDbContext data;
        private readonly IPlayerService playerService;

        public PlayersController(ESportDbContext data,IPlayerService playerService)
        {
            this.data = data;
            this.playerService = playerService;
        }

        //[Authorize]
        public IActionResult Create()=>View();

        [HttpPost]
        //[Authorize]
        public async Task<IActionResult> Create(AddPlayerFormModel input)
        {
            var isExisting= await playerService.IsExistingAsync(input.Name);

            if (!ModelState.IsValid)
            {
                return View();
            }

            if (isExisting)
            {
                ModelState.AddModelError(input.Name, PlayerExistingErrorMessage);
                return View(input);
            }


            await playerService.CreatePlayerAsync(
                input.Name,
                input.ImageUrl,
                input.YearsOfExperience,
                input.Role,
                input.Division,
                input.Description);


            TempData[GlobalMessageKey] = $"Player was successfully created!";

            return RedirectToAction(nameof(All));
        }

        public async Task<IActionResult> All(AllPlayersQueryModel query)
        {
            var players =await playerService.GetAllPlayersAsync<PlayerServiceModel>();

            query.Players = players;

            return View(query);
        }

        //[Authorize(Roles = Administrator.AdministratorRoleName)]
        public async Task<IActionResult> Edit(int id)
        {
            var player= await playerService.GetByIdAsync<EditPlayerFormModel>(id);

            if (player == null)
            {
                return NotFound();
            }

            //if (!User.IsAdministrator())
            //{
            //    return Unauthorized();
            //}

            return View(player);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditPlayerFormModel input)
        {
            if (!ModelState.IsValid)
            {
                return View(input);
            }

            //if (!User.IsAdministrator())
            //{
            //    return Unauthorized();
            //}

            await playerService.EditPlayerAsync(
                input.Id,
                input.Name,
                input.ImageUrl,
                input.Role,
                input.Division,
                input.Description);

            TempData[GlobalMessageKey] = $"Player was successfully edited!";

            return RedirectToAction(nameof(All));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var player=await playerService.GetByIdAsync<PlayerDeleteViewModel>(id);

            if (player==null)
            {
                return NotFound();
            }

            //if (!User.IsAdministrator())
            //{
            //    return Unauthorized();
            //}

            await playerService.DeletePlayerAsync(id);

            TempData[GlobalMessageKey] = $"Player was successfully deleted!";


            return RedirectToAction(nameof(All));
        }

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
