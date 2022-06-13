using E_SportManager.Service.Data.Pagination;

namespace E_SportManager.Models.Players
{
    public class AllPlayersQueryModel
    {
       public IEnumerable<PlayerServiceModel> Players { get; set; }

       public PaginationViewModel Pagination { get; set; }
    }
}
