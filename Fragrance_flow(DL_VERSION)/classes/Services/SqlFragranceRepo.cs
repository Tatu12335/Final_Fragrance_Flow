using Dapper;
using Fragrance_flow_DL_VERSION_.interfaces;
using Fragrance_flow_DL_VERSION_.models;
using Microsoft.Data.SqlClient;

namespace Fragrance_flow_DL_VERSION_.classes.Sql
{
    public class SqlFragranceRepo : IFragranceRepo
    {
        private readonly string _connectionString;
        private readonly IPasswordhasher _passwordHasher;
        private readonly ILoggger _loggger;
        public SqlFragranceRepo(string connectionString, IPasswordhasher passwordhasher, ILoggger loggger)
        {
            _connectionString = connectionString;
            _passwordHasher = passwordhasher;
            _loggger = loggger;
        }

        public async Task<IEnumerable<Fragrance>> GetFragrancesByUserId(string username, int id)
        {
            await CheckIfUserExists(username);

            string sqlQuery = "SELECT * FROM tuoksut where userId = @Id";
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var fragrance = (await conn.QueryAsync<Fragrance>(sqlQuery, new { Id = id })).ToList();
                    return fragrance;

                }
            }
            catch (Exception ex)
            {
                _loggger.Log($" [error] : {ex.Message}. {ex.StackTrace}");
                throw new Exception(" An error occured : " + ex.Message);

            }
        }
        public async Task<IEnumerable<Fragrance>> GetAllAsync()
        {
            using var conn = new SqlConnection(_connectionString);
            try
            {
                var frag = (await conn.QueryAsync<Fragrance>("select * from tuoksut;")).ToList();
                return frag;
            }
            catch (Exception ex)
            {
                _loggger.Log($" [error] : {ex.Message}. {ex.StackTrace}");
                throw new Exception(" An error occured : " + ex.Message);

            }

        }
        public async Task<Users> GetUserId(string username)
        {
            string sqlQuery = "SELECT id FROM users WHERE username = @Username";
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var userList = (await conn.QueryFirstOrDefaultAsync<Users>(sqlQuery, new { Username = username }));

                    return userList;
                }

            }
            catch (Exception ex)
            {
                _loggger.Log($" [error] : {ex.Message}. {ex.StackTrace}");
                throw new Exception(" An error occured : " + ex.Message);
            }
            return null;
        }
        public async Task<Users> CheckIfUserExists(string username)
        {
            try
            {
                string sqlQuery = "SELECT * FROM users WHERE username = @Username";

                using (var conn = new SqlConnection(_connectionString))
                {

                    var user = await conn.QueryFirstOrDefaultAsync<Users>(sqlQuery, new { Username = username });
                    
                    if (user == null) return null;
                    
                    return user;


                }

            }
            catch (Exception ex)
            {
                _loggger.Log($" [error] : {ex.Message}. {ex.StackTrace}");
                new Exception(" Error occured on (CheckIfUserExists()) : " + ex.Message);
                return null;
            }

        }
        public async Task<Users> GetBannedStatus(string username)
        {
            try
            {
                var userEntity = await GetUserId(username);
                if (userEntity == null) return null;

                string sqlQuery = "SELECT isBanned FROM users Where id = @Id";

                using (var conn = new SqlConnection(_connectionString))
                {
                    var isBanned = await conn.ExecuteScalarAsync<int>(sqlQuery, new { Id = userEntity.id });
                    if (isBanned == 1) return userEntity;
                    return null;
                }

            }
            catch (Exception ex)
            {
                _loggger.Log($" [error] : {ex.Message}. {ex.StackTrace}");
                throw new Exception(" An error occured : " + ex.Message);
            }
        }
        public async Task<Users> GetAdminStatus(string username)
        {

            try
            {
                var userEntity = await GetUserId(username);
                if (userEntity == null) return null;

                string sqlQuery = "SELECT isAdmin FROM users WHERE id = @Id";

                using (var conn = new SqlConnection(_connectionString))
                {

                    var isAdmin = await conn.ExecuteScalarAsync<int>(sqlQuery, new { Id = userEntity.id });

                    if (isAdmin == 1) return userEntity;
                    return null;
                }
            }
            catch (Exception ex)
            {
                _loggger.Log($" [error] : {ex.Message}. {ex.StackTrace}");
                throw new Exception(" An error occured : " + ex.Message);
            }
        }
        public async Task<UserSession?> Login(string username, string password)
        {
            var userEntity = await CheckIfUserExists(username);


            if (userEntity == null)
            {
                return null;
            }
            byte[] saltBytes = Convert.FromHexString(userEntity.salt);

            bool isPasswordCorrect = _passwordHasher.VerifyPassword(password, userEntity.PasswordHash, saltBytes);

            if (isPasswordCorrect)
            {

                return new UserSession(userEntity.id, userEntity.username);

            }


            return null;
        }
        public async Task AddFragrance(string username, Fragrance frag)
        {
            if (frag == null) return;
            var userEntity = await CheckIfUserExists(username);

            string sqlQuery = "INSERT INTO tuoksut (userId, Name, Brand, notes, category, weather, occasion) VALUES (@UserId, @Name, @Brand, @Notes, @Category, @Weather, @Occasion);";

            try
            {

                using (var conn = new SqlConnection(_connectionString))
                {
                    var result = await conn.ExecuteAsync(sqlQuery, new
                    {
                        UserId = userEntity.id,
                        Name = frag.name,
                        Brand = frag.brand,
                        Notes = frag.notes,
                        Category = frag.category,
                        Weather = frag.weather,
                        Occasion = frag.occasion
                    });
                }
            }
            catch (Exception ex)
            {
                _loggger.Log($" [error] : {ex.Message}. {ex.StackTrace}");
                throw new Exception(" An error occured : " + ex.Message);
            }
            return;




        }
        public async Task<UserSession> CreateNewUserAsync(string username, string email, string password)
        {


            string sqlQuery = "INSERT INTO users(username,salt,Passwordhash,email) values (@Username,@salt,@PasswordHash,@Email)";

            byte[] saltBytes;

            var passwordHash = _passwordHasher.HashPassword(password, out saltBytes);

            string saltHex = Convert.ToHexString(saltBytes);
            var userSession = await CheckIfUserExists(username);
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    await conn.ExecuteAsync(sqlQuery, new
                    {

                        Username = username,
                        salt = saltHex,
                        PasswordHash = passwordHash,
                        Email = email,

                    });
                }
            }
            catch (Exception ex)
            {
                _loggger.Log($" [error] : {ex.Message}. {ex.StackTrace}");
                throw new Exception(" An error occured : " + ex.Message);
            }


            if (userSession != null)
            {
                return new UserSession(userSession.id, userSession.username);
            }

            return null;
        }
        public async Task<int> VerifyEmail(string email)
        {
            string sqlQuery = "SELECT COUNT(1) FROM users where email = @Email";
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {

                    var exec = await conn.ExecuteScalarAsync<int>(sqlQuery, new { Email = email });
                    if (exec != 1)
                    {
                        return exec;
                    }
                    return 1;
                }
            }
            catch (Exception ex)
            {
                _loggger.Log($" [error] : {ex.Message}. {ex.StackTrace}");
                throw new Exception(" An error occured : " + ex.Message);
            }

        }
        public async Task RemoveFragranceById(int userId, int id)
        {
            // This might not be the safest way to remove but it work's for now.
            string sqlQuery = "DELETE FROM tuoksut where id = @Id and userId = @UserId";

            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    await conn.ExecuteAsync(sqlQuery, new { UserId = userId, Id = id });
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
