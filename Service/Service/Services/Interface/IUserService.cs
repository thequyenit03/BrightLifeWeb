using Service.Models;

namespace Service.Services.Interface
{
    public interface IUserService
    {
        Task<User?> CreateUserAsync(User user);
        Task<User?> GetUserFromEmailAsync(string email);
    }
}
