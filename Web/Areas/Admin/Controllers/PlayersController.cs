using E_SportManager.Models.Players;
using E_SportManager.Service.Data.Players;
using Microsoft.AspNetCore.Mvc;

namespace E_SportManager.Areas.Admin.Controllers
{
    public class PlayersController : AdminController
    {
        private readonly IPlayerService playerService;

        public PlayersController(IPlayerService player)
            =>this.playerService = player;

        public async Task<IActionResult> All()
        {
            var players= await playerService
                    .GetAllPlayersAsync<AdminPlayerServiceModel>(publicOnly:false);

            return View(players);
        }

        public IActionResult ChangeVisibility(int id)
        {
            playerService.ChangeVisibility(id);

            return RedirectToAction(nameof(All));
        }
    }
}
