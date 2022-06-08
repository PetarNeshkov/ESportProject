using E_SportManager.Data;
using E_SportManager.Models.Players;
using E_SportManager.Service.Data.Players;
using Microsoft.AspNetCore.Mvc;

using static E_SportManager.Common.ErrorMessages.Player;

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

            return View(Create());
        }

        public IActionResult All()
        {
            var players = playerService.GetAllPlayersAsync<PlayerServiceModel>();

            return View(players);
        }
    }
}
