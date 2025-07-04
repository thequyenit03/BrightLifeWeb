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
        public async Task<List<string>> GetRoleByUserId(int userId)
        {
            if(string.IsNullOrEmpty(userId.ToString()) || userId <= 0)
            {
                throw new ArgumentException("UserId cannot be null or empty.", nameof(userId));
            }
            List<string> roles = await _context.Roles.Include(r => r.UserRoles)
                .Where(r => r.UserRoles.Any(ur => ur.UserId == userId))
                .Select(r => r.Name).ToListAsync();
            return roles ?? new List<string>();

        }


    }
}
