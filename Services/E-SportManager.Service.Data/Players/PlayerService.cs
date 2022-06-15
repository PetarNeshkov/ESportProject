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

        public async Task<int> CreatePlayerAsync(
            string name,
            string imageUrl, 
            int yearsOfExperience, 
            string role, 
            string division, 
            string description,
            string userId)
        {
            var player = new Player
            {
                Name = name,
                ImageUrl = imageUrl,
                YearsOfExperience = yearsOfExperience,
                Role =role,
                Division = division,
                Description = description,
                AuthorId=userId,
                IsPublic = false
            };

            await data.Players.AddAsync(player);
            await data.SaveChangesAsync();

            return player.Id;
        }

        public async Task DeletePlayerAsync(int id)
        {
            var player = await GetByIdAsync(id);

            player.IsDeleted = true;
            player.IsPublic = false;
            player.DeletedOn= DateTime.UtcNow.ToLocalTime().ToString("dd/MM/yyyy H:mm");

            await data.SaveChangesAsync();
        }

        public async Task EditPlayerAsync(int playerId, string name, string imageUrl, int yearsOfExperience,string role, string division, string description,bool isPublic=false)
        {
            var player = await GetByIdAsync(playerId);

            player.Name = name;
            player.ImageUrl = imageUrl;
            player.Role = role;
            player.YearsOfExperience= yearsOfExperience;
            player.Division = division;
            player.Description = description;
            player.IsPublic = isPublic;

            await data.SaveChangesAsync();
        }
        public void ChangeVisibility(int id)
        {
            var player =  data.Players.First(x=>x.Id==id);

            player.IsPublic= !player.IsPublic;

            data.SaveChanges();
        }

        public async Task<IEnumerable<TModel>> GetAllPlayersAsync<TModel>(int skip=0,bool publicOnly = true)
            =>await data.Players
                 .AsNoTracking()
                 .OrderByDescending(p=>p.CreatedOn)
                 .Where(p=>!p.IsDeleted && (!publicOnly || p.IsPublic))
                 .Skip(skip).Take(PlayersPerPage)
                 .ProjectTo<TModel>(mapper.ConfigurationProvider)
                 .ToListAsync();

        public async Task<IEnumerable<TModel>> GetAllPlayersAsync<TModel>(bool publicOnly = true)
            => await data.Players
                 .AsNoTracking()
                 .OrderByDescending(p => p.CreatedOn)
                 .Where(p => !p.IsDeleted && (!publicOnly || p.IsPublic))
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

        public async Task<bool> IsExistingAsync(string name,int id)
            => await data.Players.AnyAsync(p => p.Name == name && p.Id!=id &&!p.IsDeleted);

        public async Task<Player> GetByIdAsync(int id)
           => await data.Players.FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted);

        public async Task<string> GetPlayerAuthorIdAsync(int id)
             => await data.Players
                 .AsNoTracking()
                 .Where(p => p.Id == id && !p.IsDeleted)
                 .Select(p => p.AuthorId)
                 .FirstOrDefaultAsync();
    }
}