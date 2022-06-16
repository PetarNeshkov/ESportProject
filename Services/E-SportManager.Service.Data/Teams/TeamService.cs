using AutoMapper;
using AutoMapper.QueryableExtensions;
using E_SportManager.Data;
using E_SportManager.Data.enums;

using Microsoft.EntityFrameworkCore;

namespace E_SportManager.Service.Data.Teams
{
    public class TeamService : ITeamService
    {
        private readonly ESportDbContext data;
        private readonly IMapper mapper;

        public TeamService(ESportDbContext data,IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<PlayerListServiceModel>> GetRoleAsync(Role role)
        {
            var queryablePlayers =  data.Players.AsNoTracking().Where(p=>!p.IsDeleted);

            queryablePlayers =  role switch
            {
                Role.Mid => queryablePlayers.Where(p=>p.Role==role.ToString()),
                Role.Top => queryablePlayers.Where(p => p.Role == role.ToString()),
                Role.Bottom => queryablePlayers.Where(p => p.Role == role.ToString()),
                Role.Jungle => queryablePlayers.Where(p => p.Role == role.ToString()),
                Role.Support or _ => queryablePlayers.Where(p => p.Role == role.ToString())
            };

            return await queryablePlayers
                    .ProjectTo<PlayerListServiceModel>(mapper.ConfigurationProvider)
                    .ToListAsync();
        }

        public async Task<TeamImageServiceModel> GetImageAsync(string name)
        {
            var imageUrl= await data.Players
                .Where(p => p.Name == name)
                .Select(p => p.ImageUrl)
                .FirstAsync();

            return new TeamImageServiceModel
            {
                ImageUrl = imageUrl
            };
        }

        public async Task CreateTeamAsync(
            string title, 
            string imageUrl, 
            string midLaner,
            string topLaner, 
            string jungleLaner, 
            string bottomLaner, 
            string supportLaner, 
            string authorId)
        {
            var team = new Team
            {
                Title=title,
                ImageUrl=imageUrl,
                MidLanerId= await GetPlayerIdAsync(midLaner),
                TopLanerId= await GetPlayerIdAsync(topLaner),
                BottomLanerId= await GetPlayerIdAsync(bottomLaner),
                SupportLanerId= await GetPlayerIdAsync(supportLaner),
                JungleLanerId= await GetPlayerIdAsync(jungleLaner),
                AuthorId= authorId
            };

            await data.Teams.AddAsync(team);
            await data.SaveChangesAsync();
        }

        private async Task<int> GetPlayerIdAsync(string name)
            => await data.Players
                .Where(p=>p.Name==name)
                .Select(x=>x.Id)
                .FirstAsync();

        public async Task<bool> IsExistingAsync(string title)
          => await data.Teams.AnyAsync(p => p.Title == title && !p.IsDeleted);

        public async Task<bool> IsExistingAsync(string title, int id)
          => await data.Teams.AnyAsync(p => p.Title == title && p.Id != id && !p.IsDeleted);

        public async Task<IEnumerable<TModel>> GetAllTeamsAsync<TModel>(int skip=0)
            => await data.Teams
                 .AsNoTracking()
                 .OrderByDescending(p => p.CreatedOn)
                 .Where(p => !p.IsDeleted)
                 .ProjectTo<TModel>(mapper.ConfigurationProvider)
                 .ToListAsync();

        public async Task<int> GetTotalTeamsCountAsync()
          => await data.Teams.Where(t => !t.IsDeleted).CountAsync();

        public async Task DeleteTeamAsync(int id)
        {
            var team= await GetByIdAsync(id);

            team.IsDeleted = true;
            team.DeletedOn = DateTime.UtcNow.ToLocalTime().ToString("dd/MM/yyyy H:mm");

            await data.SaveChangesAsync();
        }

        public async Task<TModel> GetByIdAsync<TModel>(int id)
          => await data.Teams
              .AsNoTracking()
              .Where(p => p.Id == id && !p.IsDeleted)
              .ProjectTo<TModel>(mapper.ConfigurationProvider)
              .FirstOrDefaultAsync();

        public async Task<Team> GetByIdAsync(int id)
            => await data.Teams
                 .FirstOrDefaultAsync(t => t.Id == id && !t.IsDeleted);

        public async Task<string> GetTeamAuthorIdAsync(int id)
            => await data.Teams
                 .AsNoTracking()
                 .Where(t => t.Id == id && !t.IsDeleted)
                 .Select(t => t.AuthorId)
                 .FirstOrDefaultAsync();

        public async Task EditTeamAsync(int id, string title, string imageUrl, string midLaner, string topLaner, string jungleLaner, string bottomLaner, string supportLaner)
        {
            var team= await GetByIdAsync(id);
            
            team.Title = title;
            team.ImageUrl = imageUrl;
            team.MidLanerId = await GetPlayerIdAsync(midLaner);
            team.TopLanerId = await GetPlayerIdAsync(topLaner);
            team.JungleLanerId = await GetPlayerIdAsync(jungleLaner);
            team.BottomLanerId = await GetPlayerIdAsync(bottomLaner);
            team.SupportLanerId = await GetPlayerIdAsync(supportLaner);

            await data.SaveChangesAsync();
        }
    }
}
