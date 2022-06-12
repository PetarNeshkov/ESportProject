using E_SportManager.Data;
using E_SportManager.Data.enums;
using E_SportManager.Models.Teams;

namespace E_SportManager.Service.Data.Teams
{
    public interface ITeamService
    {
        Task<IEnumerable<Player>> GetRoleAsync(Role role);

        Task<TeamImageServiceModel> GetImageAsync(string name);
    }
}
