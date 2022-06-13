using E_SportManager.Service.Data.Pagination;

namespace E_SportManager.Models.Teams
{
    public class AllTeamsQueryModel
    {
        public IEnumerable<TeamServiceModel> Teams { get; set; }

        public PaginationViewModel Pagination { get; set; }
    }
}
