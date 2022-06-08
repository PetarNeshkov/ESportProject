using AutoMapper;
using AutoMapper.QueryableExtensions;
using E_SportManager.Data;
using E_SportManager.Data.enums;
using E_SportManager.Service.Data.Players;
using Microsoft.EntityFrameworkCore;

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
                Role = Enum.Parse<Role>(role),
                Division = Enum.Parse<Division>(division),
                Description = description
            };

            await data.Players.AddAsync(player);
            await data.SaveChangesAsync();
        }

        public async Task<IEnumerable<TModel>> GetAllPlayersAsync<TModel>()
            =>await data.Players
                 .AsNoTracking()
                 .OrderByDescending(p=>p.CreatedOn)
                 .Where(p=>!p.IsDeleted)
                 .ProjectTo<TModel>(mapper.ConfigurationProvider)
                 .ToListAsync();

        public async Task<bool> IsExistingAsync(string name)
            => await data.Players.AnyAsync(p => p.Name == name && !p.IsDeleted);
    }
}