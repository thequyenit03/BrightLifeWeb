using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.DTOs.Auth;
using Service.Services;
using Service.Services.Interface;

namespace Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult> Login(LoginDto loginDto)
        {
            AuthResponse authResponse = await _authService.LoginAsync(loginDto);
            if (authResponse == null)
            {
                return BadRequest("Invalid login attempt.");
            }
            return Ok(authResponse);
        }
        [HttpPost]
        [Route("register")]
        public async Task<ActionResult> Register(RegisterDto registerDto)
        {
            if (registerDto == null)
            {
                return BadRequest("Register request cannot be null.");
            }
            AuthResponse authResponse = await _authService.RegisterAsync(registerDto);
            if (authResponse == null)
            {
                return BadRequest("Registration failed.");
            }
            return Ok(authResponse);
        }
        [HttpGet]
        [Authorize]
        [Route("check-authentication")]
        public ActionResult CheckAuthentication()
        {
            return Ok(new { Message = "User is authenticated." });
        }
    }
}
