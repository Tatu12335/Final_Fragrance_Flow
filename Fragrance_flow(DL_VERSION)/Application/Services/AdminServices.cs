using Dapper;
using Fragrance_flow_DL_VERSION_.Application.interfaces;
using Fragrance_flow_DL_VERSION_.Domain.Entities;
using Microsoft.Data.SqlClient;

namespace Fragrance_flow_DL_VERSION_.Application.Services
{
    public class AdminServices : IAdminServices
    {
        private readonly string _connectionString;
        private readonly ILoggger _loggger;
        // Needs the connection-string[Which is an environment variable] and the logger 
        public AdminServices(string connectionString, ILoggger loggger)
        {
            _connectionString = connectionString;
            _loggger = loggger;
        }
        // Gets all users 
        public async Task<IEnumerable<Users>> GetAllUsers()
        {

            string sqlQuery = "SELECT * FROM users";

            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var response = await conn.QueryAsync<Users>(sqlQuery);

                    return response;
                }
            }
            catch (Exception ex)
            {
                _loggger.Log($" [error] : {ex.Message}. {ex.StackTrace}");
                throw new Exception(" An error occured : " + ex.Message);

            }
        }
        // Ban User by id 
        public async Task BanUserById(int id)
        {
            string sqlQuery = "UPDATE users SET isBanned = 1 WHERE id = @Id";

            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    await conn.ExecuteAsync(sqlQuery, new { Id = id });
                }
            }
            catch (Exception ex)
            {
                _loggger.Log($" [error] : {ex.Message}. {ex.StackTrace}");
                throw new Exception(" An error occured : " + ex.Message);

            }
        }
        // And unban user by id
        public async Task UnbanUserById(int id)
        {
            string sqlQuery = "UPDATE users SET isBanned = 0 WHERE id = @Id";

            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    await conn.ExecuteAsync(sqlQuery, new { Id = id });
                }
            }
            catch (Exception ex)
            {
                _loggger.Log($" [error] : {ex.Message}. {ex.StackTrace}");
                throw new Exception(" An error occured : " + ex.Message);
            }
        }
        // Promote user to admin.
        // NOTE that i at this time have only 2 roles user and admin, i plan on making this better once the wpf frontend is good enough.
        public async Task PromoteUserById(int id)
        {
            string sqlQuery = "UPDATE users SET isAdmin = 1 WHERE id = @id";

            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    await conn.ExecuteAsync(sqlQuery, new { Id = id });
                }
            }
            catch (Exception ex)
            {
                _loggger.Log($" [error] : {ex.Message}. {ex.StackTrace}");
                throw new Exception(" An error occured : " + ex.Message);
            }
        }
        // Demote user to user
        public async Task DemoteUserById(int id)
        {
            string sqlQuery = "UPDATE users SET isAdmin = 0 WHERE id = @id";

            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    await conn.ExecuteAsync(sqlQuery, new { Id = id });
                }
            }
            catch (Exception ex)
            {
                _loggger.Log($" [error] : {ex.Message}. {ex.StackTrace}");
                throw new Exception(" An error occured : " + ex.Message);
            }
        }
        // Remove user from the db
        public async Task RemoveUserById(int id)
        {
            string sqlQuery = "DELETE from users WHERE id = @Id ";

            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    await conn.ExecuteAsync(sqlQuery, new { Id = id });
                }
            }
            catch (Exception ex)
            {
                _loggger.Log($" [error] : {ex.Message}. {ex.StackTrace}");
                throw new Exception(" An error occured : " + ex.Message);
            }
        }
    }
}
