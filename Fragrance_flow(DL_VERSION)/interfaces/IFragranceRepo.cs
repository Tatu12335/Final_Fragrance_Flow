using Fragrance_flow_DL_VERSION_.models;

namespace Fragrance_flow_DL_VERSION_.interfaces
{
    public interface IFragranceRepo
    {
        Task<IEnumerable<Fragrance>> GetAllAsync();
        Task<Users> CheckIfUserExists(string username);
        Task<Users> GetUserId(string username);
        public Task<UserSession?> Login(string username, string password);
        public Task<UserSession> CreateNewUserAsync(string username, string email, string password);
        public Task AddFragrance(string username, Fragrance frag);
        public Task<IEnumerable<Fragrance>> GetFragrancesByUserId(string username, int id);
        // public Task<UserSession> GetUserFromDb(string username);
        public Task<int> VerifyEmail(string email);
        public Task<Users> GetAdminStatus(string username);
        public Task<Users> GetBannedStatus(string username);
        public Task RemoveFragranceById(int userId, int id);

    }

}
