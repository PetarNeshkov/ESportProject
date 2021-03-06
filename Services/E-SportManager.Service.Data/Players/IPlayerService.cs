using E_SportManager.Data;

namespace E_SportManager.Service.Data.Players
{
    public interface IPlayerService
    {
        Task<int> CreatePlayerAsync(string name,string imageUrl,int yearsOfExperience,
            string role,string division,string description,string userId);

        Task<bool> IsExistingAsync(string name);

        Task<bool> IsExistingAsync(string name,int id);

        Task<IEnumerable<TModel>> GetAllPlayersAsync<TModel>(int skip=0,bool publicOnly=true);
        Task<IEnumerable<TModel>> GetAllPlayersAsync<TModel>(bool publicOnly = true);

        Task<TModel> GetByIdAsync<TModel>(int id);

        Task<Player> GetByIdAsync(int id);

        Task EditPlayerAsync(int playerId,string name,string imageUrl, int yearsOfExperience, string role,string division,
            string description, bool isPublic=false);

        Task DeletePlayerAsync(int id);

        Task<int> GetTotalPlayersCountAsync();

        Task<string> GetPlayerAuthorIdAsync(int id);

        void ChangeVisibility(int id);
    }
}
