using Microsoft.EntityFrameworkCore;
using Service.Models;
using Service.Services.Interface;

namespace Service.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context )
        {
            _context = context;
        }
        public async Task<User?> CreateUserAsync(User user)
        {
            if(user == null)
            {
                return null;
            }
            var exitUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == user.Email);
            if (exitUser != null)
            {
                return null;
            }
            _context.Users.Add(user);
            Role role = await _context.Roles.FirstOrDefaultAsync(r => r.Name == "User");
            if(role == null)
            {
                role = new Role { 
                    Name = "User",
                    Description = "This role is user" };
                _context.Roles.Add(role);
            }
            await _context.SaveChangesAsync();
            UserRole userRole = new UserRole
            {
                UserId = user.Id,
                RoleId = role.Id // Assuming 1 is the default role ID for a new user
            };
            _context.UserRoles.Add(userRole);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return user;
            }
            else
            {
                throw new InvalidOperationException("Failed to create user. Please try again.");
            }
        }

        public Task<User?> GetUserFromEmailAsync(string email)
        {
            if(string.IsNullOrEmpty(email))
            {
                return null;
            }
            return _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
