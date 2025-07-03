using Microsoft.IdentityModel.Tokens;
using Service.DTOs.User;
using Service.Services.Interface;
using System.Security.Claims;
using System.Text;

namespace Service.Services
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;
        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;   
        }
        public string GenerateToken(UserDto user)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings["SecretKey"]?? throw new ArgumentNullException("SecretKey is not configured in JwtSettings.");
            var issuser = jwtSettings["Issuer"] ?? throw new ArgumentNullException("Issuer is not configured in JwtSettings.");
            var audience = jwtSettings["Audience"] ?? throw new ArgumentNullException("Audience is not configured in JwtSettings.");
            var expiration = int.Parse(jwtSettings["Expiration"] ?? throw new ArgumentNullException("Expiration is not configured in JwtSettings."));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);

            // Create the token
            var token = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.FullName),
                new Claim(ClaimTypes.Email, user.Email),

            };

            //add role as claim if it exists    
            foreach(var role in user.r)
        }

        public Task<UserDto?> ValidateTokenAsync(string token)
        {
            throw new NotImplementedException();
        }
    }
}
