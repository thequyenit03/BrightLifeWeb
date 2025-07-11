using Microsoft.EntityFrameworkCore;
using Service.Models;
using Service.Services.Interface;

namespace Service.Services
{
    public class RoleService : IRoleService
    {
        private readonly ApplicationDbContext _context;

        public RoleService(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<string> GetRoleByUserId(int userId)
        {
            if (userId <= 0)
            {
                throw new ArgumentException("UserId must be greater than zero.", nameof(userId));
            }

            UserRole? userRole = await _context.UserRoles
                .Where(ur => ur.UserId == userId)
                .Include(ur => ur.Roles)
                .FirstOrDefaultAsync(ur => ur.Roles != null && ur.Roles.Name != null);

            if (userRole?.Roles == null)
            {
                throw new InvalidOperationException("Role not found for the given user.");
            }

            return userRole.Roles.Name;
        }
    }
}
