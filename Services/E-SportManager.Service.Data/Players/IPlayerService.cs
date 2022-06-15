using E_SportManager.Data;

namespace E_SportManager.Service.Data.Players
{
    public interface IPlayerService
    {
        Task CreatePlayerAsync(string name,string imageUrl,int yearsOfExperience,
            string role,string division,string description);

        Task<bool> IsExistingAsync(string name);

        Task<IEnumerable<TModel>> GetAllPlayersAsync<TModel>(int skip=0,bool publicOnly=true);

        Task<TModel> GetByIdAsync<TModel>(int id);

        Task<Player> GetByIdAsync(int id);

        Task EditPlayerAsync(int playerId,string name,string imageUrl, int yearsOfExperience, string role,string division,
            string description);

        Task DeletePlayerAsync(int id);

        Task<int> GetTotalPlayersCountAsync();
    }
}
