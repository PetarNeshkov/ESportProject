using AutoMapper;
using AutoMapper.QueryableExtensions;
using E_SportManager.Data;
using E_SportManager.Data.enums;

using Microsoft.EntityFrameworkCore;

using static E_SportManager.Common.GlobalConstants.Team;

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

        public async Task<TeamImageServiceModel> GetImageTeamAsync(string title)
        {
            var imageUrl = await data.Teams
                .Where(t => t.Title == title)
                .Select(t => t.ImageUrl)
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

        public async Task<bool> IsExistingAsync(string title)
          => await data.Teams.AnyAsync(p => p.Title == title && !p.IsDeleted);

        public async Task<bool> IsExistingAsync(string title, int id)
          => await data.Teams.AnyAsync(p => p.Title == title && p.Id != id && !p.IsDeleted);

        public async Task<IEnumerable<TModel>> GetAllTeamsAsync<TModel>(int skip=0)
            => await data.Teams
                 .AsNoTracking()
                 .OrderByDescending(p => p.CreatedOn)
                 .Where(p => !p.IsDeleted)
                 .Skip(skip).Take(TeamsPerPage)
                 .ProjectTo<TModel>(mapper.ConfigurationProvider)
                 .ToListAsync();

        public async Task<IEnumerable<TModel>> GetAllTeamsAsync<TModel>()
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

        public async Task<bool> GetWiningTeamAsync(string firstTeamName, string secondTeamName)
        {
            var myTeam = await GetTeamByNameAsync(firstTeamName);
            var enemyTeam= await GetTeamByNameAsync(secondTeamName);

            var myTeamPoints = await CalculateTeamPoints(myTeam);
            var enemyTeamPoints = await CalculateTeamPoints(enemyTeam);

            if (myTeamPoints> enemyTeamPoints)
            {
                myTeam.Rating++;
                myTeam.WonGames++;
                enemyTeam.LostGames++;

                await data.SaveChangesAsync();

                return true;
            }

            enemyTeam.Rating++;
            enemyTeam.WonGames++;
            myTeam.LostGames++;

            await data.SaveChangesAsync();

            return false;
        }

        private async Task<int> CalculateTeamPoints(Team team)
        {
            int totalPoints=0;
            var players=new List<Player>();

            var midLaner=await GetPlayerAsync(team.MidLanerId);
            var topLaner=await GetPlayerAsync(team.TopLanerId);
            var jungleLaner= await GetPlayerAsync(team.JungleLanerId);
            var bottomLaner=await GetPlayerAsync(team.BottomLanerId);
            var supportLaner=await GetPlayerAsync(team.SupportLanerId);

            players.Add(midLaner);
            players.Add(topLaner);
            players.Add(jungleLaner);
            players.Add(bottomLaner);
            players.Add(supportLaner);

            foreach (var player in players)
            {
                int currPoints = CalculateDivisionPointsAsync(player.Division);
                totalPoints += CalculateYearsOfExperienceMultiPlier(player.YearsOfExperience,currPoints);
            }

            return totalPoints;
        }

        private int CalculateYearsOfExperienceMultiPlier(int yearsOfExperience,int totalPoints)
        {
            if (yearsOfExperience >= 2 && yearsOfExperience < 5) 
                totalPoints=(int)Math.Round(totalPoints*1.2);
            else if(yearsOfExperience >= 5 && yearsOfExperience < 8)
                totalPoints = (int)Math.Round(totalPoints * 1.4);
            else if(yearsOfExperience >= 8 && yearsOfExperience < 10)
                totalPoints = (int)Math.Round(totalPoints * 1.5);
            else if(yearsOfExperience>=10)
                totalPoints = (int)Math.Round(totalPoints * 1.8);

            return totalPoints;
        }

        private int CalculateDivisionPointsAsync(string division)
        {
            int divisionPoints = 0;
            if (division == "Iron") divisionPoints = 2;
            else if (division == "Bronze") divisionPoints = 3;
            else if (division == "Silver") divisionPoints = 4;
            else if (division == "Gold") divisionPoints = 8;
            else if (division == "Platinium") divisionPoints = 10;
            else if (division == "Diamond") divisionPoints = 20;
            else if (division == "Master") divisionPoints = 40;
            else if (division == "GrandMaster") divisionPoints = 60;
            else if (division == "Challenger") divisionPoints = 80;

            return divisionPoints;
        }

        private async Task<Team> GetTeamByNameAsync(string firstTeamName)
           =>await data.Teams.Where(t=>t.Title==firstTeamName).FirstAsync();

        private async Task<int> GetPlayerIdAsync(string name)
            => await data.Players
                .Where(p=>p.Name==name)
                .Select(x=>x.Id)
                .FirstAsync();

        private async Task<Player> GetPlayerAsync(int id)
           => await data.Players
               .Where(p => p.Id == id)
               .FirstAsync();
    }
}
