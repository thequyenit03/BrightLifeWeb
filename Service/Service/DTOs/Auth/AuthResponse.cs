using Service.DTOs.User;

namespace Service.DTOs.Auth
{
    public class AuthResponse
    {
        public string Token { get; set; }
        public UserDto User { get; set; }
    }
}
