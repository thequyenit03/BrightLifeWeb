using Microsoft.IdentityModel.Tokens;
using Service.DTOs.User;
using Service.Models;
using Service.Services.Interface;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Service.DTOs.Auth;
namespace Service.Services
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;
        private readonly IRoleService _roleService;

        public JwtService(IConfiguration configuration,IRoleService roleService)
        {
            _configuration = configuration;
            _roleService = roleService;
        }
        public async Task<string> GenerateTokenAsync(UserDto user)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings["SecretKey"]?? throw new ArgumentNullException("SecretKey is not configured in JwtSettings.");
            var issuser = jwtSettings["Issuer"] ?? throw new ArgumentNullException("Issuer is not configured in JwtSettings.");
            var audience = jwtSettings["Audience"] ?? throw new ArgumentNullException("Audience is not configured in JwtSettings.");
            var expiration = int.Parse(jwtSettings["Expiration"] ?? throw new ArgumentNullException("Expiration is not configured in JwtSettings."));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);

            // Create the token
            var calms = new List<Claim>
            {
                new Claim("id", user.Id.ToString()),
                new Claim("userName", user.UserName),
                new Claim("email", user.Email),

            };

            //add role as claim if it exists    
            string roles = await _roleService.GetRoleByUserId(user.Id);
            if (roles != null && !string.IsNullOrEmpty(roles))
            {
                calms.Add(new Claim("role", roles));
            }
            //Generate the token
            var token = new JwtSecurityToken(
                issuer: issuser,
                audience: audience,
                claims: calms,
                expires: DateTime.Now.AddMinutes(expiration),
                signingCredentials: creds
                );
            string jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
            return jwtToken;
        }

        public Task<AuthResponse> ValidateTokenAsync(string token)
        {
            try
            {
                var jwtSettings = _configuration.GetSection("JwtSettings");
                var secretKey = jwtSettings["SecretKey"] ?? throw new ArgumentNullException("SecretKey is not configured in JwtSettings.");
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(secretKey);

                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = jwtSettings["Issuer"] ?? throw new ArgumentNullException("Issuer is not configured in JwtSettings."),
                    ValidateAudience = true,
                    ValidAudience = jwtSettings["Audience"] ?? throw new ArgumentNullException("Audience is not configured in JwtSettings."),
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);
                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                UserDto user = new UserDto
                {
                    Id = int.Parse(userId),
                    FullName = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value,
                    Email = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value,
                    Role = jwtToken.Claims.FirstOrDefault(c => c.Type== ClaimTypes.Role)?.Value,
                    IsActive = true
                };
                AuthResponse authResponse = new AuthResponse
                {
                    Token = token,
                };
                return Task.FromResult( authResponse);


            }catch (Exception ex)
            {
                Console.WriteLine($"--> Error validating token: {ex.Message}");
                throw new UnauthorizedAccessException("Invalid token.", ex);
            }
        }

       
    }
}
