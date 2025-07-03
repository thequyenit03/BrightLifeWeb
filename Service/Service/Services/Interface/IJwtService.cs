using Service.DTOs.User;

namespace Service.Services.Interface
{
    public interface IJwtService
    {
        string GenerateToken(UserDto user);
        Task<UserDto?> ValidateTokenAsync(string token);
    }
}
