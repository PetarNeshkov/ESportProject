﻿using E_SportManager.Data.enums;


namespace E_SportManager.Service.Data.Teams
{
    public interface ITeamService
    {
        Task<IEnumerable<PlayerListServiceModel>> GetRoleAsync(Role role);

        Task<TeamImageServiceModel> GetImageAsync(string name);

        Task CreateTeamAsync(string title, string imageUrl,string midLaner,
            string topLaner,string jungleLaner,string bottomLaner,
            string supportLaner,string authorId);

        Task<bool> IsExistingAsync(string title);

        Task<IEnumerable<TModel>> GetAllTeamsAsync<TModel>(int skip=0);

        Task<int> GetTotalTeamsCountAsync();
    }
}
