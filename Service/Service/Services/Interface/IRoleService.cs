namespace Service.Services.Interface
{
    public interface IRoleService
    {
        Task<string> GetRoleByUserId(int userId);
    }
}
