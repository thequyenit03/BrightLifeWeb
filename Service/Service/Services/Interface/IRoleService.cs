namespace Service.Services.Interface
{
    public interface IRoleService
    {
        Task<List<string>> GetRoleByUserId(int userId);
    }
}
