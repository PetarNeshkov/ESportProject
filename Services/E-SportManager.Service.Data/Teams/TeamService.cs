using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using E_SportManager.Data;
using E_SportManager.Data.enums;
using E_SportManager.Models.Teams;
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

        public async Task<IEnumerable<Player>> GetRoleAsync(Role role)
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

            return await queryablePlayers.ToListAsync();
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

    }
}
