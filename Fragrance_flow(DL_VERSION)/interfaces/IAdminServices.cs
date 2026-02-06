using Fragrance_flow_DL_VERSION_.models;

namespace Fragrance_flow_DL_VERSION_.interfaces
{
    public interface IAdminServices
    {
        public Task<IEnumerable<Users>> GetAllUsers();
        public Task BanUserById(int id);
        public Task UnbanUserById(int id);
        public Task PromoteUserById(int id);
        public Task DemoteUserById(int id);
        public Task RemoveUserById(int id);
    }
}
