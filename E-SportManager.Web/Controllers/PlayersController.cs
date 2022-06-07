using E_SportManager.Data;
using E_SportManager.Models.Players;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_SportManager.Controllers
{
    public class PlayersController:Controller
    {
        private readonly ESportDbContext data;

        public PlayersController(ESportDbContext data)
        {
            this.data = data;
        }

        //[Authorize]
        public IActionResult Create()=>View();
    }
}
