using E_SportManager.Data;

namespace E_SportManager.Service.Data.Players
{
    public interface IPlayerService
    {
        Task CreatePlayerAsync(string name,string imageUrl,int yearsOfExperience,
            string role,string division,string description);

        Task<bool> IsExistingAsync(string name);

        Task<IEnumerable<TModel>> GetAllPlayersAsync<TModel>();

        Task<TModel> GetByIdAsync<TModel>(int id);

        Task EditPlayerAsync(int playerId,string name,string imageUrl,string role,string division,
            string description);
    }
}
