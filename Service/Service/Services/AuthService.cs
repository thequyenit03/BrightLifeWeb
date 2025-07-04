using Service.DTOs.Auth;
using Service.DTOs.User;
using Service.Heplers;
using Service.Models;
using Service.Services.Interface;

namespace Service.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly IJwtService _jwtService;

        public AuthService(ApplicationDbContext context,IJwtService jwtService)
        {
            _context = context;
            _jwtService = jwtService;

        }
        public async Task<AuthResponse> LoginAsync(LoginDto request)
        {
            if(request == null || request.Email==null || request.Password==null)
            {
                throw new ArgumentNullException(nameof(request), "Request cannot be null.");
            }
            //validate the request 
            var user = _context.Users.FirstOrDefault(u =>u.Email == request.Email);
            if (user==null)
            {
                throw new ArgumentNullException("Invalid email");
            }
            var isValidPassword = HelperHashingPassword.VerifyPassword(request.Password, user.PasswordHash);
            if (!isValidPassword)
            {
                throw new ArgumentException("Invalid password.", nameof(request.Password));
            }
            // Generate JWT token
            UserDto userDto = new UserDto
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                UserName = user.UserName,
                IsActive = true
            };
            string jwtToken =await _jwtService.GenerateTokenAsync(userDto);
            AuthResponse authResponse = new AuthResponse
            {
                Token = jwtToken,
                User = userDto
            };
            return authResponse;

        }

        public Task<AuthResponse> RefreshTokenAsync(string token)
        {
            throw new NotImplementedException();
        }

        public async Task<AuthResponse> RegisterAsync(RegisterDto request)
        {
            // Validate the request
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request), "Request cannot be null.");
            }
            if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password) || string.IsNullOrEmpty(request.FullName))
            {
                throw new ArgumentException("Email, Password, and FullName cannot be null or empty.", nameof(request));
            }
            var exitinguser = _context.Users.FirstOrDefault(u => u.Email == request.Email);
            if(exitinguser != null)
            {
                throw new InvalidOperationException("User with this email already exists.");
            }
            // Hash the password
            string hashedPassword = HelperHashingPassword.HashPassword(request.Password);
            User user = new User
            {
                FullName = request.FullName,
                Email = request.Email,
                PasswordHash = hashedPassword,
                UserName = request.UserName,
            };
            _context.Users.Add(user);   
            await _context.SaveChangesAsync();

            UserDto userDto = new UserDto
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                UserName = user.UserName,
                IsActive = true
            };
            // Generate JWT token
            string jwtToken = await _jwtService.GenerateTokenAsync(userDto);
            AuthResponse authResponse = new AuthResponse
            {
                Token = jwtToken,
                User = userDto
            };
            
            return authResponse;
        }
    }
}
