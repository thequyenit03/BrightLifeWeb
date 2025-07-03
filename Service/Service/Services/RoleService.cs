using Microsoft.EntityFrameworkCore;
using Service.Models;
using Service.Services.Interface;

namespace Service.Services
{
    public class RoleService : IRoleService
    {
        private readonly ApplicationDbContext _dbContext;

        public RoleService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Task<List<string>> GetRoleByUserId(int userId)
        {
            if (string.IsNullOrEmpty(userId+""){
                throw new ArgumentException("User ID cannot be null or empty.", nameof(userId));

            }
            return _dbContext.UserRoles
                .Where(ur => ur.UserId == userId)
                .Include(ur => ur.Roles)
                .Select(ur => ur.Roles.Title)
                .ToListAsync();

        }
    }
}
