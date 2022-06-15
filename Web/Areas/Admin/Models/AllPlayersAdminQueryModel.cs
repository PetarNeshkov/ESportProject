using E_SportManager.Models.Players;

namespace E_SportManager.Areas.Admin.Models
{
    public class AllPlayersAdminQueryModel
    {
        public IEnumerable<AdminPlayerServiceModel> Players { get; set; }
    }
}
