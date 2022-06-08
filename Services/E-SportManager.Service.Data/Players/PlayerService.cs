using E_SportManager.Data;
using E_SportManager.Data.enums;
using E_SportManager.Service.Data.Players;
using Microsoft.EntityFrameworkCore;

namespace E_SportManager.Service.Data
{
    public class PlayerService : IPlayerService
    {
        private readonly ESportDbContext data;

        public PlayerService(ESportDbContext data)
        {
            this.data = data;
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

        public async Task<bool> IsExistingAsync(string name)
            => await data.Players.AnyAsync(c => c.Name == name);
    }
}