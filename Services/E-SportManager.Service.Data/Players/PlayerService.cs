using AutoMapper;
using AutoMapper.QueryableExtensions;
using E_SportManager.Data;
using E_SportManager.Service.Data.Players;
using Microsoft.EntityFrameworkCore;

using static E_SportManager.Common.GlobalConstants.Player;

namespace E_SportManager.Service.Data
{
    public class PlayerService : IPlayerService
    {
        private readonly ESportDbContext data;
        private readonly IMapper mapper;

        public PlayerService(ESportDbContext data,IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper;
        }

        public async Task CreatePlayerAsync(
            string name,
            string imageUrl, 
            int yearsOfExperience, 
            string role, 
            string division, 
            string description)
        {
            var player = new Player
            {
                Name = name,
                ImageUrl = imageUrl,
                YearsOfExperience = yearsOfExperience,
                Role =role,
                Division = division,
                Description = description
            };

            await data.Players.AddAsync(player);
            await data.SaveChangesAsync();
        }

        public async Task DeletePlayerAsync(int id)
        {
            var player = await GetByIdAsync(id);

            player.IsDeleted = true;
            player.DeletedOn= DateTime.UtcNow.ToLocalTime().ToString("dd/MM/yyyy H:mm");

            await data.SaveChangesAsync();
        }

        public async Task EditPlayerAsync(int playerId, string name, string imageUrl, int yearsOfExperience,string role, string division, string description)
        {
            var player = await GetByIdAsync(playerId);

            player.Name = name;
            player.ImageUrl = imageUrl;
            player.Role = role;
            player.YearsOfExperience= yearsOfExperience;
            player.Division = division;
            player.Description = description;

            await data.SaveChangesAsync();
        }

        public async Task<IEnumerable<TModel>> GetAllPlayersAsync<TModel>(int skip=0,bool publicOnly = true)
            =>await data.Players
                 .AsNoTracking()
                 .OrderByDescending(p=>p.CreatedOn)
                 .Where(p=>!p.IsDeleted)
                 .Skip(skip).Take(PlayersPerPage)
                 .ProjectTo<TModel>(mapper.ConfigurationProvider)
                 .ToListAsync();

        public async Task<TModel> GetByIdAsync<TModel>(int id)
            => await data.Players
                .AsNoTracking()
                .Where(p=> p.Id == id && !p.IsDeleted)
                .ProjectTo<TModel>(mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

        public async Task<int> GetTotalPlayersCountAsync()
            =>await data.Players.Where(p => !p.IsDeleted).CountAsync();

        public async Task<bool> IsExistingAsync(string name)
            => await data.Players.AnyAsync(p => p.Name == name && !p.IsDeleted);

        public async Task<Player> GetByIdAsync(int id)
           => await data.Players.FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted);

    }
}