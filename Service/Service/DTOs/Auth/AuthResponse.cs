using Service.DTOs.User;

namespace Service.DTOs.Auth
{
    public class AuthResponse
    {
        public string? Token { get; set; }
        public Boolean status { get; set; } = true;
        public string? Message { get; set; } = "Success";
    }
}
