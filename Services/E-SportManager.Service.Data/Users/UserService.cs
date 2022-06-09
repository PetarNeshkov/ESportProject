using E_SportManager.Data;
using Microsoft.EntityFrameworkCore;

namespace E_SportManager.Service.Data.Users
{
    public class UserService : IUserService
    {
        private readonly ESportDbContext data;

        public UserService(ESportDbContext data)
        {
            this.data = data;
        }

        public async Task<bool> IsUsernameUsedAsync(string username)
            => await data.Users.AnyAsync(u => u.UserName == username);
        public async Task<bool> IsEmailUsedAsync(string email)
            => await data.Users.AnyAsync(u => u.Email == email);
    }
}
