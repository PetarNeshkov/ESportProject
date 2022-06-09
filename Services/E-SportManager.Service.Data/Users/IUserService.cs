

namespace E_SportManager.Service.Data.Users
{
    public interface IUserService
    {
        Task<bool> IsUsernameUsedAsync(string username);

        Task<bool> IsEmailUsedAsync(string email);
    }
}
