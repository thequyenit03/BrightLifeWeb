using Service.DTOs.Auth;
using Service.DTOs.User;

namespace Service.Services.Interface
{
    public interface IJwtService
    {
        Task<string> GenerateTokenAsync(UserDto user);
        Task<AuthResponse> ValidateTokenAsync(string token);
    }
}
