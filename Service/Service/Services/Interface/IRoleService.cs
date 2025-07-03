namespace Service.Services.Interface
{
    public interface IRoleService
    {
        Task<List<string>> GetRoleByUserId(string userId);
    }
}
