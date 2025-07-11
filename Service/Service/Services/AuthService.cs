using Microsoft.EntityFrameworkCore;
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
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;

        public AuthService(ApplicationDbContext context,IJwtService jwtService,IUserService userService,IRoleService roleService)
        {
            _context = context;
            _jwtService = jwtService;
            _userService = userService;
            _roleService = roleService;

        }
        public async Task<AuthResponse> LoginAsync(LoginDto request)
        {
            if(request == null || request.Email==null || request.Password==null)
            {
                return new AuthResponse()
                {
                    status = false,
                    Token = null,
                    Message = "Invalid request data."
                };
            }
            User user = await _userService.GetUserFromEmailAsync(request.Email);
            if(user == null)
            {
                return new AuthResponse()
                {
                    status = false,
                    Token = null,
                    Message = "Invalid email or password."
                };
            }
            var isValidPassword = HelperHashingPassword.VerifyPassword(request.Password, user.PasswordHash);
            if (!isValidPassword)
            {
                return new AuthResponse()
                {
                    status = false,
                    Token = null,
                    Message = "Invalid email or password."
                };
            }

            // Generate JWT token
            UserDto userDto = new UserDto
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                UserName = user.UserName,
                IsActive = true,
                Role = await _roleService.GetRoleByUserId(user.Id)
            };
            string jwtToken =await _jwtService.GenerateTokenAsync(userDto);
            AuthResponse authResponse = new AuthResponse
            {
                Token = jwtToken,
                
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
                return new AuthResponse
                {
                    status = false,
                    Message = "Invalid request data.",
                    Token = null,
                };
            }
            if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password) || string.IsNullOrEmpty(request.FullName))
            {
                return new AuthResponse
                {
                    status = false,
                    Token = null,
                    Message = "Email, password, and full name are required."
                };
            }
            User? user =await _userService.CreateUserAsync(new User
            {
                FullName = request.FullName,
                Email = request.Email,
                UserName = request.UserName,
                PasswordHash = HelperHashingPassword.HashPassword(request.Password),

            });
            if (user == null)
            {
                return new AuthResponse
                {
                    status = false,
                    Message = "User already exists or failed to create user.",
                    Token = null,
                };
            }


            UserDto userDto = new UserDto
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                UserName = user.UserName,
                IsActive = true,
                Role = await _roleService.GetRoleByUserId(user.Id)

            };
            // Generate JWT token
            string jwtToken = await _jwtService.GenerateTokenAsync(userDto);
            AuthResponse authResponse = new AuthResponse
            {
                Token = jwtToken,
            };
            
            return authResponse;
        }
    }
}
