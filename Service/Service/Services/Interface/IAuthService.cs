using Service.DTOs.Auth;

namespace Service.Services.Interface
{
    public interface IAuthService
    {
        Task<AuthResponse> LoginAsync(LoginDto request);
        Task<AuthResponse> RegisterAsync(RegisterDto request);
        Task<AuthResponse> RefreshTokenAsync(string token);
    }
}
