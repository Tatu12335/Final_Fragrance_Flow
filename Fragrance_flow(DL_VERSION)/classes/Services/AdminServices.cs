using Dapper;
using Fragrance_flow_DL_VERSION_.interfaces;
using Fragrance_flow_DL_VERSION_.models;
using Microsoft.Data.SqlClient;

namespace Fragrance_flow_DL_VERSION_.classes.Services
{
    public class AdminServices : IAdminServices
    {
        private readonly string _connectionString;
        private readonly ILoggger _loggger;

        public AdminServices(string connectionString, ILoggger loggger)
        {
            _connectionString = connectionString;
            _loggger = loggger;
        }
        public async Task<IEnumerable<Users>> GetAllUsers()
        {

            string sqlQuery = "SELECT * FROM users";

            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var response = (await conn.QueryAsync<Users>(sqlQuery));

                    return response;
                }
            }
            catch (Exception ex)
            {
                _loggger.Log($" [error] : {ex.Message}. {ex.StackTrace}");
                throw new Exception(" An error occured : " + ex.Message);
                
            }
        }

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
